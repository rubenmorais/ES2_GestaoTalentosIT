using WebAPI.DTOClasses;
using System.Collections.Generic;

namespace WebAPI.Interfaces
{
    public interface IPropostaHabilidadeRepository
    {
        List<PropostaHabilidadeDTO> GetHabilidadesByPropostaId(int propostaId);
        void AddHabilidadeToProposta(AddPropostaHabilidadeDTO dto);
        void UpdatePropostaHabilidade(int propostaId, int habilidadeId, UpdatePropostaHabilidadeDTO dto);
        void RemoveHabilidadeFromProposta(int propostaId, int habilidadeId);
    }
}