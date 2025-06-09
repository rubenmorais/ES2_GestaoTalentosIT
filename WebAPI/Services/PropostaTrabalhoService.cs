using WebAPI.DTOClasses;
using WebAPI.Interfaces;
using DbLayer.Models;
using Microsoft.EntityFrameworkCore;

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
        
        public List<TalentosElegiveisDTO> GetTalentosElegiveis(int propostaId)
        {
            var proposta = _repository.PropostasTrabalhos
                .Include(p => p.PropostasHabilidades)
                .FirstOrDefault(p => p.Propostaid == propostaId);

            if (proposta == null)
                throw new KeyNotFoundException($"Proposta {propostaId} não encontrada.");

            var talentos =_repository.Talentos
                .Where(t => t.Visibilidade)
                .Include(t => t.TalentosHabilidades)
                .ToList();

            var elegiveis = talentos
                .Where(t => proposta.PropostasHabilidades.All(req =>
                    t.TalentosHabilidades.Any(th =>
                        th.Habilidadeid    == req.Habilidadeid &&
                        th.AnosExperiencia >= req.MinAnosExp
                    )
                ))
                .Select(t => new TalentosElegiveisDTO()
                {
                    TalentoId   = t.Talentoid,
                    Nome        = t.Nome,
                    Email       = t.Email,
                    PrecoHora   = t.PrecoHora,
                    TotalHoras  = proposta.TotalHoras,
                    TotalValue  = t.PrecoHora * proposta.TotalHoras
                })
                .OrderBy(x => x.TotalValue)
                .ToList();

            return elegiveis;
        }
    }
}