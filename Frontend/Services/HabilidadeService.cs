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

        // Busca todas as habilidades
        public async Task<List<HabilidadeDTO>?> GetAllHabilidadesAsync()
        {
            // Use URI relativo; o BaseAddress (https://localhost:7070/) será combinado com "api/habilidades"
            return await _httpClient.GetFromJsonAsync<List<HabilidadeDTO>>("api/habilidades");
        }

        // Busca uma habilidade pelo ID
        public async Task<HabilidadeDTO?> GetHabilidadeByIdAsync(int id)
        {
            try
            {
                // URI relativo para a requisição GET
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

        // Cria uma nova habilidade
        public async Task<string?> CreateHabilidadeAsync(CreateHabilidadeDTO createHabilidadeDTO)
        {
            // URI relativo para a criação
            var response = await _httpClient.PostAsJsonAsync("api/habilidades", createHabilidadeDTO);
            if (response.IsSuccessStatusCode)
            {
                return null; // Criação efetuada com sucesso.
            }
            var errorMessage = await response.Content.ReadAsStringAsync();
            return $"Erro ao criar habilidade: {errorMessage}";
        }

        // Atualiza uma habilidade existente
        public async Task<HabilidadeDTO> UpdateHabilidadeAsync(HabilidadeDTO habilidade)
        {
            // URI relativo para a atualização
            var url = $"api/habilidades/{habilidade.Habilidadeid}";
            var response = await _httpClient.PutAsJsonAsync(url, habilidade);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<HabilidadeDTO>();
            }
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception(errorMessage);
        }

        // Remove uma habilidade
        public async Task<bool> DeleteHabilidadeAsync(int id)
        {
            // URI relativo para a deleção
            var response = await _httpClient.DeleteAsync($"api/habilidades/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

