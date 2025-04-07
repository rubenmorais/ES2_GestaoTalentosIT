using Frontend.DTOClasses;
using System.Net.Http.Json;

namespace Frontend.Services
{
    public class TalentoService
    {
        private readonly HttpClient _httpClient;

        public TalentoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TalentoDTO>?> GetAllTalentosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TalentoDTO>>("https://localhost:7070/api/talento/index");
        }

        public async Task<TalentoDTO?> GetTalentoByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TalentoDTO>($"https://localhost:7070/api/talento/{id}");
        }

        public async Task<string?> CreateTalentoAsync(CreateTalentoDTO createTalentoDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7070/api/talento/create", createTalentoDTO);

            if (response.IsSuccessStatusCode)
            {
                return null; 
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            return $"Erro ao criar talento: {errorMessage}";
        }
        
        public async Task<TalentoDTO> UpdateTalentoAsync(UpdateTalentoDTO talento)
        {
            var url = $"https://localhost:7070/api/talento/{talento.Talentoid}";
            
            var response = await _httpClient.PutAsJsonAsync(url, talento);
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TalentoDTO>();
            }
            
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception(errorMessage);
        }


        public async Task<bool> DeleteTalentoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7070/api/talento/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}