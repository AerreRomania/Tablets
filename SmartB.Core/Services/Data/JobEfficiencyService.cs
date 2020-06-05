using System;
using System.Threading.Tasks;
using SmartB.Core.Constants;
using SmartB.Core.Contracts.Repository;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Models;

namespace SmartB.Core.Services.Data
{
    public class JobEfficiencyService : IJobEfficiencyService
    {
        private IGenericRepository _genericRepository;

        public JobEfficiencyService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<JobEfficiency> GetJobEfficiency(string id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.JobEfficiencyEndpoint}/{id}"
            };

            return await _genericRepository.GetAsync<JobEfficiency>(builder.ToString());
        }

        public async Task<JobEfficiency> AddJobEfficiency(JobEfficiency jobEfficiencyToAdd)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AddJobEfficiencyEndpoint
            };

            return await _genericRepository.PostAsync(builder.ToString(), jobEfficiencyToAdd);
        }
    }
}
