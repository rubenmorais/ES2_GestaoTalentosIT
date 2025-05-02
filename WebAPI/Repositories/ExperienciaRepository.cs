using DbLayer.Context;
using DbLayer.Models;
using WebAPI.DTOClasses;
using WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Repositories
{
    public class ExperienciaRepository : IExperienciaRepository
    {
        private readonly ApplicationDbContext _context;

        public ExperienciaRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<ExperienciasDTO> GetAll()
        {
            return _context.Experiencias
                .Select(e => new ExperienciasDTO
                {
                    ExperienciaId = e.Experienciaid,
                    TalentoId = e.Talentoid,
                    Titulo = e.Titulo,
                    Empresa = e.Empresa,
                    AnoInicio = e.AnoInicio,
                    AnoFim = e.AnoFim
                })
                .ToList();
        }

        public Experiencia GetById(int id)
        {
            return _context.Experiencias.FirstOrDefault(e => e.Experienciaid == id);
        }

        public Experiencia Create(CreateExperienciaDTO dto)
        {
            var talentoExiste = _context.Talentos.Any(t => t.Talentoid == dto.TalentoId);
            if (!talentoExiste)
                throw new Exception($"Talento com ID {dto.TalentoId} não existe.");
            
            if (dto.AnoFim.HasValue && dto.AnoFim < dto.AnoInicio)
                throw new Exception("O ano de fim não pode ser anterior ao ano de início.");

            var sobreposicaoExiste = _context.Experiencias.Any(e =>
                e.Talentoid == dto.TalentoId &&
                (
                    (!e.AnoFim.HasValue || e.AnoFim >= dto.AnoInicio) && 
                    (dto.AnoFim == null || e.AnoInicio <= dto.AnoFim)     
                )
            );

            if (sobreposicaoExiste)
                throw new Exception("Já existe uma experiência neste intervalo de anos para este talento.");

            var experiencia = new Experiencia
            {
                Talentoid = dto.TalentoId,
                Titulo = dto.Titulo,
                Empresa = dto.Empresa,
                AnoInicio = dto.AnoInicio,
                AnoFim = dto.AnoFim
            };

            _context.Experiencias.Add(experiencia);
            _context.SaveChanges();

            return experiencia;
        }

        public Experiencia Update(int id, UpdateExperienciaDTO dto)
        {
            var experiencia = _context.Experiencias.FirstOrDefault(e => e.Experienciaid == id);

            if (experiencia == null)
                throw new Exception("Experiência não encontrada.");
            
            if (dto.AnoFim.HasValue && dto.AnoFim < dto.AnoInicio)
                throw new Exception("O ano de fim não pode ser anterior ao ano de início.");
            
            var sobreposicaoExiste = _context.Experiencias.Any(e =>
                e.Talentoid == experiencia.Talentoid &&
                e.Experienciaid != id && 
                (
                    (!e.AnoFim.HasValue || e.AnoFim >= dto.AnoInicio) &&
                    (dto.AnoFim == null || e.AnoInicio <= dto.AnoFim)
                )
            );

            if (sobreposicaoExiste)
                throw new Exception("Já existe outra experiência neste intervalo de anos para este talento.");
            
            experiencia.Titulo = dto.Titulo;
            experiencia.Empresa = dto.Empresa;
            experiencia.AnoInicio = dto.AnoInicio;
            experiencia.AnoFim = dto.AnoFim;

            _context.SaveChanges();

            return experiencia;
        }

        public void Delete(int id)
        {
            var experiencia = _context.Experiencias.FirstOrDefault(e => e.Experienciaid == id);

            if (experiencia == null)
                throw new Exception("Experiência não encontrada.");

            _context.Experiencias.Remove(experiencia);
            _context.SaveChanges();
        }

        public async Task<List<ExperienciasDTO>> GetByTalentoIdAsync(int talentoId)
        {
            return await _context.Experiencias
                .Where(e => e.Talentoid == talentoId)
                .Select(e => new ExperienciasDTO
                {
                    ExperienciaId = e.Experienciaid,
                    TalentoId = e.Talentoid,
                    Titulo = e.Titulo,
                    Empresa = e.Empresa,
                    AnoInicio = e.AnoInicio,
                    AnoFim = e.AnoFim
                })
                .ToListAsync();
        }
    }
}