using System;
using System.Threading.Tasks;
using SmartB.Core.Models;
namespace SmartB.Core.Contracts.Services.Data
{
    public interface IJobDataService
    {
        Task<Job> GetJob(string jobId);
        Task<Job> AddJob(Job jobToAdd);
        Task<Job> UpdateJob(string id, Job jobToUpdate);
        Task<DateTime> GetLastClickForJob(string id);
        Task<DateTime> GetServerDateTime();
    }
}
