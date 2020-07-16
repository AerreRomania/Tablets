using System;
using System.Threading.Tasks;
using SmartB.Core.Constants;
using SmartB.Core.Contracts.Repository;
using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Models;
namespace SmartB.Core.Services.Data
{
    public class ArticoleService : IArticoleService
    {
        private IGenericRepository _genericRepository;
        public ArticoleService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<Articole> GetArticleAsync(int id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.ArticleEndpoint}/{id}"
            };
            return await _genericRepository.GetAsync<Articole>(builder.ToString());
        }
    }
}
