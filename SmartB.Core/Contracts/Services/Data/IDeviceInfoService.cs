using System.Threading.Tasks;
using SmartB.Core.Models;

namespace SmartB.Core.Contracts.Services.Data
{
    public interface IDeviceInfoService
    {
        Task<DeviceInfo> GetDeviceInfo(string deviceInfoId);
        Task<DeviceInfo> AddDeviceInfo(DeviceInfo deviceInfoToAdd);
        Task<DeviceInfo> UpdateDeviceInfo(DeviceInfo deviceInfoToUpdate, int id);
    }
}
