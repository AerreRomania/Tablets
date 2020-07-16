using System.Threading.Tasks;
using Plugin.Connectivity.Abstractions;
namespace SmartB.Core.Contracts.Services.General
{
    public interface IConnectionService
    {
        bool IsConnected { get; }
        Task<bool> CheckConnection();
        event ConnectivityChangedEventHandler ConnectivityChanged;
    }
}
