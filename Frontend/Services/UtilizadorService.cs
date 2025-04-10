using Frontend.DtoClasses;
using Frontend.DTOClasses;
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
        public async Task<bool> UpdateUtilizadorAsync(int id, UpdateUtilizadorDTO updateUtilizadorDTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7070/api/utilizador/{id}", updateUtilizadorDTO);
                
                if (response.IsSuccessStatusCode)
                {
                    return true; 
                }
                else
                {
                    return false; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar utilizador: {ex.Message}");
                return false; 
            }
        }
        public async Task<bool> UpdateUtilizadorAdminAsync(int id, UpdateUtilizadorAdminDTO updateUtilizadorDTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7070/api/utilizador/admin/{id}", updateUtilizadorDTO);
        
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorMessage); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
        }

        public async Task<UtilizadorDTO> GetUserByIdAsync(int id)
        {
            try
            {
                var utilizador = await _httpClient.GetFromJsonAsync<UtilizadorDTO>($"https://localhost:7070/api/utilizador/{id}");
                return utilizador;
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Utilizador não encontrado.");
                    return null; 
                }

                Console.WriteLine($"Erro ao recuperar o utilizador: {ex.Message}");
                return null;
            }
        }
        public async Task<List<UtilizadorDTO>> GetAllUsersAsync()
        {
            try
            {
                var utilizadores = await _httpClient.GetFromJsonAsync<List<UtilizadorDTO>>("https://localhost:7070/api/utilizador/index");
                return utilizadores;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao recuperar a lista de utilizadores: {ex.Message}");
                return new List<UtilizadorDTO>();
            }
        }
        
        public async Task<string> CreateUtilizadorAsync(CreateUtilizadorDTO createUtilizadorDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7070/api/Utilizador/create",
                    createUtilizadorDTO);

                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    return "Já existe um utilizador com esse e-mail.";
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Erro no registo: {errorMessage}");
                    return "Erro ao registrar utilizador.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar registar: {ex.Message}");
                return "Erro inesperado. Tente novamente mais tarde.";
            }
        }
        
        public async Task<string> DeleteUtilizadorAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:7070/api/utilizador/{id}");
        
                if (response.IsSuccessStatusCode)
                {
                    return "Utilizador apagado com sucesso!";
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return errorMessage;  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return $"{ex.Message}";
            }
        }
    }
}