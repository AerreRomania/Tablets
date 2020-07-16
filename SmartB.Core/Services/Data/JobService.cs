using System;
using System.Threading.Tasks;
using Akavache;
using SmartB.Core.Constants;
using SmartB.Core.Contracts.Repository;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Models;
namespace SmartB.Core.Services.Data
{
    public class JobService : BaseService, IJobDataService
    {
        private IGenericRepository _genericRepository;
        public JobService(IGenericRepository genericRepository, IBlobCache cache=null) : base(cache)
        {
            _genericRepository = genericRepository;
        }
        public async Task<Job> GetJob(string jobId)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.JobEndpoint}/{jobId}"
            };
            return await _genericRepository.GetAsync<Job>(builder.ToString());
        }
        public async Task<DateTime> GetLastClickForJob(string id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.JobEndpoint}/last={id}"
            };
            return await _genericRepository.GetAsync<DateTime>(builder.ToString());
        }
        public async Task<DateTime> GetServerDateTime()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.JobEndpoint}/date"
            };
            return await _genericRepository.GetAsync<DateTime>(builder.ToString());
        }
        public async Task<Job> AddJob(Job jobToAdd)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AddJobEndpoint
            };
           return await _genericRepository.PostAsync(builder.ToString(), jobToAdd);
        }
        public async Task<Job> UpdateJob(string id, Job jobToUpdate)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.UpdateJobEndpoint}/{id}"
            };
            return await _genericRepository.PutAsync(builder.ToString(), jobToUpdate);
        }
    }
}
