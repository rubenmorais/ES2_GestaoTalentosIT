using Frontend.DTOClasses;
using System.Net.Http.Json;

namespace Frontend.Services
{
    public class PropostaTalentoService
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "https://localhost:7070/api/PropostaTalento";

        public PropostaTalentoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PropostaTalentoDTO>?> GetTalentosByPropostaIdAsync(int propostaId)
        {
            return await _httpClient.GetFromJsonAsync<List<PropostaTalentoDTO>>($"{baseUrl}/proposta/{propostaId}");
        }

        public async Task<HttpResponseMessage> AddTalentoToPropostaAsync(AddPropostaTalentoDTO dto)
        {
            return await _httpClient.PostAsJsonAsync($"{baseUrl}/add", dto);
        }

        public async Task<HttpResponseMessage> RemoveTalentoFromPropostaAsync(int propostaId, int talentoId)
        {
            return await _httpClient.DeleteAsync($"{baseUrl}/remove/{propostaId}/{talentoId}");
        }
    }
}