using WebAPI.DTOClasses;
using DbLayer.Models;

namespace WebAPI.Interfaces
{
    public interface ITalentoRepository
    {
        List<TalentoDTO> GetAll();
        TalentoDTO? GetById(int id);
        Talento Create(CreateTalentoDTO dto);
        TalentoDTO Update(int id, UpdateTalentoDTO dto);
        void Delete(int id);
    }
}