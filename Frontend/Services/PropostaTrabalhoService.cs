using Frontend.DTOClasses;
using System.Net.Http.Json;

namespace Frontend.Services
{
    public class PropostaTrabalhoService
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "https://localhost:7070/api/PropostaTrabalho";

        public PropostaTrabalhoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PropostaTrabalhoDTO>?> GetAllPropostasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PropostaTrabalhoDTO>>($"{baseUrl}/index");
        }

        public async Task<PropostaTrabalhoDTO?> GetPropostaByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PropostaTrabalhoDTO>($"{baseUrl}/{id}");
        }

        public async Task<HttpResponseMessage> CreatePropostaAsync(CreatePropostaTrabalhoDTO proposta)
        {
            return await _httpClient.PostAsJsonAsync($"{baseUrl}/create", proposta);
        }

        public async Task<HttpResponseMessage> UpdatePropostaAsync(int id, UpdatePropostaTrabalhoDTO propostaAtualizada)
        {
            return await _httpClient.PutAsJsonAsync($"{baseUrl}/{id}", propostaAtualizada);
        }

        public async Task<HttpResponseMessage> DeletePropostaAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{baseUrl}/{id}");
        }
    }
}