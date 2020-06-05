using System;
using SmartB.Core.Contracts.Services.General;
using SmartB.Core.Models;
using SmartB.Core.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Enumerations;

using Xamarin.Forms;

namespace SmartB.Core.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private ObservableCollection<MainMenuItem> _menuItems;
        private ISettingsService _settingsService;
        private IUsersDataService _userDataService;
        private IDeviceDataService _deviceDataService;
        public MenuViewModel(IConnectionService connectionService,
            INavigationService navigationService,
            IDialogService dialogService, 
            ISettingsService settingsService,
            IUsersDataService usersDataService, IDeviceDataService deviceDataService) 
            : base(connectionService, navigationService, dialogService)
        {
            _settingsService = settingsService;
            _userDataService = usersDataService;
            _deviceDataService = deviceDataService;
            MenuItems = new ObservableCollection<MainMenuItem>();
            LoadMenuItems();
        }

        public string WelcomeText => " " + _settingsService.UserNameSetting;
        public ICommand MenuItemTappedCommand => new Command(OnMenuItemTapped);
        public ICommand ConnectToOlTablet => new Command(OnConnectOlTablet);
        public ICommand ConnectToOlTablet2 => new Command(OnConnectOlTablet2);

     
        private void OnConnectOlTablet2(object obj)
        {
            if (!_connectionService.IsConnected)
            {
                var dialog = _dialogService.ShowProgressDialog("Reconnecting OLTablet2");
                dialog.Show();
                MessagingCenter.Send(this, "reconnectWifiOL2");
                dialog.Hide();
            }
            else
            {
                _dialogService.ShowToast("Device is already connected.");
            }

        }

        private void OnConnectOlTablet(object obj)
        {
            if (!_connectionService.IsConnected)
            {
                var dialog = _dialogService.ShowProgressDialog("Reconnecting OLTablet");
                dialog.Show();
                MessagingCenter.Send(this, "reconnectWifiOL");
                dialog.Hide();
            }
            else
            {
                _dialogService.ShowToast("Device is already connected.");
            }

        }

        public ObservableCollection<MainMenuItem> MenuItems
        {
            get => _menuItems;
            set
            {
                _menuItems = value;
                OnPropertyChanged();
            }
        }

        private async void OnMenuItemTapped(object menuItemTappedEventArgs)
        {
            if ((menuItemTappedEventArgs as ItemTappedEventArgs)?.Item is MainMenuItem menuItem && menuItem.MenuText == "Log out")
            {
                bool isJobFinished = _settingsService.JobIdSettings == string.Empty;

                if (isJobFinished)
                {
                    try
                    {
                        var dialog = _dialogService.ShowProgressDialog("Logging out... ");

                        dialog.Show();

                        var user = await _userDataService.GetUser(_settingsService.UserIdSetting);

                        if (user.Active)
                        {
                            user.Active = false;
                            await _userDataService.UpdateUserActivity(user.Id.ToString(), user);
                        }

                        //var device = await _deviceDataService.GetDevice(_settingsService.DeviceIdSettings);

                        //if (device.Active)
                        //{
                        //    device.Active = false;
                        //    await _deviceDataService.UpdateDevice(device, _settingsService.DeviceIdSettings);
                        //}


                        _settingsService.RemoveSettings();
                        await _navigationService.ClearBackStack();

                       dialog.Hide();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    var type = menuItem.ViewModelToLoad;
                  await  _navigationService.NavigateToAsync(type);
                }
                else
                {
                    await _dialogService.ShowDialog("Job in not finished, please stop current job and then log out.", "Information", "OK");
                }
            }
        }

        private void LoadMenuItems()
        {
            //MenuItems.Add(new MainMenuItem
            //{
            //    MenuText = "Home",
            //    ViewModelToLoad = typeof(MainViewModel),
            //    MenuItemType = MenuItemType.Home
            //});

            //MenuItems.Add(new MainMenuItem
            //{
            //    MenuText = "type 1"

            //});

            //MenuItems.Add(new MainMenuItem
            //{
            //    MenuText = "type 2"

            //});

            //MenuItems.Add(new MainMenuItem
            //{
            //    MenuText = "type 3"
            //});

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Log out",
                ViewModelToLoad = typeof(LoginViewModel),
                MenuItemType = MenuItemType.Logout
            });
        }
    }
}
