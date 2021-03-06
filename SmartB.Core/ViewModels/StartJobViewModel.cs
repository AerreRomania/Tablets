﻿using System;
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
            var dialog = _dialogService.ShowProgressDialog("Please wait...");
            dialog.Show();
            if (await _connectionService.CheckConnection())
            {
               
                await CloseJob();
                dialog.Hide();
            }
            else
            {
                await _dialogService.ShowDialog(
                    "Connection problem please connect your device to WIFI and try again.",
                    "Internet connection problem",
                    "OK");
                dialog.Hide();
            }
        }
        private async void OnStartJobCommand()
        {
            var dialog = _dialogService.ShowProgressDialog("Please wait...");
            dialog.Show();
            if (await _connectionService.CheckConnection())
            {
               
                await FirstWrite();
                dialog.Hide();
            }
            else
            {
                await _dialogService.ShowDialog(
                    "Connection problem please connect your device to WIFI and try again.",
                    "Internet connection problem",
                    "OK");
                dialog.Hide();
            }
        }
        private async Task FirstWrite()
        {
            try
            {
               
                if(_settingsService.UserLineSettings != _settingsService.MachineLineSettings)
                {
                    await _dialogService.ShowDialog("You can work only in your line.", "Warning", "OK");
                    return;
                }
                
                var currentTime = await _jobDataService.GetServerDateTime();
                var machine = await _masiniService.GetMachineAsync(_settingsService.MachineIdSettings);
                var jobId = _settingsService.JobIdSettings;
                var job = await _jobDataService.GetJob(jobId);
                job.FirstWrite = currentTime;
                Models.MasiniForUpdate machineToUpdate = new Models.MasiniForUpdate()
                {
                    Id = machine.Id,
                    Occupied = false,
                    Active=true
                };
                await _masiniService.UpdateMachineActivity(machineToUpdate,machineToUpdate.Id);
                await _jobDataService.UpdateJob(jobId, job);
                
                await _navigationService.NavigateToAsync<JobViewModel>();
               
            }
            catch (Exception e)
            {
                await _dialogService.ShowDialog(e.Message, "Exception:"+e.InnerException, "OK");
            }
        }
        private async Task CloseJob()
        {
            try
            {
                
                var job = await _jobDataService.GetJob(_settingsService.JobIdSettings);
                var machine = await _masiniService.GetMachineAsync(_settingsService.MachineIdSettings);
                var currentTime = await _jobDataService.GetServerDateTime();
                job.FirstWrite = currentTime;
                job.LastWrite = currentTime;
                job.Closed = currentTime;
                await _jobDataService.UpdateJob(job.Id.ToString(), job);

                //Models.MasiniForUpdate machineToUpdate = new Models.MasiniForUpdate()
                //{
                //    Id = machine.Id,
                //    Occupied = false,
                //    Active = false
                //};
                //await _masiniService.UpdateMachineActivity(machineToUpdate, machine.Id);

                //if (machine.Active)
                //{
                //    machine.Active = false;
                //    await _masiniService.UpdateMachineActivity(machine.Id, machine);
                //} //last update


                _settingsService.JobIdSettings = null;
                _settingsService.JobNormSettings = null;
                _settingsService.CounterSettings = null;
                _settingsService.OneClickWorthSettings = null;
                _settingsService.IsNormCalculatedSettings = null;
                _settingsService.TotalEfficiencySettings = null;
                _settingsService.SelectedPhaseSettings = null;
                _settingsService.LastClickSetting = null;
               
                await _navigationService.NavigateBackAsync();
            }
            catch (Exception e)
            {
                await _dialogService.ShowDialog(e.Message, "Exception:UpdateJobLastWrite", "OK");
            }
        }
    }
}
