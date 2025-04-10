using DbLayer.Context;
using DbLayer.Models;
using WebAPI.DTOClasses;


namespace WebAPI.Services
{
    public class CategoriaService
    {
        private readonly ApplicationDbContext _context;

        public CategoriaService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public List<CategoriaDTO> GetAllCategorias()
        {
            try
            {
                return _context.CategoriasProfissionais
                    .Select(c => new CategoriaDTO
                    {
                        CategoriaId = c.Categoriaid,
                        Categoria = c.Categoria
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public CategoriaDTO GetCategoriaById(int id)
        {
            try
            {
                var categoria = _context.CategoriasProfissionais.FirstOrDefault(c => c.Categoriaid == id);
                if (categoria == null)
                {
                    throw new Exception("Categoria não encontrada.");
                }

                return new CategoriaDTO
                {
                    CategoriaId = categoria.Categoriaid,
                    Categoria = categoria.Categoria
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public void CreateCategoria(CreateCategoriaDTO dto)
        {
            try
            {
                var exists = _context.CategoriasProfissionais.Any(c => c.Categoria == dto.Categoria);
                if (exists)
                {
                    throw new Exception("Já existe uma categoria com esse nome.");
                }

                var categoria = new CategoriasProfissionai
                {
                    Categoria = dto.Categoria
                };

                _context.CategoriasProfissionais.Add(categoria);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public void UpdateCategoria(int id, CreateCategoriaDTO dto)
        {
            try
            {
                var categoria = _context.CategoriasProfissionais.FirstOrDefault(c => c.Categoriaid == id);
                if (categoria == null)
                {
                    throw new Exception("Categoria não encontrada.");
                }

                var nomeDuplicado = _context.CategoriasProfissionais
                    .Any(c => c.Categoria == dto.Categoria && c.Categoriaid != id);

                if (nomeDuplicado)
                {
                    throw new Exception("Já existe outra categoria com esse nome.");
                }

                categoria.Categoria = dto.Categoria;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public void DeleteCategoria(int id)
        {
            try
            {
                var categoria = _context.CategoriasProfissionais.FirstOrDefault(c => c.Categoriaid == id);
                if (categoria == null)
                {
                    throw new Exception("Categoria não encontrada.");
                }

                _context.CategoriasProfissionais.Remove(categoria);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
