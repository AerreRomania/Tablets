using System;
using System.Threading.Tasks;
using SmartB.Core.Constants;
using SmartB.Core.Contracts.Repository;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Models;

namespace SmartB.Core.Services.Data
{
    public class CommessaTimService : ICommessaTimService
    {
        private IGenericRepository _genericRepository;

        public CommessaTimService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<CommessaTim> GetCommessaTimAsync(string barcode)
        {
              UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
              {
                  Path = $"{ApiConstants.GetOrderFromTim}/{barcode}"
              };

               var commessa = await _genericRepository.GetAsync<CommessaTim>(builder.ToString());

               return commessa;
        }
    }
}
