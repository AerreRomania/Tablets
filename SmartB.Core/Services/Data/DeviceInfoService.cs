using System;
using System.Threading.Tasks;
using Akavache;
using SmartB.Core.Constants;
using SmartB.Core.Contracts.Repository;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Models;
namespace SmartB.Core.Services.Data
{
    public class DeviceInfoService : BaseService , IDeviceInfoService
    {
        private IGenericRepository _genericRepository;
        public DeviceInfoService(IGenericRepository genericRepository, IBlobCache cache=null) : base(cache)
        {
            _genericRepository = genericRepository;
        }
        public async Task<DeviceInfo> GetDeviceInfo(string deviceInfoId)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.DeviceInfoEndpoint}/{deviceInfoId}"
            };
            return await _genericRepository.GetAsync<DeviceInfo>(builder.ToString());
        }
        public async Task<DeviceInfo> AddDeviceInfo(DeviceInfo deviceInfoToAdd)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AddDeviceInfoEndpoint
            };
            return await _genericRepository.PostAsync(builder.ToString(), deviceInfoToAdd);
        }
        public async Task<DeviceInfo> UpdateDeviceInfo(DeviceInfo deviceInfoToUpdate, int id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.UpdateDeviceInfoEndpoint}/{id}"
            };
            return await _genericRepository.PutAsync(builder.ToString(), deviceInfoToUpdate);
        }
    }
}
