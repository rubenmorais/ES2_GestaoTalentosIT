using WebAPI.DTOClasses;
using DbLayer.Models;

namespace WebAPI.Interfaces
{
    public interface IExperienciaRepository
    {
        List<ExperienciasDTO> GetAll();
        Experiencia GetById(int id);
        Experiencia Create(CreateExperienciaDTO dto);
        Experiencia Update(int id, UpdateExperienciaDTO dto);
        void Delete(int id);
        Task<List<ExperienciasDTO>> GetByTalentoIdAsync(int talentoId);
    }
}