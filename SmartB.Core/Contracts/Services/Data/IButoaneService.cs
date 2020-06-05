using System.Threading.Tasks;
using SmartB.Core.Models;

namespace SmartB.Core.Contracts.Services.Data
{
    public interface IButoaneService
    {
        Task<Click> GetClick(string clickId);
        Task<Click> AddClick(Click clickToAdd);
    }
}
