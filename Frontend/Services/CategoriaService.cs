using Frontend.DTOClasses;
using System.Net.Http.Json;
using Frontend.DtoClasses;

namespace Frontend.Services
{
    public class CategoriaService
    {
        private readonly HttpClient _httpClient;

        public CategoriaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoriaDTO>?> GetAllCategoriasAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CategoriaDTO>>("https://localhost:7070/api/categoria");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar categorias: {ex.Message}");
                return null;
            }
        }

        public async Task<CategoriaDTO?> GetCategoriaByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7070/api/categoria/{id}");

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null; 
                }

                response.EnsureSuccessStatusCode(); 

                return await response.Content.ReadFromJsonAsync<CategoriaDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar categoria com ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<string?> CreateCategoriaAsync(CreateCategoriaDTO createCategoriaDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7070/api/categoria", createCategoriaDTO);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            return $"{responseContent}";
        }


        public async Task<string> UpdateCategoriaAsync(UpdateCategoriaDTO categoria)
        {
            var url = $"https://localhost:7070/api/categoria/{categoria.CategoriaId}";
    
            var response = await _httpClient.PutAsJsonAsync(url, categoria);
    
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return content; 
            }

            throw new Exception(content);
        }



        public async Task<bool> DeleteCategoriaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7070/api/categoria/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true; 
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();  
                throw new Exception(errorMessage); 
            }
        }

    }
}
