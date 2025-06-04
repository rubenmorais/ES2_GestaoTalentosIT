using WebAPI.DTOClasses;

namespace WebAPI.Interfaces
{
    public interface IPropostaTalentoRepository
    {
        List<PropostaTalentoDTO> GetTalentosByPropostaId(int propostaId);
        void AddTalentoToProposta(AddPropostaTalentoDTO dto);
        void RemoveTalentoFromProposta(int propostaId, int talentoId);
    }
}