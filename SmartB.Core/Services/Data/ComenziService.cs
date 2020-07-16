using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using SmartB.Core.Constants;
using SmartB.Core.Contracts.Repository;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Models;
namespace SmartB.Core.Services.Data
{
    public class ComenziService : BaseService, IComenziService
    {
        private IGenericRepository _genericRepository;
        public ComenziService(IGenericRepository genericRepository, IBlobCache cache=null): base(cache)
        {
            _genericRepository = genericRepository;
        }
        public async Task<IEnumerable<Comenzi>> GetOrdersAsync(int sectorId)
        {
            List<Comenzi> ordersFromCache = await GetFromCache<List<Comenzi>>(CacheNameConstants.AllOrders);
            if (ordersFromCache != null)
            {
                return ordersFromCache;
            }
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.OrdersEndpoint}/sector={sectorId}"
            };
            var orders = await _genericRepository.GetAsync<IEnumerable<Comenzi>>(builder.ToString());
            await Cache.InsertObject(CacheNameConstants.AllOrders, orders, DateTimeOffset.Now.AddSeconds(60));
            return orders;
        }
        public async Task<Comenzi> GetOrderAsync(int id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.OrdersEndpoint}/{id}"
            };
            return await _genericRepository.GetAsync<Comenzi>(builder.ToString());
        }
        public async Task<Comenzi> GetOrderWithName(string commessa)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.OrdersEndpoint}/{commessa}"
            };
            return await _genericRepository.GetAsync<Comenzi>(builder.ToString());
        }
    }
}
