using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartB.Core.Constants;
using SmartB.Core.Contracts.Repository;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Models;
namespace SmartB.Core.Services.Data
{
    public class PauseService : IPauseService
    {
        private IGenericRepository _genericRepository;
        public PauseService(IGenericRepository genericRepository) 
        {
            _genericRepository = genericRepository;
        }
        public async Task<Pause> GetPause(string jobId)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.PauseEndpoint}/{jobId}"
            };
            return await _genericRepository.GetAsync<Pause>(builder.ToString());
        }
        public async Task<Pause> AddPause(Pause pauseToAdd)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AddPauseEndpoint
            };
            return await _genericRepository.PostAsync(builder.ToString(), pauseToAdd);
        }
        public async Task<IEnumerable<Pause>> GetPauses(string jobId)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.PausesEndpoint}/job={jobId}"
            };
            return await _genericRepository.GetAsync<IEnumerable<Pause>>(builder.ToString());
        }
    }
}
