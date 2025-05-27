using DbLayer.Context;
using DbLayer.Models;
using WebAPI.DTOClasses;
using WebAPI.Interfaces;


namespace WebAPI.Repositories
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly ApplicationDbContext _context;

        public EstadoRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            EnsureEstadosExist();
        }
        private void EnsureEstadosExist()
        {
            if (!_context.Estados.Any())
            {
                var estadosPadrao = new List<Estado>
                {
                    new Estado { Estadoid = 1, Estado1 = "Ativo" },
                    new Estado { Estadoid = 2, Estado1 = "Pendente" },
                    new Estado { Estadoid = 3, Estado1 = "Cancelado" },
                };

                _context.Estados.AddRange(estadosPadrao);
                _context.SaveChanges();
            }
        }
        
        public List<EstadoDTO> GetAllEstados()
        {
            return _context.Estados
                .Select(e => new EstadoDTO
                {
                    EstadoId = e.Estadoid,
                    Nome = e.Estado1
                })
                .ToList();
        }
    }
}