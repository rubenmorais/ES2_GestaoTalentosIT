using WebAPI.DTOClasses;
using WebAPI.DtoClasses;

namespace WebAPI.Interfaces
{
    public interface ICategoriaRepository
    {
        List<CategoriaDTO> GetAll();
        CategoriaDTO GetById(int id);
        void Create(CreateCategoriaDTO dto);
        void Update(int id, UpdateCategoriaDTO dto);
        void Delete(int id);
    }
}