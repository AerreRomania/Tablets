using SmartB.Core.Models;
using System.Threading.Tasks;

namespace SmartB.Core.Contracts.Services.Data
{
    public interface IArticoleService
    {
        Task<Articole> GetArticleAsync(int id);
    }
}