using System.Net.Http.Json;
using Frontend.DTOClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Services
{
    public class RelatorioService
    {
        private readonly HttpClient _httpClient;

        public RelatorioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RelatorioPrecoMedioDTO>> GetRelatorioPrecoMedioAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<RelatorioPrecoMedioDTO>>("api/relatorio/preco-medio") ?? new List<RelatorioPrecoMedioDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter relatório: {ex.Message}");
                return new List<RelatorioPrecoMedioDTO>();
            }
        }
    }
} 