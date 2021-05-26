using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Contracts.Services.General;
using SmartB.Core.DTOs;
using SmartB.Core.Exceptions;
using SmartB.Core.Extensions;
using SmartB.Core.Models;
using SmartB.Core.ViewModels.Base;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;
namespace SmartB.Core.ViewModels
{
    public class JobViewModel : ViewModelBase
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
        
        public JobViewModel(IConnectionService connectionService, 
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
            _workPermitOverQuantity = false;
            
        }
        public string Commessa => _settingsService.CommessaFromBarcode;
        public string EmployeeName => _settingsService.UserNameSetting;
        public string MachineCode => _settingsService.MachineCodeSettings;
        public string Phase => _settingsService.SelectedPhaseSettings;
        public string Sector => _settingsService.UserSectorSettings;
        public ICommand PauseJobAcceptCommand => new Command(OnPauseJobAcceptCommand);
        public ICommand PauseJobCommand => new Command(OnPauseJobCommand);
        public ICommand SavePiece => new Command(OnSavePiece);
        public ICommand StopJobCommand => new Command(OnStopJobCommand);
        public ICommand StopStopwatchCommand => new Command(OnStopStopwatchCommand);
        public ICommand EfficiencyCommand => new Command(OnEfficiecyCommand);

        private async void OnEfficiecyCommand(object obj)
        {
            var dialog = _dialogService.ShowProgressDialog("Please Wait...");
            dialog.Show();
            await _navigationService.NavigateToAsync<EfficiencyViewModel>();
            dialog.Hide();
        }
        public override async Task InitializeAsync(object data)
        {
            var currentDate = await _jobService.GetServerDateTime();
            //var isLoggedFromYesterday = await IsCurrentUserLoggedFromYesterday();
            //if (isLoggedFromYesterday)
            //    return;
            ////   MessagingCenter.Send(this, "connectToBT", _settingsService.MachineCodeSettings);
            await ShiftControl(currentDate.Hour);
           
            if (DateTime.Parse(_settingsService.UserLoginDateSettings).Day == 0) _settingsService.CounterSettings = "0" ;
            if (DateTime.Parse(_settingsService.UserLoginDateSettings).Day != currentDate.Day) _settingsService.CounterSettings = "0";
            await FillLocalJobData();
            var normHour = _settingsService.JobNormSettings.ToInteger();
            var clickWorth = _settingsService.OneClickWorthSettings.ToInteger();
            if (clickWorth != 1)
            {
                if (_settingsService.IsNormCalculatedSettings.Equals("true"))
                {
                    Norm = normHour.ToString();
                }
                else
                {
                    Norm = (normHour / clickWorth).ToString();
                    _settingsService.JobNormSettings = Norm;
                    _settingsService.IsNormCalculatedSettings = "true";
                }
            }
        }
        private async Task<bool> IsCurrentUserLoggedFromYesterday()
        {
            try
            {
                 var currentDate = await _jobService.GetServerDateTime();
                //var currentDate = DateTime.Now.AddDays(1);
                //var currentDate = DateTime.Now;
                if (DateTime.Parse(_settingsService.UserLoginDateSettings).Day == 0) return false;
                if (DateTime.Parse(_settingsService.UserLoginDateSettings).Day == currentDate.Day) return false;
                var dialog = _dialogService.ShowProgressDialog("Checking user... ");
                dialog.Show();
                await UpdateJobLastWrite(false);
                var user = await _usersService.GetUser(_settingsService.UserIdSetting);
                if (user.Active)
                {
                    //user.Active = false;
                    //await _usersService.UpdateUserActivity(user.Id.ToString(), user);
                    dialog.Hide();
                    return true;
                }
                else
                {
                    _settingsService.RemoveSettings();
                    dialog.Hide();
                    await _navigationService.ClearBackStack();
                    await _navigationService.NavigateToAsync<LoginViewModel>();
                    return false;
                }
            }
            catch (Exception)
            {
                // ignored
                return false;
            }
        }
        private async Task WaitAndExecute(int milliseconds, Action actionToExecute)
        {
            await Task.Delay(milliseconds);
            actionToExecute();
        }
        private async Task AddEfficiencyForJob(Job job)
        {
            try
            {
                var totalpauses = new TimeSpan();
                var pauses = await _pauseService.GetPauses(job.Id.ToString());
                if (pauses!=null)
                {
                    foreach (var pause in pauses)
                    {
                        if (pause.Type == "Pause 1")
                        {
                            totalpauses += pause.EndPause - pause.StartPause;
                        }
                        if (pause.Type == "Pause 2")
                        {
                            totalpauses += pause.EndPause - pause.StartPause;
                        }
                    }
                }
                var span = job.LastWrite - job.FirstWrite - totalpauses;
                if (span != null)
                {
                    var norm = _settingsService.JobNormSettings.ToInteger();
                   // var currentTime = await _jobService.GetServerDateTime();
                    double normForHours = _counter / norm * 100f;
                    var efficiency = span.Value.Ticks == 0 ? 0.0 : normForHours;
                    var jobEfficiency = new JobEfficiency
                    {
                        Efficiency = efficiency,
                        RealizariID = job.Id,
                        SpentTime = span.Value.Ticks
                    };
                    var efficiencyEntity = await _jobEfficiency.AddJobEfficiency(jobEfficiency);
                    _settingsService.EfficiencyIdsSettings += $"{efficiencyEntity.EfficiencyID},";
                }
            }
            catch (Exception e)
            {
                await _dialogService.ShowDialog($"{e.Message}", "", "OK");
            }
        }
        private void EnableClickPieceButton()
        {
            
            IsButtonEnabled = true;
            IsBusyIndicator = false;
        }
        private async Task FillLocalJobData()
        {
            await Task.Delay(100);
            try
            {
                //Hours = new Hours
                //{
                //    H6 = _settingsService.H6Settings.ToInteger(),
                //    H7 = _settingsService.H7Settings.ToInteger(),
                //    H8 = _settingsService.H8Settings.ToInteger(),
                //    H9 = _settingsService.H9Settings.ToInteger(),
                //    H10 = _settingsService.H10Settings.ToInteger(),
                //    H11 = _settingsService.H11Settings.ToInteger(),
                //    H12 = _settingsService.H12Settings.ToInteger(),
                //    H13 = _settingsService.H13Settings.ToInteger(),
                //    H14 = _settingsService.H14Settings.ToInteger(),
                //    H15 = _settingsService.H15Settings.ToInteger(),
                //    H16 = _settingsService.H16Settings.ToInteger(),
                //    H17 = _settingsService.H17Settings.ToInteger(),
                //    H18 = _settingsService.H18Settings.ToInteger(),
                //    H19 = _settingsService.H19Settings.ToInteger(),
                //    H20 = _settingsService.H20Settings.ToInteger(),
                //    H21 = _settingsService.H21Settings.ToInteger(),
                //    H22 = _settingsService.H22Settings.ToInteger(),
                //    H23 = _settingsService.H23Settings.ToInteger()
                //};
                //EfficiencyForHours = new EfficiencyForHours
                //{
                //    H6Efficiency = _settingsService.H6EfficiencySettings.ToDouble(),
                //    H7Efficiency = _settingsService.H7EfficiencySettings.ToDouble(),
                //    H8Efficiency = _settingsService.H8EfficiencySettings.ToDouble(),
                //    H9Efficiency = _settingsService.H9EfficiencySettings.ToDouble(),
                //    H10Efficiency = _settingsService.H10EfficiencySettings.ToDouble(),
                //    H11Efficiency = _settingsService.H11EfficiencySettings.ToDouble(),
                //    H12Efficiency = _settingsService.H12EfficiencySettings.ToDouble(),
                //    H13Efficiency = _settingsService.H13EfficiencySettings.ToDouble(),
                //    H14Efficiency = _settingsService.H14EfficiencySettings.ToDouble(),
                //    H15Efficiency = _settingsService.H15EfficiencySettings.ToDouble(),
                //    H16Efficiency = _settingsService.H16EfficiencySettings.ToDouble(),
                //    H17Efficiency = _settingsService.H17EfficiencySettings.ToDouble(),
                //    H18Efficiency = _settingsService.H18EfficiencySettings.ToDouble(),
                //    H19Efficiency = _settingsService.H19EfficiencySettings.ToDouble(),
                //    H20Efficiency = _settingsService.H20EfficiencySettings.ToDouble(),
                //    H21Efficiency = _settingsService.H21EfficiencySettings.ToDouble(),
                //    H22Efficiency = _settingsService.H22EfficiencySettings.ToDouble(),
                //    H23Efficiency = _settingsService.H23EfficiencySettings.ToDouble()
                //};
                Norm = _settingsService.JobNormSettings;
                EfficiencyTotal = _settingsService.TotalEfficiencySettings.ToDouble();
                Counter = _settingsService.CounterSettings.ToInteger();
                TotalPieces = _settingsService.TotalPiecesSettings.ToInteger();
                //EfficiencyHour = _settingsService.HourEfficiencySettings.ToInteger();
                //HourCounter = _settingsService.HourCounterSettings.ToInteger();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private async Task<double> NormForHours(int norm, DateTime currentDate)
        {
            var job = await _jobService.GetJob(_settingsService.JobIdSettings);
            var firstClick = job.FirstWrite;
            var lastClick = await _jobService.GetLastClickForJob(job.Id.ToString());
            var span = DateTime.Parse(_settingsService.UserLoginDateSettings).Day != currentDate.Day ? lastClick - firstClick
                : currentDate - firstClick;
            if (span != null && span.Value.TotalHours > 1)
            {
                return norm * span.Value.TotalHours;
            }
            return norm;
        }
        private async void OnPauseJobAcceptCommand(object obj)
        {
            try
            {
                if (_pause1Switch)
                {
                    PauseType = "Pause 1";
                }
                else if (_pause2Switch)
                {
                    PauseType = "Pause 2";
                }
                else if (_wcSwitch)
                {
                    PauseType = "WC";
                }
                else if (_technicalProblemsSwitch)
                {
                    PauseType = "Technical Problem";
                }
                else if (_otherSwitch)
                {
                    PauseType = "Other";
                }
                else
                {
                    await _dialogService.ShowDialog("Please select pause type", "Information", "OK");
                    return;
                }
                _pause = new Pause
                {
                    Type = PauseType,
                    StartPause = await _jobService.GetServerDateTime(),
                    RealizareID = Convert.ToInt32(_settingsService.JobIdSettings)
                };
                await StartStopwatch();
            }
            catch (HttpRequestExceptionEx e)
            {
                await _dialogService.ShowDialog(e.Message, "HttpRequestExceptionEx:OnPauseJobAcceptCommand", "OK");
            }
            catch (Exception)
            {
                //ignore
            }
        }
        private void OnPauseJobCommand(object obj)
        {
            CanShowPausePopup = true;
        }
        private async void OnSavePiece(object obj)
        {
            try
            {
                var clickTime = await _jobService.GetServerDateTime();
                await ShiftControl(clickTime.Hour);
                if (_connectionService.IsConnected)
                {
                    int.TryParse(_settingsService.CommessaIdSettings, out int commessaId);
                    int.TryParse(_settingsService.PhaseIdSettings, out int phaseId);
                    string barCode =_settingsService.CommessaFromBarcode;
                    var commessa = await _comenziService.GetOrderWithName(barCode);
                    int producedQuantity = await _jobService.GetProducedPieces(commessaId, phaseId);
                  
                    if (producedQuantity >= commessa.Cantitate && !_workPermitOverQuantity)
                    {
                        // await _dialogService.ShowDialog("Test", "Test", "Ok");
                        var result = await _dialogService.ShowPromptDialog("Please enter the pin to continue and make more.", "Maxim Quanity is done!", "Ok", "Cancel", "Pin");
                        if (!result.Ok)
                            return;
                        else
                        {
                            string pin = result.Text;
                            if (!string.IsNullOrEmpty(pin))
                            {
                                bool isManager = await _usersService.GetManagerAsync(pin);

                                if (isManager)
                                    _workPermitOverQuantity = true;
                                else
                                {
                                    await _dialogService.ShowDialog("Please change JOB!", "Maxim quantity is done", "Ok");
                                    return;
                                }
                            }
                            else
                            {
                                await _dialogService.ShowDialog("Please change JOB!", "Maxim quantity is done", "Ok");
                                return;
                            }
                        }
                    }

                    var normHour = _settingsService.JobNormSettings.ToInteger();
                    var idleClickTime = new TimeSpan(1, 0, 0).TotalMinutes / normHour;
                    
                    var click = new Click
                    {
                        Adresa = 0410,
                        Buton = false,
                        IdRealizare = _settingsService.JobIdSettings.ToInteger(),
                        IdDifetto = null
                    };
                    Counter++;
                    TotalPieces++;
                    await CheckMachineState();
                    //await PiecesByHour(clickTime); // no pieces
                    await _butoaneService.AddClick(click);
                    //  await UpdateJobFirstWrite(clickTime); 
                    //await WeightedAverage(idleClickTime, clickTime);
                    //await EfficiencyByHour(clickTime); //no eff
                    _settingsService.LastClickSetting = clickTime.ToString();
                    await WaitAndExecute(10000,EnableClickPieceButton);
                    _settingsService.CounterSettings = Counter.ToString();
                    _settingsService.TotalEfficiencySettings = EfficiencyTotal.ToString();
                    _settingsService.TotalPiecesSettings = TotalPieces.ToString();
                }
                else
                {
                    await _dialogService.ShowDialog(
                        "Connection problem please connect your device to WIFI and try again.",
                        "Internet connection problem",
                        "OK");
                }
            }
            catch (HttpRequestExceptionEx e)
            {
                await _dialogService.ShowDialog(e.HttpCode + "\n" + e.Message, "HttpRequestExceptionEx", "OK");
            }
            catch (Exception)
            {
                //ignore
            }
        }
        private async Task CheckMachineState()
        {
            try
            {
                var machine = await _masiniService.GetMachineStateAsync(_settingsService.MachineIdSettings);
                if (machine) return;
                await UpdateJobLastWrite(false);
                await _navigationService.NavigateToAsync<HomeViewModel>();
                await _dialogService.ShowDialog("Only one machine can be in use", " Machine is deactivated!", "OK");
            }
            catch (HttpRequestExceptionEx exception)
            {
                _dialogService.ShowToast(exception.Message +
                   "MachineInUse:HttpRequestExceptionEx");
            }
            catch (Exception)
            {
                //ignore
            }
        }
        private async void OnStopJobCommand(object obj)
        {
            var action = await _dialogService.ShowConfirmationDialog("Stop current job",
                "Do you want to stop your current job?", "Yes", "No");
            if (!action) return;
            var dialog = _dialogService.ShowProgressDialog("Please Wait...");
            try
            {
                dialog.Show();
                await UpdateJobLastWrite(true);
                dialog.Hide();
            }
            catch (HttpRequestExceptionEx e)
            {
                await _dialogService.ShowDialog(
                    $"{e.HttpCode} : {e.Message}",
                    "OnStopJobCommand:HttpRequestExceptionEx", "OK");
            }
            catch (Exception)
            {
                //ignore
            }
        }
        private async void OnStopStopwatchCommand(object obj)
        {
            try
            {
                var action = await _dialogService.ShowConfirmationDialog("Leaving pause", "Are you sure?", "Yes", "No");
                if (!action) return;
                _pause.EndPause = await _jobService.GetServerDateTime();
                CanShowStopwatchPopup = false;
                await _pauseService.AddPause(_pause);
                _pause = null;
            }
            catch (HttpRequestExceptionEx e)
            {
                await _dialogService.ShowDialog(e.HttpCode.ToString(), "HttpRequestExceptionEx:OnStopStopwatchCommand", "OK");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
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
        private async Task StartStopwatch()
        {
            Stopwatch stopwatch = new Stopwatch();
            CanShowStopwatchPopup = true;
            Device.StartTimer(
                TimeSpan.FromSeconds(1), () =>
            {
                stopwatch.Start();
                if (CanShowStopwatchPopup)
                {
                    int elapsedSec = (int)stopwatch.ElapsedMilliseconds / 1000 % 60;
                    int elapsedMin = (int)stopwatch.ElapsedMilliseconds / 1000 / 60;
                    StopwatchTime = "Elapsed Time\n" + elapsedMin + "m " + elapsedSec + "s";
                    return true;
                }
                stopwatch.Reset();
                return false;
            });
            await Task.Delay(100);
        }
        //private async Task UpdateJobFirstWrite(DateTime firstClick)
        //{
        //    try
        //    {
        //        if (Counter == 1)
        //        {
        //            var jobId = _settingsService.JobIdSettings;
        //            var job = await _jobService.GetJob(jobId);
        //            job.FirstWrite = firstClick;
        //            await _jobService.UpdateJob(jobId, job);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}
        private async Task UpdateJobLastWrite(bool isStop)
        {
            try
            {
                var job = await _jobService.GetJob(_settingsService.JobIdSettings);
                var machine = await _masiniService.GetMachineAsync(_settingsService.MachineIdSettings);
                var lastWrite = await _jobService.GetLastClickForJob(_settingsService.JobIdSettings);
                var currentTime = await _jobService.GetServerDateTime();
                if (lastWrite == DateTime.Parse("11/11/2011 12:00:00 AM"))
                {
                    job.LastWrite = currentTime;
                    job.Closed = currentTime;
                }
                else if (DateTime.Parse(_settingsService.UserLoginDateSettings).Day != currentTime.Day)
                {
                    job.LastWrite = lastWrite;
                    job.Closed = lastWrite;
                }
                else
                {
                    job.LastWrite = lastWrite;
                    job.Closed = currentTime;
                }
                await _jobService.UpdateJob(job.Id.ToString(), job);
                //await AddEfficiencyForJob(job);

                if (machine.Active && isStop)
                {
                    MasiniForUpdate machineToUpdate = new MasiniForUpdate()
                    {
                        Id = machine.Id,
                        Occupied = false,
                        Active = false
                    };
                    await _masiniService.UpdateMachineActivity(machineToUpdate, machineToUpdate.Id);
                }

                _settingsService.JobIdSettings = null;
                _settingsService.JobNormSettings = null;
                _settingsService.CounterSettings = null;
                _settingsService.OneClickWorthSettings = null;
                _settingsService.IsNormCalculatedSettings = null;
                _settingsService.TotalEfficiencySettings = null;
                _settingsService.SelectedPhaseSettings = null;
                _settingsService.LastClickSetting = null;
                // MessagingCenter.Send(this, "disconnectFromBT");
                await _navigationService.NavigateToAsync<HomeViewModel>();
            }
            catch (Exception e)
            {
                await _dialogService.ShowDialog(e.Message, "Exception:UpdateJobLastWrite"+e, "OK");
            }
        }
        private async Task WeightedAverage(double normInMinutes, DateTime clickTime)
        {
            try
            {
                var norm = _settingsService.JobNormSettings.ToInteger();
                var normHours = await NormForHours(_settingsService.JobNormSettings.ToInteger(), clickTime);
                var finishedJobsIds = _settingsService.EfficiencyIdsSettings.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                if (finishedJobsIds.Length >= 1)
                {
                    var jobEfficiencies = new List<JobEfficiency>();
                    var previousJobs = new List<Job>();
                    //var pauses = new List<Pause>();
                    var previousJobsIds = _settingsService.JobsIdSettings.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 0; i < finishedJobsIds.Length; i++)
                    {
                        jobEfficiencies.Add(await _jobEfficiency.GetJobEfficiency(finishedJobsIds[i]));
                    }
                    for (int i = 0; i < previousJobsIds.Length; i++)
                    {
                        previousJobs.Add(await _jobService.GetJob(previousJobsIds[i]));
                    }
                    var weightedAverage = 0.0;
                    var firstJob = previousJobs.FirstOrDefault();
                    var totalTime = await _jobService.GetServerDateTime() - firstJob?.FirstWrite;
                    foreach (var job in jobEfficiencies.Where(job => job.SpentTime != 0))
                    {
                        if (totalTime != null)
                            weightedAverage += TimeSpan.FromTicks(job.SpentTime).TotalMinutes / totalTime.Value.TotalMinutes * job.Efficiency;
                    }
                    EfficiencyTotal = weightedAverage;
                }
                else
                {
                    EfficiencyTotal = _counter / normHours * 100f;
                }
                EfficiencyCurrent = _settingsService.LastClickSetting != string.Empty ?
                   TimeSpan.FromMinutes(normInMinutes).TotalSeconds / clickTime.Subtract(DateTime.Parse(_settingsService.LastClickSetting)).TotalSeconds * 100f
                    : 100;
                EfficiencyHour = (double)HourCounter / norm * 100f;
                if (EfficiencyCurrent > 0 && EfficiencyCurrent < 70)
                {
                    BackgroundColorButton = Color.Crimson;
                }
                else if (EfficiencyCurrent >= 70 && EfficiencyCurrent < 89)
                {
                    BackgroundColorButton = Color.Yellow;
                }
                else if (EfficiencyCurrent >= 89 && EfficiencyCurrent < 200)
                {
                    BackgroundColorButton = Color.ForestGreen;
                }
                //Bluetooth Colors
                //if (EfficiencyHour > 0 && EfficiencyHour < 70)
                //{
                //    MessagingCenter.Send(this, "red");
                //}
                //else if (EfficiencyHour >= 70 && EfficiencyHour < 89)
                //{
                //    MessagingCenter.Send(this, "yellow");
                //}
                //else if (EfficiencyHour >= 89 && EfficiencyHour < 200)
                //{
                //    MessagingCenter.Send(this, "green");
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        #region Button Switch Methods
        private void SelectOtherSwitch()
        {
            TechnicalProblemsSwitch = false;
            Pause1Switch = false;
            WcSwitch = false;
            Pause2Switch = false;
        }
        private void SelectPause1Switch()
        {
            OtherSwitch = false;
            Pause2Switch = false;
            TechnicalProblemsSwitch = false;
            WcSwitch = false;
        }
        private void SelectPause2Switch()
        {
            OtherSwitch = false;
            Pause1Switch = false;
            TechnicalProblemsSwitch = false;
            WcSwitch = false;
        }
        private void SelectTechnicalProblemsSwitch()
        {
            OtherSwitch = false;
            Pause1Switch = false;
            WcSwitch = false;
            Pause2Switch = false;
        }
        private void SelectWcSwitch()
        {
            OtherSwitch = false;
            Pause1Switch = false;
            TechnicalProblemsSwitch = false;
            Pause2Switch = false;
        }
        #endregion
        #region Properties
        private bool _workPermitOverQuantity;
        #region StopWatch Properties

        private bool _canShowStopwatchPopup;

        private string _pauseType;

        private string _stopwatchTime;

        public bool CanShowStopwatchPopup
        {
            get { return _canShowStopwatchPopup; }
            set
            {
                _canShowStopwatchPopup = value;
                OnPropertyChanged();
            }
        }
        public string PauseType
        {
            get { return _pauseType; }
            set
            {
                _pauseType = value;
                OnPropertyChanged();
            }
        }
        public string StopwatchTime
        {
            get { return _stopwatchTime; }
            set
            {
                _stopwatchTime = value;
                OnPropertyChanged();
            }
        }


        #endregion
        #region Pause Switch Properties

        private bool _otherSwitch;
        private bool _pause1Switch;
        private bool _pause2Switch;
        private bool _technicalProblemsSwitch;
        private bool _wcSwitch;


        public bool OtherSwitch
        {
            get { return _otherSwitch; }
            set
            {
                _otherSwitch = value;
                if (value)
                {
                    SelectOtherSwitch();
                }
                OnPropertyChanged();
            }
        }

        public bool Pause1Switch
        {
            get { return _pause1Switch; }
            set
            {
                _pause1Switch = value;
                if (value)
                {
                    SelectPause1Switch();
                }
                OnPropertyChanged();
            }
        }
        public bool Pause2Switch
        {
            get { return _pause2Switch; }
            set
            {
                _pause2Switch = value;
                if (value)
                {
                    SelectPause2Switch();
                }
                OnPropertyChanged();
            }
        }
        public bool TechnicalProblemsSwitch
        {
            get { return _technicalProblemsSwitch; }
            set
            {
                _technicalProblemsSwitch = value;
                if (value)
                {
                    SelectTechnicalProblemsSwitch();
                }
                OnPropertyChanged();
            }
        }

        public bool WcSwitch
        {
            get { return _wcSwitch; }
            set
            {
                _wcSwitch = value;
                if (value)
                {
                    SelectWcSwitch();
                }
                OnPropertyChanged();
            }
        }
        #endregion
        #region Hours Properties

        #endregion
        #region Shift Properties

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


        #endregion
        #region Popup Properties

        private bool _canShowPausePopup;

        private bool _canShowStopPopup;

        public bool CanShowPausePopup
        {
            get { return _canShowPausePopup; }
            set
            {
                _canShowPausePopup = value;
                OnPropertyChanged();
            }
        }
        public bool CanShowStopPopup
        {
            get { return _canShowStopPopup; }
            set
            {
                _canShowStopPopup = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Click Button Properties
        private double _animationDuration;
        private bool _isBusyIndicator;
        private bool _isButtonEnabled=true;

        public double AnimationDuration
        {
            get { return _animationDuration; }
            set
            {
                _animationDuration = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusyIndicator
        {
            get { return _isBusyIndicator; }
            set
            {
                _isBusyIndicator = value;
                OnPropertyChanged();
            }
        }

        public bool IsButtonEnabled
        {
            get { return _isButtonEnabled; }
            set
            {
                _isButtonEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion
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
        #region Efficiency Properties
        private double _efficiencyCurrent;
        private double _efficiencyHour;
        private double _efficiencyTotal;
        private string _norm;
        public double EfficiencyCurrent
        {
            get { return _efficiencyCurrent;}
            set
            {
                _efficiencyCurrent = value;
                OnPropertyChanged();
            }
        }
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
        public double EfficiencyTotal
        {
            get { return _efficiencyTotal; }
            set
            {
                _efficiencyTotal = value;
                OnPropertyChanged();
            }
        }
        public string Norm
        {
            get { return _norm; }
            set
            {
                _norm = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Counters Properties
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
        public int HourCounter { get; set; }
        #endregion
        private Color _backgroundColorButton = Color.ForestGreen;
        public Color BackgroundColorButton
        {
            get { return _backgroundColorButton; }
            set
            {
                if (value == _backgroundColorButton)
                    return;
                _backgroundColorButton = value;
                OnPropertyChanged();
            }
        }
        //private readonly EfficiencyForHours EfficiencyForHours;
        // private readonly Hour Hours;
        private int _totalPieces;
        public int TotalPieces
        {
            get => _totalPieces;
            set
            {
                _totalPieces = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}

