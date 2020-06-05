using System.Threading.Tasks;
using SmartB.Core.Models;

namespace SmartB.Core.Contracts.Services.Data
{
    public interface IJobEfficiencyService
    {
        Task<JobEfficiency> GetJobEfficiency(string id);
        Task<JobEfficiency> AddJobEfficiency(JobEfficiency jobEfficiencyToAdd);
    }
}
