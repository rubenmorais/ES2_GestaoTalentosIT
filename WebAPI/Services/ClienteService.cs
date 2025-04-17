using WebAPI.DTOClasses;
using WebAPI.Interfaces;
using System.Collections.Generic;

namespace WebAPI.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<ClienteDTO> GetAllClientes()
        {
            return _repository.GetAll();
        }

        public ClienteDTO GetClienteById(int id)
        {
            return _repository.GetById(id);
        }

        public void CreateCliente(CreateClienteDTO dto)
        {
            _repository.Create(dto);
        }

        public void UpdateCliente(int id, UpdateClienteDTO dto)
        {
            _repository.Update(id, dto);
        }

        public void DeleteCliente(int id)
        {
            _repository.Delete(id);
        }
    }
}