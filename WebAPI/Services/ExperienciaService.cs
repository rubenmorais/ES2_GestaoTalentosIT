using DbLayer.Models;
using WebAPI.DTOClasses;

namespace WebAPI.Services
{
    public class ExperienciaService
    {
        private static List<Experiencia> _experiencias = new List<Experiencia>();
        private static int _nextId = 1;

        public List<ExperienciasDTO> GetAllExperiencias()
        {
            return _experiencias.Select(e => new ExperienciasDTO
            {
                ExperienciaId = e.Experienciaid,
                TalentoId = e.Talentoid,
                Titulo = e.Titulo,
                Empresa = e.Empresa,
                AnoInicio = e.AnoInicio,
                AnoFim = e.AnoFim
            }).ToList();
        }

        public Experiencia CriarExperiencia(CreateExperienciaDTO dto)
        {
            var experiencia = new Experiencia
            {
                Experienciaid = _nextId++,
                Talentoid = dto.TalentoId,
                Titulo = dto.Titulo,
                Empresa = dto.Empresa,
                AnoInicio = dto.AnoInicio,
                AnoFim = dto.AnoFim
            };

            _experiencias.Add(experiencia);
            return experiencia;
        }

        public Experiencia UpdateExperiencia(int id, UpdateExperienciaDTO dto)
        {
            var experiencia = _experiencias.FirstOrDefault(e => e.Experienciaid == id);

            if (experiencia == null)
                throw new Exception("Experiência não encontrada.");

            experiencia.Titulo = dto.Titulo;
            experiencia.Empresa = dto.Empresa;
            experiencia.AnoInicio = dto.AnoInicio;
            experiencia.AnoFim = dto.AnoFim;

            return experiencia;
        }

        public Experiencia GetExperienciaPorId(int id)
        {
            return _experiencias.FirstOrDefault(e => e.Experienciaid == id);
        }
        
        public async Task<List<ExperienciasDTO>> GetByTalentoIdAsync(int id) {
            var resultado = _experiencias
                .Where(e => e.Talentoid == id)
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

            return await Task.FromResult(resultado);
        }


        public void DeleteExperiencia(int id)
        {
            var experiencia = _experiencias.FirstOrDefault(e => e.Experienciaid == id);

            if (experiencia == null)
                throw new Exception("Experiência não encontrada.");

            _experiencias.Remove(experiencia);
        }
    }
}
