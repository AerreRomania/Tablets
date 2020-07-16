using System.Collections.Generic;
using System.Threading.Tasks;
using SmartB.Core.Models;
namespace SmartB.Core.Contracts.Services.Data
{
    public interface IPhaseService
    {
        Task<IEnumerable<Phases>> GetPhasesAsync(int articleId, string machineName);
    }
}
