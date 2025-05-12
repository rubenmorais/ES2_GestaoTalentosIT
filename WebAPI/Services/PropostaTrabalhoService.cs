using WebAPI.DTOClasses;
using WebAPI.Interfaces;
using DbLayer.Models;

namespace WebAPI.Services
{
    public class PropostaTrabalhoService
    {
        private readonly IPropostaTrabalhoRepository _repository;

        public PropostaTrabalhoService(IPropostaTrabalhoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<PropostaTrabalhoDTO> GetAllPropostas()
        {
            return _repository.GetAll();
        }

        public PropostaTrabalhoDTO? GetPropostaPorId(int id)
        {
            return _repository.GetById(id);
        }

        public PropostasTrabalho CriarProposta(CreatePropostaTrabalhoDTO dto)
        {
            return _repository.Create(dto);
        }

        public PropostaTrabalhoDTO UpdateProposta(int id, UpdatePropostaTrabalhoDTO dto)
        {
            return _repository.Update(id, dto);
        }

        public void DeleteProposta(int id)
        {
            _repository.Delete(id);
        }
    }
}