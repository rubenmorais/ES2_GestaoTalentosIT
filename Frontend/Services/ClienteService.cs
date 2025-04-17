using Frontend.DTOClasses;
using System.Net.Http.Json;
using Frontend.DtoClasses;

namespace Frontend.Services
{
    public class ClienteService
    {
        private readonly HttpClient _httpClient;

        public ClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ClienteDTO>?> GetAllClientesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ClienteDTO>>("https://localhost:7070/api/cliente");
        }

        public async Task<ClienteDTO?> GetClienteByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7070/api/cliente/{id}");

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null; 
                }

                response.EnsureSuccessStatusCode(); 

                return await response.Content.ReadFromJsonAsync<ClienteDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar cliente com ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<string?> CreateClienteAsync(CreateClienteDTO createClienteDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7070/api/cliente", createClienteDTO);

            if (response.IsSuccessStatusCode)
            {
                return null; 
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            return $"Erro ao criar cliente: {errorMessage}";
        }
        
        public async Task<bool> UpdateClienteAsync(int id, UpdateClienteDTO cliente)
        {
            var url = $"https://localhost:7070/api/cliente/{id}";
            
            var response = await _httpClient.PutAsJsonAsync(url, cliente);
            
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7070/api/cliente/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}