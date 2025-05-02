using WebAPI.DTOClasses;
using DbLayer.Models;

namespace WebAPI.Interfaces
{
    public interface IUtilizadorRepository
    {
        List<UtilizadorDTO> GetAll();
        UtilizadorDTO GetById(int id);
        void Create(CreateUtilizadorDTO dto);
        void Update(int id, UpdateUtilizadorDTO dto);
        void UpdateAdmin(int id, UpdateUtilizadorAdminDTO dto);
        void Delete(int id);
        bool IsAdmin(int id);
        bool VerifyPassword(string providedPassword, string storedPasswordHash);
        UpdateUtilizadorDTO GetUpdateUtilizadorDTO(int id);
    }
}