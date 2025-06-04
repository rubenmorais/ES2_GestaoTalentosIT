using WebAPI.DTOClasses;
using WebAPI.Interfaces;
using System;
using System.Collections.Generic;

namespace WebAPI.Services
{
    public class PropostaTalentoService
    {
        private readonly IPropostaTalentoRepository _repository;

        public PropostaTalentoService(IPropostaTalentoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<PropostaTalentoDTO> GetTalentosByPropostaId(int propostaId)
        {
            return _repository.GetTalentosByPropostaId(propostaId);
        }

        public void AddTalentoToProposta(AddPropostaTalentoDTO dto)
        {
            _repository.AddTalentoToProposta(dto);
        }

        public void RemoveTalentoFromProposta(int propostaId, int talentoId)
        {
            _repository.RemoveTalentoFromProposta(propostaId, talentoId);
        }
    }
}