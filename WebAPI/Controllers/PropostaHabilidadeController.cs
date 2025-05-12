using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using WebAPI.DTOClasses;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropostaHabilidadeController : ControllerBase
    {
        private readonly PropostaHabilidadeService _service;

        public PropostaHabilidadeController(PropostaHabilidadeService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("proposta/{propostaId}")]
        public ActionResult<List<PropostaHabilidadeDTO>> GetHabilidadesByPropostaId(int propostaId)
        {
            try
            {
                var habilidades = _service.GetHabilidadesByPropostaId(propostaId);
                return Ok(habilidades);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao recuperar habilidades requeridas: {ex.Message}");
            }
        }

        [HttpPost("add")]
        public ActionResult AddHabilidadeToProposta([FromBody] AddPropostaHabilidadeDTO dto)
        {
            try
            {
                _service.AddHabilidadeToProposta(dto);
                return CreatedAtAction(nameof(GetHabilidadesByPropostaId), new { propostaId = dto.PropostaId }, dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar habilidade requerida: {ex.Message}");
            }
        }

        [HttpPut("update/{propostaId}/{habilidadeId}")]
        public ActionResult UpdatePropostaHabilidade(int propostaId, int habilidadeId, [FromBody] UpdatePropostaHabilidadeDTO dto)
        {
            try
            {
                _service.UpdatePropostaHabilidade(propostaId, habilidadeId, dto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar habilidade requerida: {ex.Message}");
            }
        }

        [HttpDelete("remove/{propostaId}/{habilidadeId}")]
        public ActionResult RemoveHabilidadeFromProposta(int propostaId, int habilidadeId)
        {
            try
            {
                _service.RemoveHabilidadeFromProposta(propostaId, habilidadeId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao remover habilidade requerida: {ex.Message}");
            }
        }
    }
}