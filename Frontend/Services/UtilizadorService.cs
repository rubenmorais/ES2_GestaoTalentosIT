

using Microsoft.AspNetCore.Components.Authorization;

namespace Frontend.Services
{
    public class UtilizadorService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly HttpClient _httpClient;

        public UtilizadorService(AuthenticationStateProvider authenticationStateProvider, HttpClient httpClient)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _httpClient = httpClient;
        }

        public async Task<int?> GetUserIdAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var userClaims = authState.User.Claims;
            
            var userIdClaim = userClaims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userIdClaim != null)
            {
                if (int.TryParse(userIdClaim.Value, out var userId))
                {
                    return userId;
                }
            }

            return null; 
        }
        public async Task<bool> IsAdminAsync()
        {
            var userId = await GetUserIdAsync();
            
            if (userId == null)
            {
                return false; 
            }
            
            var isAdmin = await _httpClient.GetFromJsonAsync<bool>($"https://localhost:7070/api/utilizador/isadmin/{userId}");

            return isAdmin;
        }
    }
}