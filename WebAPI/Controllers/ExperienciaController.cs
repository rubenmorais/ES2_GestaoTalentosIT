using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using WebAPI.DTOClasses;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienciaController : ControllerBase
    {
        private readonly ExperienciaService _experienciaService;

        public ExperienciaController(ExperienciaService experienciaService)
        {
            _experienciaService = experienciaService;
        }

        [HttpGet("index")]
        public ActionResult<List<ExperienciasDTO>> GetAllExperiencias()
        {
            try
            {
                return _experienciaService.GetAllExperiencias();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao recuperar experiências: {ex.Message}");
            }
        }

        [HttpPost("create")]
        public ActionResult CreateExperiencia([FromBody] CreateExperienciaDTO createExperienciaDTO)
        {
            try
            {
                var experiencia = _experienciaService.CriarExperiencia(createExperienciaDTO);

                return CreatedAtAction(nameof(GetExperienciaPorId), new { id = experiencia.Experienciaid }, createExperienciaDTO);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar a experiência: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateExperienciaDTO dto)
        {
            try
            {
                var updatedExperiencia = _experienciaService.UpdateExperiencia(id, dto);
                return Ok(updatedExperiencia);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetExperienciaPorId(int id)
        {
            try
            {
                var experiencia = _experienciaService.GetExperienciaPorId(id);

                if (experiencia == null)
                    return NotFound($"Experiência com ID {id} não encontrada.");

                return Ok(experiencia);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao procurar experiência: {ex.Message}");
            }
        }
        
        [HttpGet("talento/{id}")]
        public async Task<ActionResult<List<ExperienciasDTO>>> GetByTalentoId(int id)
        {
            var experiencias = await _experienciaService.GetByTalentoIdAsync(id);
            return Ok(experiencias);
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteExperiencia(int id)
        {
            try
            {
                _experienciaService.DeleteExperiencia(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound($"Erro ao apagar experiência: {ex.Message}");
            }
        }
    }
}
