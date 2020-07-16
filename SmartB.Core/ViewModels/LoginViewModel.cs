using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Contracts.Services.General;
using SmartB.Core.Exceptions;
using SmartB.Core.Extensions;
using SmartB.Core.ViewModels.Base;
using Xamarin.Forms;
namespace SmartB.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private IAuthenticationService _authenticationService;
        private IJobDataService _jobDataSevice;
        private ISettingsService _settingsService;
        private IUsersDataService _userDataService;
        //private IDeviceDataService _deviceDataService;
        private string _username;
        private string _password;
        private ObservableCollection<string> _names;
        public LoginViewModel(IConnectionService connectionService, INavigationService navigationService, IDialogService dialogService, 
            ISettingsService settingsService, IAuthenticationService authenticationService, IUsersDataService usersDataService, IJobDataService jobDataService) 
            : base(connectionService, navigationService, dialogService)
        {
            _jobDataSevice = jobDataService;
            _settingsService = settingsService;
            _authenticationService = authenticationService;
            _userDataService = usersDataService; 
            //_deviceDataService = deviceDataService;
        }
        public ICommand LoginCommand => new Command(OnLogin);
        public ICommand ScannerCommand => new Command(OnScan);
        public ObservableCollection<string> NamesList
        {
            get { return _names; }
            set
            {
                _names = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        public override async Task InitializeAsync(object data)
        {
            NamesList = (await _userDataService.GetAllUserNames()).ToObservableCollection();
        }
        //private async Task DeviceInfo()
        //{
        //    try
        //    {
        //        var device = new Models.Device
        //        {
        //            SN =  CrossDeviceInfo.Current.Id,
        //            Type = CrossDeviceInfo.Current.Idiom.ToString(),
        //            Model = CrossDeviceInfo.Current.Model,
        //            Platform = CrossDeviceInfo.Current.Platform.ToString(),
        //            Version = CrossDeviceInfo.Current.VersionNumber.ToString()
        //        };

        //    var deviceEntity =  await _deviceDataService.AddDevice(device);

        //    if (deviceEntity.DeviceID != 0)
        //    {
        //        _settingsService.DeviceIdSettings = deviceEntity.DeviceID.ToString();
        //    }
        //    else
        //    {
        //        var deviceFromDatabase = await _deviceDataService.GetDeviceWithSN(device.SN);
        //        if (deviceFromDatabase !=null)
        //        {
        //            _settingsService.DeviceIdSettings = deviceFromDatabase.DeviceID.ToString();
        //        }
        //    }

        //    }
        //    catch (HttpRequestExceptionEx)
        //    {
               
        //    }

        //}
        private async void OnLogin(object obj)
        {
            IsBusy = true;
            var dialog = _dialogService.ShowProgressDialog("Logging in... ");
            dialog.Show();
            if (_connectionService.IsConnected)
            {
                try
                {
                    var authenticationResponse = await _authenticationService.Authenticate(UserName, Password);
                    if (authenticationResponse.IsAuthenticated)
                    {
                        var timeWhenUserLogged = await _jobDataSevice.GetServerDateTime();
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
                        var user = authenticationResponse.User;
                        user.Active = true;
                        user.LastTimeLogged = timeWhenUserLogged;
                        await _userDataService.UpdateUserActivity(user.Id.ToString(), user);
                        //      await DeviceInfo();
                        await _navigationService.NavigateToAsync<MainViewModel>();
                        dialog.Hide();
                        IsBusy = false;
                    }
                }
                catch (HttpRequestExceptionEx exception)
                {
                    dialog.Hide();
                  await  _dialogService.ShowDialog(exception.Message, "Http request error", "OK");
                } 
                catch (Exception exception)
                {
                    dialog.Hide();
                    await _dialogService.ShowDialog(exception.Message, "Http request error", "OK");
                    // ignored
                }
            }
            else
            {
                dialog.Hide();
                await _dialogService.ShowDialog(
                    "Connection problem please try again later.",
                    "Internet connection problem",
                    "OK");
            }
        }
        private async void OnScan(object obj)
        {
           await _navigationService.NavigateToAsync<ScannerViewModel>();
        }
    }
}