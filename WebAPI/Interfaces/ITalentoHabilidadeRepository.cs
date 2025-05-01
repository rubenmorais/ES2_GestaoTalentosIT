using WebAPI.DTOClasses;
using System.Collections.Generic;

namespace WebAPI.Interfaces
{
    public interface ITalentoHabilidadeRepository
    {
        List<TalentoHabilidadeDTO> GetHabilidadesByTalentoId(int talentoId);
        void AddHabilidadeToTalento(AddTalentoHabilidadeDTO dto);
        void UpdateTalentoHabilidade(int talentoId, int habilidadeId, UpdateTalentoHabilidadeDTO dto);
        void RemoveHabilidadeFromTalento(int talentoId, int habilidadeId);
    }
}