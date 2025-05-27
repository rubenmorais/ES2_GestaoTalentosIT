using Frontend.DTOClasses;
using System.Net.Http.Json;

namespace Frontend.Services
{
    public class PropostaHabilidadeService
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "https://localhost:7070/api/PropostaHabilidade";

        public PropostaHabilidadeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PropostaHabilidadeDTO>?> GetHabilidadesByPropostaIdAsync(int propostaId)
        {
            return await _httpClient.GetFromJsonAsync<List<PropostaHabilidadeDTO>>($"{baseUrl}/proposta/{propostaId}");
        }

        public async Task<HttpResponseMessage> AddHabilidadeToPropostaAsync(AddPropostaHabilidadeDTO dto)
        {
            return await _httpClient.PostAsJsonAsync($"{baseUrl}/add", dto);
        }

        public async Task<HttpResponseMessage> UpdatePropostaHabilidadeAsync(int propostaId, int habilidadeId, UpdatePropostaHabilidadeDTO dto)
        {
            return await _httpClient.PutAsJsonAsync($"{baseUrl}/update/{propostaId}/{habilidadeId}", dto);
        }

        public async Task<HttpResponseMessage> RemoveHabilidadeFromPropostaAsync(int propostaId, int habilidadeId)
        {
            return await _httpClient.DeleteAsync($"{baseUrl}/remove/{propostaId}/{habilidadeId}");
        }
    }
}