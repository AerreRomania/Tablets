namespace SmartB.Core.Constants
{
    public class ApiConstants
    {
        public ApiConstants()
        {
        }
        public const string BaseApiUrl = "http://192.168.96.37:47001/";
       // public const string BaseApiUrl = "http://192.168.96.37:47002/";
        //public const string BaseApiUrl = "http://192.168.8.100:5000/";

        public const string UsersEndpoint = "api/users/names";
        public const string UpdateUserEndpoint = "api/users";
        public const string UserEndpoint = "api/users";

        public const string AuthenticateEndpoint = "api/authentication";

        public const string MachinesEndpoint = "api/machines";
        public const string UpdateMachineEndpoint = "api/machines";

        public const string OrdersEndpoint = "api/orders";

        public const string ArticleEndpoint = "api/article"; 

        public const string JobEndpoint = "api/job";
        public const string AddJobEndpoint = "api/job";
        public const string UpdateJobEndpoint = "api/job";

        public const string GetOrderFromTim = "api/tim";

        public const string ClickEndpoint = "api/click";
        public const string AddClickEndpoint = "api/click";

        public const string PauseEndpoint = "api/pause";
        public const string PausesEndpoint = "api/pause";
        public const string AddPauseEndpoint = "api/pause";

        public const string JobEfficiencyEndpoint = "api/efficiency";
        public const string AddJobEfficiencyEndpoint = "api/efficiency";

        public const string DeviceEndpoint = "api/device";
        public const string AddDeviceEndpoint = "api/device";
        public const string UpdateDeviceEndpoint = "api/device";

        public const string DeviceInfoEndpoint = "api/deviceinfo";
        public const string AddDeviceInfoEndpoint = "api/deviceinfo";
        public const string UpdateDeviceInfoEndpoint = "api/deviceinfo";
    }   
}
