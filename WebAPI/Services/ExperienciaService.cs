using DbLayer.Models;
using WebAPI.DTOClasses;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class ExperienciaService
    {
        private readonly IExperienciaRepository _repository;

        public ExperienciaService(IExperienciaRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<ExperienciasDTO> GetAllExperiencias()
        {
            return _repository.GetAll();
        }

        public Experiencia CriarExperiencia(CreateExperienciaDTO dto)
        {
            return _repository.Create(dto);
        }

        public Experiencia UpdateExperiencia(int id, UpdateExperienciaDTO dto)
        {
            return _repository.Update(id, dto);
        }

        public Experiencia GetExperienciaPorId(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<List<ExperienciasDTO>> GetByTalentoIdAsync(int talentoId)
        {
            return await _repository.GetByTalentoIdAsync(talentoId);
        }

        public void DeleteExperiencia(int id)
        {
            _repository.Delete(id);
        }
    }
}