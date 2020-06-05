using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartB.Core.Constants;
using SmartB.Core.Contracts.Repository;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Models;

namespace SmartB.Core.Services.Data
{
    public class PhaseService : IPhaseService
    {
        private IGenericRepository _genericRepository;

        public PhaseService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Phases>> GetPhasesAsync(int articleId, string machineName)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.ArticleEndpoint}/{articleId}/phases/{machineName}"
            };
            return await _genericRepository.GetAsync<IEnumerable<Phases>>(builder.ToString());
        }
    }
}
