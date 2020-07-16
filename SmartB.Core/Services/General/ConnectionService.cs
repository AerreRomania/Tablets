using System.Threading.Tasks;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using SmartB.Core.Contracts.Services.General;
namespace SmartB.Core.Services.General
{
    public class ConnectionService : IConnectionService
    {
        private readonly IConnectivity _connectivity;
        public ConnectionService()
        {
            _connectivity = CrossConnectivity.Current;

            _connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            ConnectivityChanged?.Invoke(this, new ConnectivityChangedEventArgs
            {
                IsConnected = e.IsConnected
            });
        }
        public bool IsConnected => _connectivity.IsConnected;
        public async Task<bool> CheckConnection()
        {
            return await _connectivity.IsReachable("http://192.168.96.14:61382/api/article/233");
        }
        public event ConnectivityChangedEventHandler ConnectivityChanged;
    }
}
