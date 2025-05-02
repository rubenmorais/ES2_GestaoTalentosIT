using WebAPI.DTOClasses;

namespace WebAPI.Interfaces
{
    public interface IHabilidadeRepository
    {
        Task<IEnumerable<HabilidadeDTO>> GetAllAsync();
        Task<HabilidadeDTO?> GetByIdAsync(int id);
        Task<HabilidadeDTO> CreateAsync(CreateHabilidadeDTO dto);
        Task<bool> UpdateAsync(int id, UpdateHabilidadeDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> HabilidadeExistsAsync(int id);
    }
}