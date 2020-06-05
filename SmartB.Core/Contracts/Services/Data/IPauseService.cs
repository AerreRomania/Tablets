using System.Collections.Generic;
using System.Threading.Tasks;
using SmartB.Core.Models;

namespace SmartB.Core.Contracts.Services.Data
{
    public interface IPauseService
    {
        Task<Pause> GetPause(string jobId);
        Task<IEnumerable<Pause>> GetPauses(string jobId);
        Task<Pause> AddPause(Pause pauseToAdd);
    }
}
