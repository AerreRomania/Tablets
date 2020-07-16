using System.Threading.Tasks;
using SmartB.Core.Models;
namespace SmartB.Core.Contracts.Services.Data
{
    public interface ICommessaTimService
    {
        Task<CommessaTim> GetCommessaTimAsync(string barcode);
    }
}
