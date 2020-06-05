using Autofac;
using SmartB.Core.ViewModels;
using System;
using SmartB.Core.Contracts.Repository;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Contracts.Services.General;
using SmartB.Core.Repository;
using SmartB.Core.Services.Data;
using SmartB.Core.Services.General;

namespace SmartB.Core.Bootstrap
{
    public class AppContainer
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //ViewModels
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<MenuViewModel>();
            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<StartJobViewModel>();
            builder.RegisterType<StartManichinoViewModel>();
            builder.RegisterType<ScannerViewModel>();
            builder.RegisterType<JobViewModel>();
            builder.RegisterType<ManichinoViewModel>();

            //services - data
            builder.RegisterType<UsersDataService>().As<IUsersDataService>();
            builder.RegisterType<MasiniService>().As<IMasiniService>();
            builder.RegisterType<ComenziService>().As<IComenziService>();
            builder.RegisterType<ArticoleService>().As<IArticoleService>();
            builder.RegisterType<PhaseService>().As<IPhaseService>();
            builder.RegisterType<JobService>().As<IJobDataService>();
            builder.RegisterType<CommessaTimService>().As<ICommessaTimService>();
            builder.RegisterType<ClickService>().As<IButoaneService>();
            builder.RegisterType<PauseService>().As<IPauseService>();
            builder.RegisterType<JobEfficiencyService>().As<IJobEfficiencyService>();
            builder.RegisterType<DeviceService>().As<IDeviceDataService>();
            builder.RegisterType<DeviceInfoService>().As<IDeviceInfoService>();

            //services - general
            builder.RegisterType<ConnectionService>().As<IConnectionService>();
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();

            //General
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();

            _container = builder.Build();
        }

        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }

}
