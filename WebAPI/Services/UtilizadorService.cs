using WebAPI.DTOClasses;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class UtilizadorService
    {
        private readonly IUtilizadorRepository _utilizadorRepository;

        public UtilizadorService(IUtilizadorRepository utilizadorRepository)
        {
            _utilizadorRepository = utilizadorRepository ?? throw new ArgumentNullException(nameof(utilizadorRepository));
        }

        public List<UtilizadorDTO> GetAllUsers()
        {
            return _utilizadorRepository.GetAll();
        }

        public UtilizadorDTO GetUserById(int id)
        {
            return _utilizadorRepository.GetById(id);
        }

        public void CreateUtilizador(CreateUtilizadorDTO createUtilizadorDTO)
        {
            _utilizadorRepository.Create(createUtilizadorDTO);
        }

        public void UpdateUtilizador(int utilizadorId, UpdateUtilizadorDTO updateUtilizadorDTO)
        {
            _utilizadorRepository.Update(utilizadorId, updateUtilizadorDTO);
        }

        public void UpdateUtilizadorAdmin(int utilizadorId, UpdateUtilizadorAdminDTO updateUtilizadorDTO)
        {
            _utilizadorRepository.UpdateAdmin(utilizadorId, updateUtilizadorDTO);
        }

        public void DeleteUtilizador(int id)
        {
            _utilizadorRepository.Delete(id);
        }

        public bool IsAdmin(int utilizadorId)
        {
            return _utilizadorRepository.IsAdmin(utilizadorId);
        }

        public bool VerifyPassword(string providedPassword, string storedPasswordHash)
        {
            return _utilizadorRepository.VerifyPassword(providedPassword, storedPasswordHash);
        }

        public UpdateUtilizadorDTO GetUpdateUtilizadorDTO(int id)
        {
            return _utilizadorRepository.GetUpdateUtilizadorDTO(id);
        }
    }
}