using System.Collections.Generic;
using System.Threading.Tasks;
using SmartB.Core.Models;

namespace SmartB.Core.Contracts.Services.Data
{
    public interface IMasiniService
    {
        Task<IEnumerable<Masini>> GetMachinesAsync(int sectorId, string line);

        Task<bool> GetMachineStateAsync(string id);

        Task<Masini> GetMachineAsync(string id);

        Task<Masini> UpdateMachineActivity(int id, Masini machineToUpdate);
    }
}
