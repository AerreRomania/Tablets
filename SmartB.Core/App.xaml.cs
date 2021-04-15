using System;
using System.Threading.Tasks;
using SmartB.Core.Bootstrap;
using SmartB.Core.Contracts.Services.General;
using Xamarin.Forms;

namespace SmartB.Core
{
	public partial class App : Application
	{
		public App ()
		{
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTUxNzYyQDMxMzcyZTMzMmUzMFhqTklKZUdOdkE5TG0rcEFnUDJma05wSjFFNG9sK014NGpRSm5YM0FzeE09");
            InitializeComponent();
            InitalizeApp();

            AppContainer.Resolve<INavigationService>().InitializeAsync();
        }

        private void InitalizeApp()
        {
            AppContainer.RegisterDependencies();

        }

        //private async Task InitializeNavigation()
        //{
        //    var navigationService = AppContainer.Resolve<INavigationService>();
        //    await navigationService.InitializeAsync();
        //}
       
        protected override void OnStart()
        {
           
            // Handle when your app starts
        }
       
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}