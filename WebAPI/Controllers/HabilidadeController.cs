using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOClasses;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabilidadesController : ControllerBase
    {
        private readonly HabilidadeService _service;

        public HabilidadesController(HabilidadeService service)
        {
            _service = service;
        }

        // GET: api/habilidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HabilidadeDTO>>> GetHabilidades()
        {
            var habilidades = await _service.GetAllAsync();
            return Ok(habilidades);
        }

        // GET: api/habilidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HabilidadeDTO>> GetHabilidade(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
                return NotFound();

            return dto;
        }

        // POST: api/habilidades
        [HttpPost]
        public async Task<ActionResult<HabilidadeDTO>> CreateHabilidade(CreateHabilidadeDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                return BadRequest("Nome da habilidade é obrigatório.");

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetHabilidade), new { id = created.Habilidadeid }, created);
        }

        // PUT: api/habilidades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHabilidade(int id, UpdateHabilidadeDTO dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success)
                return BadRequest("Erro ao atualizar a habilidade.");
            return NoContent();
        }

        // DELETE: api/habilidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabilidade(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return BadRequest("Não foi possível apagar a habilidade. Ela pode estar associada a profissionais ou não existir.");
            return NoContent();
        }
    }
}
