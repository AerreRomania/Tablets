using System.Collections.Generic;
using System.Threading.Tasks;
using SmartB.Core.Models;

namespace SmartB.Core.Contracts.Services.Data
{
    public interface IComenziService
    {
        Task<IEnumerable<Comenzi>> GetOrdersAsync(int sectorId);
        Task<Comenzi> GetOrderAsync(int id);
        Task<Comenzi> GetOrderWithName(string commessa);
    }
}
