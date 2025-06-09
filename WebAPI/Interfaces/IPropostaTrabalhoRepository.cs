using WebAPI.DTOClasses;
using DbLayer.Models;

namespace WebAPI.Interfaces
{
    public interface IPropostaTrabalhoRepository
    {
        List<PropostaTrabalhoDTO> GetAll();
        PropostaTrabalhoDTO GetById(int id);
        PropostasTrabalho Create(CreatePropostaTrabalhoDTO dto);
        PropostaTrabalhoDTO Update(int id, UpdatePropostaTrabalhoDTO dto);
        void Delete(int id);
        IQueryable<PropostasTrabalho> PropostasTrabalhos { get; }
        IQueryable<Talento>        Talentos          { get; }
    }
}