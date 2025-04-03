using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;

namespace Frontend.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private ClaimsPrincipal _user = new ClaimsPrincipal(new ClaimsIdentity());
        private int? _userId; 

        public CustomAuthStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsStringAsync("authToken");

            ClaimsPrincipal user = new ClaimsPrincipal(new ClaimsIdentity());

            if (!string.IsNullOrEmpty(token))
            {
                var claims = ParseToken(token); 
                user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            }

            var authState = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
            return authState;
        }


        public async Task MarkUserAsAuthenticated(string token)
        {
            var claims = ParseToken(token);
            _user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

            var userIdClaim = _user.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            _userId = int.TryParse(userIdClaim, out var userId) ? (int?)userId : null; 

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task MarkUserAsLoggedOut()
        {
            _user = new ClaimsPrincipal(new ClaimsIdentity());
            _userId = null; 
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private IEnumerable<Claim> ParseToken(string token)
        {
            var payload = token.Split('.')[1];
            var jsonBytes = WebEncoders.Base64UrlDecode(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kv => new Claim(kv.Key, kv.Value.ToString()));
        }
    }

}