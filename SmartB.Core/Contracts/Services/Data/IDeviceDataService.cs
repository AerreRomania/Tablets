using System.Threading.Tasks;
using SmartB.Core.Models;

namespace SmartB.Core.Contracts.Services.Data
{
    public interface IDeviceDataService
    {
        Task<Device> GetDevice(string deviceId);
        Task<Device> GetDeviceWithSN(string serial);
        Task<Device> AddDevice(Device deviceToAdd);
        Task<Device> UpdateDevice(Device deviceToUpdate, string id);
    }
}
