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
    public class UsersDataService : BaseService, IUsersDataService
    {
        private IGenericRepository _genericRepository;
        public UsersDataService(IGenericRepository genericRepository,IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }
        public async Task<IEnumerable<string>> GetAllUserNames()
        {
            List<string> usersFromCache = await GetFromCache<List<string>>(CacheNameConstants.AllUsers);
            if (usersFromCache != null) //loaded from cache
            {
                return usersFromCache;
            }
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.UsersEndpoint
            };
            var userNames = await _genericRepository.GetAsync<List<string>>(builder.ToString());
            await Cache.InsertObject(CacheNameConstants.AllUsers, userNames, DateTimeOffset.Now.AddSeconds(60));
            return userNames;
        }
        public async Task<bool> GetUserStateAsync(string id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.UserEndpoint}/active={id}"
            };
            return await _genericRepository.GetAsync<bool>(builder.ToString());
        }
        public async Task<Angajati> GetUser(string id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.UserEndpoint}/{id}"
            };
            return await _genericRepository.GetAsync<Angajati>(builder.ToString());
        }
        public async Task<Angajati> UpdateUserActivity(string id, Angajati userToUpdate)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.UpdateUserEndpoint}/{id}"
            };
            return await _genericRepository.PutAsync(builder.ToString(), userToUpdate);
        }
        public async Task<bool> GetManagerAsync(string pin)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.UserEndpoint}/p={pin}"
            };
            return await _genericRepository.GetAsync<bool>(builder.ToString());
        }
    }
}