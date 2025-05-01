using WebAPI.DTOClasses;
using WebAPI.Interfaces;
using System;
using System.Collections.Generic;

namespace WebAPI.Services
{
    public class TalentoHabilidadeService
    {
        private readonly ITalentoHabilidadeRepository _repository;

        public TalentoHabilidadeService(ITalentoHabilidadeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<TalentoHabilidadeDTO> GetHabilidadesByTalentoId(int talentoId)
        {
            return _repository.GetHabilidadesByTalentoId(talentoId);
        }

        public void AddHabilidadeToTalento(AddTalentoHabilidadeDTO dto)
        {
            _repository.AddHabilidadeToTalento(dto);
        }

        public void UpdateTalentoHabilidade(int talentoId, int habilidadeId, UpdateTalentoHabilidadeDTO dto)
        {
            _repository.UpdateTalentoHabilidade(talentoId, habilidadeId, dto);
        }

        public void RemoveHabilidadeFromTalento(int talentoId, int habilidadeId)
        {
            _repository.RemoveHabilidadeFromTalento(talentoId, habilidadeId);
        }
    }
}