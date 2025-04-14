using WebAPI.DTOClasses;
using WebAPI.DtoClasses;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        
        public List<CategoriaDTO> GetAllCategorias()
        {
            return _repository.GetAll();
        }
        
        public CategoriaDTO GetCategoriaById(int id)
        {
            return _repository.GetById(id);
        }
        
        public void CreateCategoria(CreateCategoriaDTO dto)
        {
            _repository.Create(dto);
        }
        
        public void UpdateCategoria(int id, UpdateCategoriaDTO dto)
        {
            _repository.Update(id, dto);
        }
        
        public void DeleteCategoria(int id)
        {
            _repository.Delete(id);
        }
    }
}