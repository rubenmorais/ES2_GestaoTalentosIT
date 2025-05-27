using DbLayer.Models;
using WebAPI.DTOClasses;
using WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class EstadoService
    {
        private readonly IEstadoRepository _repository;

        public EstadoService(IEstadoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        
        public List<EstadoDTO> GetAllEstados()
        {
            return _repository.GetAllEstados();
        }
    }
}