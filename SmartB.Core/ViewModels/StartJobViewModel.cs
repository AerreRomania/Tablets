using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Contracts.Services.General;
using SmartB.Core.ViewModels.Base;
using Xamarin.Forms;
namespace SmartB.Core.ViewModels
{
    public class StartJobViewModel : ViewModelBase
    {
        private IJobDataService _jobDataService;
        private ISettingsService _settingsService;
        private IMasiniService _masiniService;
        public StartJobViewModel(IJobDataService jobDataService,
                                 ISettingsService settingsService, 
                                 IMasiniService masiniService,
                                 IConnectionService connectionService, 
                                 INavigationService navigationService, 
                                 IDialogService dialogService) : base(connectionService, navigationService, dialogService)
                                {
                                    _jobDataService = jobDataService;
                                    _settingsService = settingsService;
                                    _masiniService = masiniService;
                                }
        public ICommand StartJob => new Command(OnStartJobCommand);
        public ICommand CancelJob => new Command(OnCancelJobCommand);
        public string Machine => _settingsService.MachineCodeSettings;
        public string Phase => _settingsService.SelectedPhaseSettings;
        public string Order => _settingsService.CommessaFromBarcode;
        private async void OnCancelJobCommand()
        {
            await CloseJob();
        }
        private async void OnStartJobCommand()
        {
            await FirstWrite();
        }
        private async Task FirstWrite()
        {
            try
            {
                var dialog = _dialogService.ShowProgressDialog("Please wait...");
                dialog.Show();
                var currentTime = await _jobDataService.GetServerDateTime();
                var jobId = _settingsService.JobIdSettings;
                var job = await _jobDataService.GetJob(jobId);
                job.FirstWrite = currentTime;
                await _jobDataService.UpdateJob(jobId, job);
                dialog.Hide();
                await _navigationService.NavigateToAsync<JobViewModel>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private async Task CloseJob()
        {
            try
            {
                var dialog = _dialogService.ShowProgressDialog("Please wait...");
                dialog.Show();
                var job = await _jobDataService.GetJob(_settingsService.JobIdSettings);
                var machine = await _masiniService.GetMachineAsync(_settingsService.MachineIdSettings);
                var currentTime = await _jobDataService.GetServerDateTime();
                job.FirstWrite = currentTime;
                job.LastWrite = currentTime;
                job.Closed = currentTime;
                await _jobDataService.UpdateJob(job.Id.ToString(), job);
                if (machine.Active)
                {
                    machine.Active = false;
                    await _masiniService.UpdateMachineActivity(machine.Id, machine);
                }
                _settingsService.JobIdSettings = null;
                _settingsService.JobNormSettings = null;
                _settingsService.CounterSettings = null;
                _settingsService.OneClickWorthSettings = null;
                _settingsService.IsNormCalculatedSettings = null;
                _settingsService.TotalEfficiencySettings = null;
                _settingsService.SelectedPhaseSettings = null;
                _settingsService.LastClickSetting = null;
                dialog.Hide();
                await _navigationService.NavigateBackAsync();
            }
            catch (Exception e)
            {
                await _dialogService.ShowDialog(e.Message, "Exception:UpdateJobLastWrite", "OK");
            }
        }
    }
}
