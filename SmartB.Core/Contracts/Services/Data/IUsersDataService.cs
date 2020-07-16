using System.Collections.Generic;
using System.Threading.Tasks;
using SmartB.Core.Models;
namespace SmartB.Core.Contracts.Services.Data
{
    public  interface IUsersDataService
    {
        Task<IEnumerable<string>> GetAllUserNames();
        Task<bool> GetUserStateAsync(string id);
        Task<Angajati> GetUser(string id);
        Task<Angajati> UpdateUserActivity(string id, Angajati userToUpdate);
    }
}