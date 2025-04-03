using DbLayer.Context;
using DbLayer.Models;
using WebAPI.DTOClasses;

namespace WebAPI.Services
{
    public class TalentoService
    {
        private readonly ApplicationDbContext _context;

        public TalentoService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Talento CriarTalento(CreateTalentoDTO dto)
        {
            var utilizadorExiste = _context.Utilizadores.Any(u => u.Utilizadorid == dto.UtilizadorId);
            if (!utilizadorExiste)
            {
                throw new ArgumentException("O UtilizadorId fornecido não existe.");
            }
            var emailExiste = _context.Talentos.Any(t => t.Email == dto.Email);
            if (emailExiste)
            {
                throw new ArgumentException("O e-mail fornecido já está associado a outro talento.");
            }

            var talento = new Talento
            {
                Utilizadorid = dto.UtilizadorId,
                Nome = dto.Nome,
                Pais = dto.Pais,
                Email = dto.Email,
                PrecoHora = dto.PrecoPorHora,
                Visibilidade = dto.Visibilidade
            };

            _context.Talentos.Add(talento);
            _context.SaveChanges();

            return talento;
        }

        public List<TalentoDTO> GetAllTalentos()
        {
            return _context.Talentos
                .Select(t => new TalentoDTO
                {
                    Talentoid = t.Talentoid,
                    UtilizadorId = t.Utilizadorid,
                    Nome = t.Nome,
                    Pais = t.Pais,
                    Email = t.Email,
                    PrecoPorHora = t.PrecoHora ?? 0, 
                    Visibilidade = t.Visibilidade ?? false  
                })
                .ToList();
        }

        public TalentoDTO? GetTalentoPorId(int id)
        {
            return _context.Talentos
                .Where(t => t.Talentoid == id)
                .Select(t => new TalentoDTO
                {
                    Talentoid = t.Talentoid,
                    UtilizadorId = t.Utilizadorid,
                    Nome = t.Nome,
                    Pais = t.Pais,
                    Email = t.Email,
                    PrecoPorHora = t.PrecoHora ?? 0, 
                    Visibilidade = t.Visibilidade ?? false
                })
                .FirstOrDefault();
        }

        public void DeleteTalento(int id)
        {
            var talento = _context.Talentos.FirstOrDefault(t => t.Talentoid == id);
            if (talento == null)
            {
                throw new Exception($"Talento com ID {id} não encontrado.");
            }

            _context.Talentos.Remove(talento);
            _context.SaveChanges();
        }
    }
}
