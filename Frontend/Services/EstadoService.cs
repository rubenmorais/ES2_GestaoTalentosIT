using Frontend.DTOClasses;
using System.Net.Http.Json;

namespace Frontend.Services
{
    public class EstadoService
    {
        private readonly HttpClient _httpClient;

        public EstadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EstadoDTO>?> GetAllEstadosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EstadoDTO>>("api/estados");
        }
    }
}