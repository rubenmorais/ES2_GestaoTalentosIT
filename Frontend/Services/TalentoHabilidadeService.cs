using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Frontend.DTOClasses;

namespace Frontend.Services
{
    public class TalentoHabilidadeService
    {
        private readonly HttpClient _httpClient;

        public TalentoHabilidadeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TalentoHabilidadeDTO>> GetHabilidadesByTalentoIdAsync(int talentoId)
        {
            return await _httpClient.GetFromJsonAsync<List<TalentoHabilidadeDTO>>($"api/TalentoHabilidade/talento/{talentoId}");
        }

        public async Task<HttpResponseMessage> AddHabilidadeToTalentoAsync(AddTalentoHabilidadeDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/TalentoHabilidade/add", dto);
        }

        public async Task<HttpResponseMessage> UpdateTalentoHabilidadeAsync(int talentoId, int habilidadeId, UpdateTalentoHabilidadeDTO dto)
        {
            return await _httpClient.PutAsJsonAsync($"api/TalentoHabilidade/update/{talentoId}/{habilidadeId}", dto);
        }

        public async Task<HttpResponseMessage> RemoveHabilidadeFromTalentoAsync(int talentoId, int habilidadeId)
        {
            return await _httpClient.DeleteAsync($"api/TalentoHabilidade/remove/{talentoId}/{habilidadeId}");
        }
    }
}