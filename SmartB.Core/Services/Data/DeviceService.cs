using System;
using System.Threading.Tasks;
using Akavache;
using SmartB.Core.Constants;
using SmartB.Core.Contracts.Repository;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Models;

namespace SmartB.Core.Services.Data
{
    public class DeviceService : BaseService , IDeviceDataService
    {
        private IGenericRepository _genericRepository;

        public DeviceService(IGenericRepository genericRepository,IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<Device> GetDevice(string deviceId)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.DeviceEndpoint}/{deviceId}"
            };

            return await _genericRepository.GetAsync<Device>(builder.ToString());
        }

        public async Task<Device> GetDeviceWithSN(string serial)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.DeviceEndpoint}/sn={serial}"
            };

            return await _genericRepository.GetAsync<Device>(builder.ToString());
        }

        public async Task<Device> AddDevice(Device deviceToAdd)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AddDeviceEndpoint
            };

            return await _genericRepository.PostAsync(builder.ToString(), deviceToAdd);
        }

        public async Task<Device> UpdateDevice(Device deviceToUpdate, string id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.UpdateDeviceEndpoint}/{id}"
            };

            return await _genericRepository.PutAsync(builder.ToString(), deviceToUpdate);
        }
    }
}
