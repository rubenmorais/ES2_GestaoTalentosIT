using WebAPI.DTOClasses;
using WebAPI.Interfaces;
using System;
using System.Collections.Generic;

namespace WebAPI.Services
{
    public class PropostaHabilidadeService
    {
        private readonly IPropostaHabilidadeRepository _repository;

        public PropostaHabilidadeService(IPropostaHabilidadeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<PropostaHabilidadeDTO> GetHabilidadesByPropostaId(int propostaId)
        {
            return _repository.GetHabilidadesByPropostaId(propostaId);
        }

        public void AddHabilidadeToProposta(AddPropostaHabilidadeDTO dto)
        {
            _repository.AddHabilidadeToProposta(dto);
        }

        public void UpdatePropostaHabilidade(int propostaId, int habilidadeId, UpdatePropostaHabilidadeDTO dto)
        {
            _repository.UpdatePropostaHabilidade(propostaId, habilidadeId, dto);
        }

        public void RemoveHabilidadeFromProposta(int propostaId, int habilidadeId)
        {
            _repository.RemoveHabilidadeFromProposta(propostaId, habilidadeId);
        }
    }
}