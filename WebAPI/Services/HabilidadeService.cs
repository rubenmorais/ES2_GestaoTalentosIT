using WebAPI.DTOClasses;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class HabilidadeService
    {
        private readonly IHabilidadeRepository _habilidadeRepository;

        public HabilidadeService(IHabilidadeRepository habilidadeRepository)
        {
            _habilidadeRepository = habilidadeRepository ?? throw new ArgumentNullException(nameof(habilidadeRepository));
        }

        public async Task<IEnumerable<HabilidadeDTO>> GetAllAsync()
        {
            return await _habilidadeRepository.GetAllAsync();
        }

        public async Task<HabilidadeDTO?> GetByIdAsync(int id)
        {
            return await _habilidadeRepository.GetByIdAsync(id);
        }

        public async Task<HabilidadeDTO> CreateAsync(CreateHabilidadeDTO dto)
        {
            return await _habilidadeRepository.CreateAsync(dto);
        }

        public async Task<bool> UpdateAsync(int id, UpdateHabilidadeDTO dto)
        {
            return await _habilidadeRepository.UpdateAsync(id, dto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _habilidadeRepository.DeleteAsync(id);
        }

        public async Task<bool> HabilidadeExistsAsync(int id)
        {
            return await _habilidadeRepository.HabilidadeExistsAsync(id);
        }
    }
}