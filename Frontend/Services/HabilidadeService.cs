using System.Net.Http.Json;
using Frontend.DtoClasses;

namespace Frontend.Services
{
    public class HabilidadeService
    {
        private readonly HttpClient _httpClient;

        public HabilidadeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<List<HabilidadeDTO>?> GetAllHabilidadesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<HabilidadeDTO>>("api/habilidades");
        }
        
        public async Task<HabilidadeDTO?> GetHabilidadeByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/habilidades/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<HabilidadeDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar habilidade com ID {id}: {ex.Message}");
                return null;
            }
        }
        
        public async Task<string?> CreateHabilidadeAsync(CreateHabilidadeDTO createHabilidadeDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/habilidades", createHabilidadeDTO);
            if (response.IsSuccessStatusCode)
            {
                return null; 
            }
            var errorMessage = await response.Content.ReadAsStringAsync();
            return $"Erro ao criar habilidade: {errorMessage}";
        }
        
        public async Task<bool> UpdateHabilidadeAsync(UpdateHabilidadeDTO habilidade)
        {
            
            var url = $"api/habilidades/{habilidade.Habilidadeid}";
            var response = await _httpClient.PutAsJsonAsync(url, habilidade);

            if (response.IsSuccessStatusCode)
            {
                return true; 
            }
            
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception(errorMessage);
        }
        
        public async Task<bool> DeleteHabilidadeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/habilidades/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

