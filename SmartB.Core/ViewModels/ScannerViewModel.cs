using System;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Contracts.Services.General;
using SmartB.Core.Exceptions;
using SmartB.Core.Models;
using SmartB.Core.ViewModels.Base;
using Xamarin.Forms;
using ZXing;
using Device = Xamarin.Forms.Device;
namespace SmartB.Core.ViewModels
{
     public class ScannerViewModel : ViewModelBase
    {
        private IAuthenticationService _authenticationService;
        private ICommessaTimService _commessaTimService;
        private IUsersDataService _userService;
        private bool _isAnalyzing = true;
        private bool _isScanning = true;
        private IJobDataService _jobDataService;
        private ISettingsService _settingsService;
        private IMasiniService _machineService;
        //private IDeviceDataService _deviceDataService;
        public ScannerViewModel(IConnectionService connectionService,
            INavigationService navigationService,
            IDialogService dialogService,
            IUsersDataService userService,
            ISettingsService settingsService,
            IAuthenticationService authenticationService,
            ICommessaTimService commessaTimService, 
            IJobDataService jobDataService,
            IMasiniService machineService) 
            : base(connectionService, navigationService, dialogService)
        {
            _machineService = machineService ?? throw new ArgumentNullException(nameof(machineService));
            _jobDataService = jobDataService ?? throw new ArgumentNullException(nameof(jobDataService));
            _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _commessaTimService = commessaTimService ?? throw new ArgumentNullException(nameof(commessaTimService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
          //  _deviceDataService = deviceDataService ?? throw new ArgumentNullException(nameof(deviceDataService));
        }
        public bool IsAnalyzing
        {
            get { return _isAnalyzing; }
            set
            {
                if (!Equals(_isAnalyzing, value))
                {
                    _isAnalyzing = value;
                    OnPropertyChanged(nameof(IsAnalyzing));
                }
            }
        }
        public bool IsScanning
        {
            get { return _isScanning; }
            set
            {
                if (!Equals(_isScanning, value))
                {
                    _isScanning = value;
                    OnPropertyChanged(nameof(IsScanning));
                }
            }
        }
        public Command QRScanResultCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsAnalyzing = false;
                    IsScanning = false;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        string[] scannerData = Result.Text?.Split('-');
                        if (Result.BarcodeFormat == BarcodeFormat.CODE_39)
                        {
                            CommessaWithBarcode(Convert.ToInt32(Result.Text).ToString());
                        }
                        if (scannerData?[0] == "User")
                        {
                            LoginWithBarcode(scannerData[1], scannerData[2]);
                        }
                        if (scannerData?[0] == "Machine")
                        {
                            MachineWithBarCode(scannerData[1]);
                        }
                    });
                });
            }
        }
        public Result Result { get; set; }
        private async void CommessaWithBarcode(string barcode)
        {
            IsBusy = true;
            if (_connectionService.IsConnected)
            {
                if (!string.IsNullOrEmpty(_settingsService.UserIdSetting))
                {
                    try
                    {
                        var commessa = await _commessaTimService.GetCommessaTimAsync(barcode);
                        var splitedText = commessa.Commessa.Split('-');
                        _settingsService.CommessaFromBarcode = splitedText[2];
                        IsBusy = false;
                        await _navigationService.NavigateToAsync<MainViewModel>();
                        await _dialogService.ShowDialog($"Commessa {commessa.Commessa} is added", string.Empty, "OK");
                    }
                    catch (Exception e)
                    {
                        await _dialogService.ShowDialog(e.ToString(), "Exception", "OK");
                    }
                }
                else
                {
                    await _navigationService.NavigateToAsync<LoginViewModel>();
                    await _dialogService.ShowDialog("You must login first.", "Information", "OK");
                }
            }
            else
            {
                await _navigationService.NavigateToAsync<MainViewModel>();
                await _dialogService.ShowDialog(
                     "Connection problem please try again later.",
                     "Internet connection problem",
                     "OK");
            }
        }
        //private async Task DeviceInfo()
        //{
        //    try
        //    {
        //        var device = new Models.Device
        //        {
        //            SN = CrossDeviceInfo.Current.Id,
        //            Type = CrossDeviceInfo.Current.Idiom.ToString(),
        //            Model = CrossDeviceInfo.Current.Model,
        //            Platform = CrossDeviceInfo.Current.Platform.ToString(),
        //            Version = CrossDeviceInfo.Current.VersionNumber.ToString()
        //        };

        //        var deviceEntity = await _deviceDataService.AddDevice(device);

        //        if (deviceEntity.DeviceID != 0)
        //        {
        //            _settingsService.DeviceIdSettings = deviceEntity.DeviceID.ToString();
        //        }
        //        else
        //        {
        //            var deviceFromDatabase = await _deviceDataService.GetDeviceWithSN(device.SN);
        //            _settingsService.DeviceIdSettings = deviceFromDatabase.DeviceID.ToString();
        //        }

        //    }
        //    catch (HttpRequestExceptionEx)
        //    {

        //    }

        //}
        private async void LoginWithBarcode(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(_settingsService.UserNameSetting))
            {
                IsBusy = true;
                var dialog = _dialogService.ShowProgressDialog("Logging in... ");
                dialog.Show();
                if (_connectionService.IsConnected)
                {
                    try
                    {
                        AuthenticationResponse authenticationResponse =
                            await _authenticationService.Authenticate(username, password);
                        if (authenticationResponse.IsAuthenticated)
                        {
                            var timeWhenUserLogged = await _jobDataService.GetServerDateTime();
                            // we store the Id to know if the user is already logged in to the application
                            _settingsService.UserIdSetting = authenticationResponse.User.Id.ToString();
                            _settingsService.UserNameSetting = authenticationResponse.User.Angajat;
                            var strSector = string.Empty;
                            var idSector = authenticationResponse.User.IdSector;
                            if (idSector == 1)
                                strSector = "Confection";
                            else if (idSector == 2)
                                strSector = "Stiro";
                            else if (idSector == 6)
                                strSector = "Ramendo";
                            else if (idSector == 7)
                                strSector = "Tessitura";
                            else if (idSector == 8)
                                strSector = "Sartoria";
                            _settingsService.UserSectorSettings = strSector;
                            _settingsService.UserLineSettings = authenticationResponse.User.Linie;
                            _settingsService.UserLoginDateSettings = timeWhenUserLogged.ToString();
                            IsBusy = false;
                            var user = authenticationResponse.User;
                            user.Active = true;
                            user.LastTimeLogged = timeWhenUserLogged;
                            await _userService.UpdateUserActivity(user.Id.ToString(), user);
                            //await DeviceInfo();
                            await _navigationService.NavigateToAsync<MainViewModel>();
                        }
                        dialog.Hide();
                    }
                    catch (HttpRequestExceptionEx e)
                    {
                        dialog.Hide();
                        await _navigationService.NavigateToAsync<LoginViewModel>();
                        await _dialogService.ShowDialog(
                            $"This username/password combination isn't known. HTTP Code: {e.HttpCode} Error Message: {e.Message}",
                            "Error logging you in",
                            "OK");
                    }
                    catch (Exception exception)
                    {
                        dialog.Hide();
                        await _navigationService.NavigateToAsync<LoginViewModel>();
                        await _dialogService.ShowDialog(
                            $"Unknown error occured: {exception.Message}",
                            "Error logging you in",
                            "OK");
                    }
                }
                else
                {
                    dialog.Hide();
                    await _navigationService.NavigateToAsync<LoginViewModel>();
                    await _dialogService.ShowDialog(
                        "Connection problem please try again later.",
                        "Internet connection problem",
                        "OK");
                }
            }
            else
            {
                await _navigationService.NavigateToAsync<MainViewModel>();
                await _dialogService.ShowDialog(
                    $"User already logged in with name {_settingsService.UserNameSetting}, please logout to change user account",
                    "User logged", "OK");
            }
        }
        private async void MachineWithBarCode(string id)
        {
            IsBusy = true;
            if (_connectionService.IsConnected)
            {
                if (!string.IsNullOrWhiteSpace(_settingsService.UserNameSetting))
                {
                    var machine = await _machineService.GetMachineAsync(id);
                    if (machine.Occupied)
                    {
                        await _navigationService.NavigateToAsync<MainViewModel>();
                        await _dialogService.ShowDialog(
                            $"Two persons can't be logged on one machine at the same time!",
                            "Machine is occupied!",
                            "OK");
                    }
                    else
                    {
                        if (machine.Linie.Contains(_settingsService.UserLineSettings))
                        {
                            try
                            {
                                // we store the Id to know if the user is already logged in to the application
                                _settingsService.MachineIdSettings = id;
                                _settingsService.MachineCodeSettings = machine.CodMasina;
                                _settingsService.MachineNameSettings = machine.Descriere;
                                _settingsService.MachineLineSettings = machine.Linie;
                                IsBusy = false;

                                int.TryParse(id, out var machineId);
                                MasiniForUpdate machineToUpdate = new MasiniForUpdate
                                {
                                    Id = machineId,
                                    Occupied = true
                                };


                                await _machineService.UpdateMachineActivity(machineToUpdate);

                                await _navigationService.NavigateToAsync<MainViewModel>();
                            }
                            catch (Exception e)
                            {
                                await _dialogService.ShowDialog(e.ToString(), "Exception", "OK");
                            }
                        }
                        else
                        {
                            await _navigationService.NavigateToAsync<MainViewModel>();
                            await _dialogService.ShowDialog(
                                $"You selected wrong line. Scanned machine belong to {machine.Linie} and your selected line by manager is {_settingsService.UserLineSettings}. " +
                                "Please contact your superior!",
                                "Wrong line!",
                                "OK");
                        }
                    }
                }
                else
                {
                    await _navigationService.NavigateToAsync<LoginViewModel>();
                    await _dialogService.ShowDialog(
                        "First try to sing in, then you can scan the machine.",
                        "Wrong barcode!",
                        "OK");
                }
            }
            else
            {
                await _navigationService.NavigateToAsync<MainViewModel>();
                await _dialogService.ShowDialog(
                     "Connection problem please try again later.",
                     "Internet connection problem",
                     "OK");
            }
        }
    }
}
