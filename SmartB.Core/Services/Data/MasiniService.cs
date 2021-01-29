using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using SmartB.Core.Constants;
using SmartB.Core.Contracts.Repository;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Models;
namespace SmartB.Core.Services.Data
{
    public class MasiniService : BaseService, IMasiniService
    {
        private IGenericRepository _genericRepository;
        public MasiniService(IGenericRepository genericRepository, IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }
        public async Task<IEnumerable<Masini>> GetMachinesAsync(int sectorId, string line)
        {
            List<Masini> machinesFromCache = await GetFromCache<List<Masini>>(CacheNameConstants.AllMachines);
            if (machinesFromCache != null)
            {
                return machinesFromCache;
            }
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.MachinesEndpoint}/sector={sectorId}/line={line}"
            };
            var machines =  await _genericRepository.GetAsync<IEnumerable<Masini>>(builder.ToString());
            var machinesAsync = machines as Masini[] ?? machines.ToArray();
            await Cache.InsertObject(CacheNameConstants.AllMachines, machinesAsync, DateTimeOffset.Now.AddSeconds(30));
            return machinesAsync;
        }
        public async  Task<bool> GetMachineStateAsync(string id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path =$"{ApiConstants.MachinesEndpoint}/active={id}"
            };
            return await _genericRepository.GetAsync<bool>(builder.ToString());
        }
        public async Task<Masini> GetMachineAsync(string id)
        {
           UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
           {
               Path = $"{ApiConstants.MachinesEndpoint}/{id}"
           };
           return await _genericRepository.GetAsync<Masini>(builder.ToString());
        }
        public async Task UpdateMachineActivity(MasiniForUpdate machineToUpdate)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.UpdateMachineEndpoint}"
            };
            await _genericRepository.PutAsync(builder.ToString(), machineToUpdate);
        }
    }
}
