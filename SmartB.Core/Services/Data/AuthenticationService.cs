using SmartB.Core.Contracts.Services.Data;
using SmartB.Core.Models;
using System;
using System.Threading.Tasks;
using SmartB.Core.Constants;
using SmartB.Core.Contracts.Repository;
using SmartB.Core.Contracts.Services.General;
namespace SmartB.Core.Services.Data
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ISettingsService _settingsService;
        private IDialogService _dilaogService;
        public AuthenticationService(IGenericRepository genericRepository, ISettingsService settingsService, IDialogService dialogService)
        {
            _settingsService = settingsService;
            _genericRepository = genericRepository;
            _dilaogService = dialogService;
        }
        public async Task<AuthenticationResponse> Authenticate(string username, string password)
        {
            AuthenticationResponse authResponse = null;
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.AuthenticateEndpoint}/username={username}/password={password}"
            };
            var response = await _genericRepository.GetAsync<Angajati>(builder.ToString());
            if (response == null) return null;
            if (!response.Active)
            {
                authResponse = new AuthenticationResponse()
                {
                    IsAuthenticated = true,
                    User = response
                };
            }
            else
            {
                await _dilaogService.ShowDialog(
                    $"User {response.Angajat} already logged.\nPlease contact your superior.",
                    $"{response.Angajat} already logged", "OK");
            }
            return authResponse;
        }
        public bool IsUserAuthenticated()
        {
            return !string.IsNullOrEmpty(_settingsService.UserIdSetting);
        }
    }
}
