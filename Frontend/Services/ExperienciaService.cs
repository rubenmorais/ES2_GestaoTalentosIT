using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frontend.DTOClasses;

namespace Frontend.Services
{
    public class ExperienciaService
    {
        private readonly HttpClient _http;

        public ExperienciaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ExperienciaDTO>> GetAllExperienciasAsync()
        {
            return await _http.GetFromJsonAsync<List<ExperienciaDTO>>("api/Experiencia/index");
        }

        public async Task<ExperienciaDTO> GetExperienciaByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ExperienciaDTO>($"api/Experiencia/{id}");
        }
        
        public async Task<List<ExperienciaDTO>> GetExperienciasByTalentoIdAsync(int talentoId)
        {
            return await _http.GetFromJsonAsync<List<ExperienciaDTO>>($"api/Experiencia/talento/{talentoId}");
        }

        public async Task<HttpResponseMessage> CreateExperienciaAsync(CreateExperienciaDTO novaExp)
        {
            return await _http.PostAsJsonAsync("api/Experiencia/create", novaExp);
        }

        public async Task<HttpResponseMessage> UpdateExperienciaAsync(int id, UpdateExperienciaDTO atualizacao)
        {
            return await _http.PutAsJsonAsync($"api/Experiencia/{id}", atualizacao);
        }

        public async Task<HttpResponseMessage> DeleteExperienciaAsync(int id)
        {
            return await _http.DeleteAsync($"api/Experiencia/{id}");
        }
    }
}