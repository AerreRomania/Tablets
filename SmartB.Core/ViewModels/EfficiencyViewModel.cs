using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Contracts.Services.General;
using SmartB.Core.DTOs;
using SmartB.Core.Extensions;
using SmartB.Core.Models;
using SmartB.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartB.Core.ViewModels
{
    public class EfficiencyViewModel:ViewModelBase
    {

        private IButoaneService _butoaneService;
        private IJobEfficiencyService _jobEfficiency;
        private IJobDataService _jobService;
        private IMasiniService _masiniService;
        private Pause _pause;
        private IPauseService _pauseService;
        private ISettingsService _settingsService;
        private IUsersDataService _usersService;
        private IDeviceDataService _deviceDataService;
        readonly ICommessaTimService _commessaService;
        readonly IComenziService _comenziService;

        public EfficiencyViewModel(IConnectionService connectionService,
                            INavigationService navigationService,
                            IDialogService dialogService,
                            ISettingsService settingsService,
                            IJobDataService jobService,
                            IButoaneService butoaneService,
                            IPauseService pauseService,
                            IJobEfficiencyService jobEfficiency,
                            IMasiniService masiniService,
                            IUsersDataService usersDataService,
                            IDeviceDataService deviceDataService,
                            ICommessaTimService commessaService,
                            IComenziService comenziService) :
                            base(connectionService, navigationService, dialogService)
        {
            _jobEfficiency = jobEfficiency;
            _settingsService = settingsService;
            _jobService = jobService;
            _butoaneService = butoaneService;
            _pauseService = pauseService;
            _masiniService = masiniService;
            _usersService = usersDataService;
            _deviceDataService = deviceDataService;
            _commessaService = commessaService;
            _comenziService = comenziService;
        }
        public override async Task InitializeAsync(object data)
        {
            await ShiftControl(DateTime.Now.Hour);
            await FillLocalJobData();
        }
        private async Task ShiftControl(int hour)
        {
            if (hour <= 15)
            {
                FirstShift = true;
                SecondShift = false;
            }
            else
            {
                FirstShift = false;
                SecondShift = true;
            }
            await Task.Delay(100);
        }
            private async Task FillLocalJobData()
        {
            await Task.Delay(100);
            try
            {
                Hours = new Hours
                {
                    H6 = _settingsService.H6Settings.ToInteger(),
                    H7 = _settingsService.H7Settings.ToInteger(),
                    H8 = _settingsService.H8Settings.ToInteger(),
                    H9 = _settingsService.H9Settings.ToInteger(),
                    H10 = _settingsService.H10Settings.ToInteger(),
                    H11 = _settingsService.H11Settings.ToInteger(),
                    H12 = _settingsService.H12Settings.ToInteger(),
                    H13 = _settingsService.H13Settings.ToInteger(),
                    H14 = _settingsService.H14Settings.ToInteger(),
                    H15 = _settingsService.H15Settings.ToInteger(),
                    H16 = _settingsService.H16Settings.ToInteger(),
                    H17 = _settingsService.H17Settings.ToInteger(),
                    H18 = _settingsService.H18Settings.ToInteger(),
                    H19 = _settingsService.H19Settings.ToInteger(),
                    H20 = _settingsService.H20Settings.ToInteger(),
                    H21 = _settingsService.H21Settings.ToInteger(),
                    H22 = _settingsService.H22Settings.ToInteger(),
                    H23 = _settingsService.H23Settings.ToInteger()
                };
                EfficiencyForHours = new EfficiencyForHours
                {
                    H6Efficiency = _settingsService.H6EfficiencySettings.ToDouble(),
                    H7Efficiency = _settingsService.H7EfficiencySettings.ToDouble(),
                    H8Efficiency = _settingsService.H8EfficiencySettings.ToDouble(),
                    H9Efficiency = _settingsService.H9EfficiencySettings.ToDouble(),
                    H10Efficiency = _settingsService.H10EfficiencySettings.ToDouble(),
                    H11Efficiency = _settingsService.H11EfficiencySettings.ToDouble(),
                    H12Efficiency = _settingsService.H12EfficiencySettings.ToDouble(),
                    H13Efficiency = _settingsService.H13EfficiencySettings.ToDouble(),
                    H14Efficiency = _settingsService.H14EfficiencySettings.ToDouble(),
                    H15Efficiency = _settingsService.H15EfficiencySettings.ToDouble(),
                    H16Efficiency = _settingsService.H16EfficiencySettings.ToDouble(),
                    H17Efficiency = _settingsService.H17EfficiencySettings.ToDouble(),
                    H18Efficiency = _settingsService.H18EfficiencySettings.ToDouble(),
                    H19Efficiency = _settingsService.H19EfficiencySettings.ToDouble(),
                    H20Efficiency = _settingsService.H20EfficiencySettings.ToDouble(),
                    H21Efficiency = _settingsService.H21EfficiencySettings.ToDouble(),
                    H22Efficiency = _settingsService.H22EfficiencySettings.ToDouble(),
                    H23Efficiency = _settingsService.H23EfficiencySettings.ToDouble()
                };
               // Norm = _settingsService.JobNormSettings;
                EfficiencyTotal = _settingsService.TotalEfficiencySettings.ToDouble();
                Counter = _settingsService.CounterSettings.ToInteger();
               // TotalPieces = _settingsService.TotalPiecesSettings.ToInteger();
                //EfficiencyHour = _settingsService.HourEfficiencySettings.ToInteger();
              //  HourCounter = _settingsService.HourCounterSettings.ToInteger();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private async Task PiecesByHour(DateTime date)
        {
            await Task.Delay(100);
            Hours[$"H{date.Hour}"] = Counter <= 1 ? Hours[$"H{date.Hour}"] = 1 : (int)Hours[$"H{date.Hour}"] + 1;
            HourCounter = (int)Hours[$"H{date.Hour}"];
            OnPropertyChanged(nameof(Hours));
            switch (date.Hour)
            {
                case 6:
                    _settingsService.H6Settings = Hours.H6.ToString();
                    break;
                case 7:
                    _settingsService.H7Settings = Hours.H7.ToString();
                    break;
                case 8:
                    _settingsService.H8Settings = Hours.H8.ToString();
                    break;
                case 9:
                    _settingsService.H9Settings = Hours.H9.ToString();
                    break;
                case 10:
                    _settingsService.H10Settings = Hours.H10.ToString();
                    break;
                case 11:
                    _settingsService.H11Settings = Hours.H11.ToString();
                    break;
                case 12:
                    _settingsService.H12Settings = Hours.H12.ToString();
                    break;
                case 13:
                    _settingsService.H13Settings = Hours.H13.ToString();
                    break;
                case 14:
                    _settingsService.H14Settings = Hours.H14.ToString();
                    break;
                case 15:
                    _settingsService.H15Settings = Hours.H15.ToString();
                    break;
                case 16:
                    _settingsService.H16Settings = Hours.H16.ToString();
                    break;
                case 17:
                    _settingsService.H17Settings = Hours.H17.ToString();
                    break;
                case 18:
                    _settingsService.H18Settings = Hours.H18.ToString();
                    break;
                case 19:
                    _settingsService.H19Settings = Hours.H19.ToString();
                    break;
                case 20:
                    _settingsService.H20Settings = Hours.H20.ToString();
                    break;
                case 21:
                    _settingsService.H21Settings = Hours.H21.ToString();
                    break;
                case 22:
                    _settingsService.H22Settings = Hours.H22.ToString();
                    break;
                case 23:
                    _settingsService.H23Settings = Hours.H23.ToString();
                    break;
            }
        }
        private async Task EfficiencyByHour(DateTime date)
        {
            await Task.Delay(100);
            EfficiencyForHours[$"H{date.Hour}Efficiency"] = Counter <= 1 ? 0.0 : EfficiencyHour;
            OnPropertyChanged(nameof(EfficiencyForHours));
            switch (date.Hour)
            {
                case 6:
                    _settingsService.H6EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 7:
                    _settingsService.H7EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 8:
                    _settingsService.H8EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 9:
                    _settingsService.H9EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 10:
                    _settingsService.H10EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 11:
                    _settingsService.H11EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 12:
                    _settingsService.H12EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 13:
                    _settingsService.H13EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 14:
                    _settingsService.H14EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 15:
                    _settingsService.H15EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 16:
                    _settingsService.H16EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 17:
                    _settingsService.H17EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 18:
                    _settingsService.H18EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 19:
                    _settingsService.H19EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 20:
                    _settingsService.H20EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 21:
                    _settingsService.H21EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 22:
                    _settingsService.H22EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
                case 23:
                    _settingsService.H22EfficiencySettings =
                        EfficiencyForHours[$"H{date.Hour}Efficiency"].ToString();
                    break;
            }
        }
        public int HourCounter { get; set; }
        private Hours _hours;
        public Hours Hours
        {
            get => _hours;
            set
            {
                if (_hours != value)
                {
                    _hours = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _counter;
        public int Counter
        {
            get { return _counter; }
            set
            {
                _counter = value;
                OnPropertyChanged();
            }
        }
        private double _efficiencyCurrent;
        public double EfficiencyCurrent
        {
            get { return _efficiencyCurrent; }
            set
            {
                _efficiencyCurrent = value;
                OnPropertyChanged();
            }
        }
        private double _efficiencyTotal;
        public double EfficiencyTotal
        {
            get { return _efficiencyTotal; }
            set
            {
                _efficiencyTotal = value;
                OnPropertyChanged();
            }
        }
        private EfficiencyForHours _efficiencyForHours;
        public EfficiencyForHours EfficiencyForHours
        {
            get => _efficiencyForHours;
            set
            {
                if (_efficiencyForHours != value)
                {
                    _efficiencyForHours = value;
                    OnPropertyChanged();
                }
            }
        }
        private double _efficiencyHour;
        public double EfficiencyHour
        {
            get
            {
                return _efficiencyHour;
            }
            set
            {
                _efficiencyHour = value;
                OnPropertyChanged();
            }
        }
        private bool _firstShift;

        private bool _secondShift;

        public bool FirstShift
        {
            get { return _firstShift; }
            set
            {
                _firstShift = value;
                OnPropertyChanged();
            }
        }
        public bool SecondShift
        {
            get { return _secondShift; }
            set
            {
                _secondShift = value;
                OnPropertyChanged();
            }
        }
    }
}
