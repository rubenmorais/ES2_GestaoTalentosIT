using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using WebAPI.DTOClasses;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalentoHabilidadeController : ControllerBase
    {
        private readonly TalentoHabilidadeService _service;

        public TalentoHabilidadeController(TalentoHabilidadeService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("talento/{talentoId}")]
        public ActionResult<List<TalentoHabilidadeDTO>> GetHabilidadesByTalentoId(int talentoId)
        {
            try
            {
                var habilidades = _service.GetHabilidadesByTalentoId(talentoId);
                return Ok(habilidades);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao recuperar habilidades: {ex.Message}");
            }
        }

        [HttpPost("add")]
        public ActionResult AddHabilidadeToTalento([FromBody] AddTalentoHabilidadeDTO dto)
        {
            try
            {
                _service.AddHabilidadeToTalento(dto);
                return CreatedAtAction(nameof(GetHabilidadesByTalentoId), new { talentoId = dto.TalentoId }, dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar habilidade: {ex.Message}");
            }
        }

        [HttpPut("update/{talentoId}/{habilidadeId}")]
        public ActionResult UpdateTalentoHabilidade(int talentoId, int habilidadeId, [FromBody] UpdateTalentoHabilidadeDTO dto)
        {
            try
            {
                _service.UpdateTalentoHabilidade(talentoId, habilidadeId, dto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar habilidade: {ex.Message}");
            }
        }

        [HttpDelete("remove/{talentoId}/{habilidadeId}")]
        public ActionResult RemoveHabilidadeFromTalento(int talentoId, int habilidadeId)
        {
            try
            {
                _service.RemoveHabilidadeFromTalento(talentoId, habilidadeId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao remover habilidade: {ex.Message}");
            }
        }
    }
}