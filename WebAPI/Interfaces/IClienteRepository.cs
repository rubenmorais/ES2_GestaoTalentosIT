using WebAPI.DTOClasses;


namespace WebAPI.Interfaces
{
    public interface IClienteRepository
    {
        List<ClienteDTO> GetAll();
        ClienteDTO GetById(int id);
        void Create(CreateClienteDTO dto);
        void Update(int id, UpdateClienteDTO dto);
        void Delete(int id);
    }
}