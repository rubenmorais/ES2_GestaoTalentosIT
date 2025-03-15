using Frontend.DtoClasses;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

namespace Frontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage,
            AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<string?> LoginAsync(LoginDTO loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();

                if (authResponse != null)
                {
                    await _localStorage.SetItemAsync("authToken", authResponse.Token);
                    await ((CustomAuthStateProvider)_authStateProvider).MarkUserAsAuthenticated(authResponse.Token);
                    return authResponse.Token;
                }
            }

            return null;
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await ((CustomAuthStateProvider)_authStateProvider).MarkUserAsLoggedOut();
        }

        public async Task<bool> RegisterAsync(CreateUtilizadorDTO createUtilizadorDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7070/api/Utilizador/create", createUtilizadorDTO);
                
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Erro no registo: {errorMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar registar: {ex.Message}");
                return false;
            }
        }
    }
}