using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Contracts.Services.General;
using SmartB.Core.Exceptions;
using SmartB.Core.Extensions;
using SmartB.Core.Models;
using SmartB.Core.ViewModels.Base;
using SmartB.Core.Views;
using Xamarin.Forms;

namespace SmartB.Core.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private IArticoleService _articleService;
        private IJobDataService _jobDataService;
        private IMasiniService _masiniService;
        private IComenziService _orderService;
        private IPhaseService _phaseService;
        private ISettingsService _settingsService;
        private IUsersDataService _usersService;
        private IDeviceDataService _deviceDataService;
        private IDeviceInfoService _deviceInfoService;

        public HomeViewModel(IConnectionService connectionService,
                             INavigationService navigationService, 
                             IDialogService dialogService,
                             IUsersDataService usersService,
                             ISettingsService settingsService,
                             IMasiniService masiniService, 
                             IComenziService orderService,
                             IArticoleService articleService,
                             IPhaseService phaseService,
                             IJobDataService jobDataService,
                             IDeviceDataService deviceDataService, 
                             IDeviceInfoService deviceInfoService)

            : base(connectionService, navigationService, dialogService)
        {
            _masiniService = masiniService;
            _settingsService = settingsService;
            _orderService = orderService;
            _articleService = articleService;
            _phaseService = phaseService;
            _jobDataService = jobDataService;
            _usersService = usersService;
            _deviceDataService = deviceDataService;
            _deviceInfoService = deviceInfoService;

        }

        public string EmployeeName =>  _settingsService.UserNameSetting;
        public string Line => _settingsService.UserLineSettings;
        public string Sector =>   _settingsService.UserSectorSettings;
        public ICommand AddJobCommand => new Command(OnAddJobCommand);
        public ICommand ScanCommand => new Command(OnScanCommand);
        public ICommand RefreshData => new Command(OnRefreshData);

        private async void OnRefreshData()
        {
          var dialog =  _dialogService.ShowProgressDialog("Refreshing Data \n Please wait...");
          dialog.Show();
          await FetchData();
          dialog.Hide();
        }

        public override async Task InitializeAsync(object data)
        {
            var isLoggedFromYesterday = await IsCurrentUserLoggedFromYesterday();
            if (isLoggedFromYesterday) return;
            try
            {
                if (_settingsService.JobIdSettings != string.Empty)
                {
                    if (_settingsService.MachineNameSettings == "MANICHINO" || _settingsService.MachineNameSettings == "Manichino")
                    {
                        await _navigationService.NavigateToAsync<ManichinoViewModel>();
                        return;
                    }

                    await _navigationService.NavigateToAsync<JobViewModel>();
                    return;
                }

                await FetchData();

            }
            catch (HttpRequestExceptionEx e)
            {
                await _dialogService.ShowDialog(e.HttpCode.ToString(), "InitializeAsync", "OK");
            }
            catch (Exception)
            {
                //ignore
            }
        }

        private async Task FetchData()
        {
            try
            {
                var strSector = _settingsService.UserSectorSettings;
                int sector = 0;
                if (strSector == "Confection")
                    sector = 1;
                else if (strSector == "Stiro")
                    sector = 2;
                else if (strSector == "Ramendo")
                    sector = 6;
                else if (strSector == "Tessitura")
                    sector = 7;
                else if (strSector == "Sartoria")
                    sector = 8;

                Machines = (await _masiniService.GetMachinesAsync(sector, _settingsService.UserLineSettings)).ToObservableCollection();

                Orders = (await _orderService.GetOrdersAsync(sector)).ToObservableCollection();

                var machineId = _settingsService?.MachineIdSettings.ToInteger();

                SelectedMachine = _machines.Any(m => m.Id == machineId)
                    ? Machines?.FirstOrDefault(m => m.Id == machineId)
                    : null;

                SelectedOrder = _settingsService != null && _settingsService.CommessaFromBarcode != string.Empty
                    ? (await _orderService.GetOrderWithName(_settingsService.CommessaFromBarcode))
                    : null;
            }
            catch (HttpRequestExceptionEx e)
            {
                await _dialogService.ShowDialog(e.HttpCode.ToString(), "HttpRequestExceptionEx:FetchData", "OK");
            }
            catch (Exception)
            {
                //ignore
            }
        }

        private void AddMachineToSettings(Masini selectedMachine)
        {
            if (selectedMachine == null) return;
            if (!_settingsService.UserLineSettings.Contains(selectedMachine.Linie)) return;
            _settingsService.MachineIdSettings = selectedMachine.Id.ToString();
            _settingsService.MachineCodeSettings = selectedMachine.CodMasina;
            _settingsService.MachineNameSettings = selectedMachine.Descriere;
            _settingsService.MachineLineSettings = selectedMachine.Linie;
        }

        private void AddOrderToSettings(Comenzi order)
        {
            if (order == null)
            {
                Phases.Clear();
                return;
            }
            _settingsService.CommessaFromBarcode = order.NrComanda;
        }

        private async Task<bool> IsCurrentUserLoggedFromYesterday()
        {
            try
            {
                var currentDate = await _jobDataService.GetServerDateTime();
                if (Convert.ToDateTime(_settingsService.UserLoginDateSettings).Day == 0) return false;
                if (Convert.ToDateTime(_settingsService.UserLoginDateSettings).Day == currentDate.Day) return false;
                var dialog = _dialogService.ShowProgressDialog("Logging out... ");
                dialog.Show();


                var user =
                    await _usersService.GetUser(_settingsService.UserIdSetting);

                if (user.Active)
                {
                    user.Active = false;
                    await _usersService.UpdateUserActivity(user.Id.ToString(), user);
                }

                var machine =
                    await _masiniService.GetMachineAsync(_settingsService.MachineIdSettings);

                if (machine.Active)
                {
                    machine.Active = false;
                    await _masiniService.UpdateMachineActivity(machine.Id, machine);
                }

                //var device = await _deviceDataService.GetDevice(_settingsService.DeviceIdSettings);
                //if (device.Active)
                //{
                //    device.Active = false;
                //    await _deviceDataService.UpdateDevice(device, _settingsService.DeviceIdSettings);
                //}
 
                _settingsService.RemoveSettings();
          
                dialog.Hide();

                await _navigationService.ClearBackStack();
                await _navigationService.NavigateToAsync<LoginViewModel>();
            }
            catch (Exception)
            {
                // ignored
            }
            return true;
        }

        private async Task GetArticleForOrder()
        {
            try
            {
                var articleEntity = await _articleService.GetArticleAsync(_selectedOrder.IdArticol);
                if (articleEntity!=null)
                {
                    Article = articleEntity;
                }
                
            }
            catch (HttpRequestExceptionEx e)
            {
                 await _dialogService.ShowDialog(e.HttpCode.ToString(), "HttpRequestExceptionEx:GetArticleAsync", "OK"); 
            }
            catch (Exception)
            {
                //ignore
            }
        }

        private async Task GetPhasesForArticleAndMachine(int articleId, string machineCode)
        {
            try
            {
                var articlePhases = await _phaseService.GetPhasesAsync(articleId, machineCode);

                if (articlePhases == null)
                {
                        await _dialogService.ShowDialog("There are no phases for the selected article.", "Info", "OK");
                }
                else 
                { 
                    Phases = (articlePhases).ToObservableCollection();
                }
            }
            catch (HttpRequestExceptionEx e)
            {
                 await _dialogService.ShowDialog(e.HttpCode.ToString(), "HttpRequestExceptionEx:GetPhasesAsync", "OK");
            }
            catch (Exception)
            {
                //ignore
            }
        }

        //private async Task SaveDeviceUsage()
        //{
        //    try
        //    {
        //        var device = await _deviceDataService.GetDevice(_settingsService.DeviceIdSettings);

        //        if (!device.Active)
        //        {
        //            device.Active = true;
        //            await _deviceDataService.UpdateDevice(device, _settingsService.DeviceIdSettings);
        //        }

        //        var deviceInfo = new DeviceInfo
        //        {
        //            DeviceID = device.DeviceID,
        //            EmployeeID = _settingsService.UserIdSetting.ToInteger(),
        //            MachineID = _settingsService.MachineIdSettings.ToInteger(),
        //            UsedDate = await _jobDataService.GetServerDateTime()
        //        };

        //        await _deviceInfoService.AddDeviceInfo(deviceInfo);

        //    }
        //    catch (HttpRequestExceptionEx e)
        //    {
        //        await _dialogService.
        //            ShowDialog($"{e.HttpCode}",
        //                "SaveDeviceUsage:Exception",
        //                "OK");
        //    }
        //}

        private async Task SaveJob()
        {
            try
            {
                //   await SaveDeviceUsage();
                var userId = _settingsService.UserIdSetting.ToInteger();
                var machine = await _masiniService.GetMachineAsync(_settingsService.MachineIdSettings);
                var jobCreationTime = await _jobDataService.GetServerDateTime();
                var job = new Job
                {
                    IdAngajat = userId,
                    IdMasina = machine.Id,
                    IdComanda = _selectedOrder.Id,
                    IdOperatie = _selectedPhase.Id,
                    Creat = jobCreationTime,
                    Cantitate = 0410,
                    LastWrite = null,
                    Inchis = true,
                    FirstWrite = null
                };

                machine.Active = true;
                machine.LastTimeUsed = jobCreationTime;

                await _masiniService.UpdateMachineActivity(machine.Id, machine);

                var addedJob = await _jobDataService.AddJob(job);

                _settingsService.JobIdSettings = addedJob.Id.ToString();
                _settingsService.JobsIdSettings += addedJob.Id + ",";
                _settingsService.JobNormSettings = _selectedPhase.BucatiOra.ToString();
                _settingsService.OneClickWorthSettings = _selectedPhase.BucatiButon.ToString();
                _settingsService.SelectedPhaseSettings = _selectedPhase.Operatie;
                _settingsService.CounterSettings = null;
            }
            catch (HttpRequestExceptionEx e)
            {
                await _dialogService.ShowDialog($"{e.Message}",
                    "OnAddJobCommand:Exception",
                    "OK");
            }
            catch (Exception e)
            {
                await _dialogService.ShowDialog($"{e.Message}",
                    "OnAddJobCommand:Exception",
                    "OK");
            }
        }

        private async void OnAddJobCommand(object o)
        {
            var dialog = _dialogService.ShowProgressDialog("Creating job... ");
            dialog.Show();

            if (!string.IsNullOrEmpty(_settingsService.UserIdSetting) &&
                !string.IsNullOrEmpty(_settingsService.MachineIdSettings)
                && _selectedOrder != null && _selectedPhase != null)
            {
                var machine = await _masiniService.GetMachineAsync(_settingsService.MachineIdSettings);
                if (!machine.Active)
                {
                    if (machine.Descriere == "MANICHINO")
                    {
                        await SaveJob();
                        await _navigationService.NavigateToAsync<StartManichinoViewModel>();
                       // await _navigationService.NavigateToAsync<ManichinoViewModel>();
                        dialog.Hide();
                    }
                    else
                    {
                        await SaveJob();
                        await _navigationService.NavigateToAsync<StartJobViewModel>();
                        // await _navigationService.NavigateToAsync<JobViewModel>();
                        dialog.Hide();
                    }
                }
                else
                {
                    dialog.Hide();
                    await _dialogService.ShowDialog(
                        $"Machine {machine.CodeAndName} is already taken.\nPlease select another machine or contact your superior.",
                        "Warning", "OK");
                }
            }
            else
            {
                dialog.Hide();
                await _dialogService.ShowDialog("Please select all fields and then continue.", "Empty fields", "OK");
            }
        }
        private async void OnScanCommand(object obj)
        {
            await _navigationService.NavigateToAsync<ScannerViewModel>();
        }


        #region Properties
        private Articole _article;

        private ObservableCollection<Masini> _machines;
        private ObservableCollection<Comenzi> _orders;
        private ObservableCollection<Phases> _phases;

        private Masini _selectedMachine;
        private Comenzi _selectedOrder;
        private Phases _selectedPhase;
        public Articole Article
        {
            get => _article;
            set
            {
                _article = value;
                OnPropertyChanged();
                Task.Run(async () => {await GetPhasesForArticleAndMachine(_article.Id, _settingsService.MachineNameSettings); });
            }
        }
        public ObservableCollection<Masini> Machines
        {
            get => _machines;
            set
            {
                _machines = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Comenzi> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Phases> Phases
        {
            get => _phases;
            set
            {
                _phases = value;
                OnPropertyChanged();
            }
        }
        public  Masini SelectedMachine
        {
            get => _selectedMachine;
            set
            {
                _selectedMachine = value;
                OnPropertyChanged();
                AddMachineToSettings(_selectedMachine);
                if (_article == null || _selectedMachine == null) return;
                Task.Run(async () => { await GetPhasesForArticleAndMachine(_article.Id, _settingsService.MachineNameSettings); });
            }
        }
        public Comenzi SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged();
                AddOrderToSettings(_selectedOrder);
                Task.Run(async () => { await GetArticleForOrder();});
            }
        }
        public Phases SelectedPhase
        {
            get => _selectedPhase;
            set
            {
                _selectedPhase = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}