using System;
using System.Threading.Tasks;
using Akavache;
using SmartB.Core.Constants;
using SmartB.Core.Contracts.Repository;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Models;

namespace SmartB.Core.Services.Data
{
    public  class ClickService : BaseService , IButoaneService
    {
        private IGenericRepository _genericRepository;
        public ClickService(IGenericRepository genericRepository,IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<Click> GetClick(string clickId)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.ClickEndpoint}/{clickId}"
            };

            return await _genericRepository.GetAsync<Click>(builder.ToString());
        }

        public async Task<Click> AddClick(Click clickToAdd)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AddClickEndpoint
            };

            return await _genericRepository.PostAsync(builder.ToString(), clickToAdd);
        }
    }
}
