using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebAPI.DTOClasses;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropostaTalentoController : ControllerBase
    {
        private readonly PropostaTalentoService _service;

        public PropostaTalentoController(PropostaTalentoService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("proposta/{propostaId}")]
        public ActionResult<List<PropostaTalentoDTO>> GetTalentosByPropostaId(int propostaId)
        {
            try
            {
                var talentos = _service.GetTalentosByPropostaId(propostaId);
                return Ok(talentos);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao recuperar talentos: {ex.Message}");
            }
        }

        [HttpPost("add")]
        public ActionResult AddTalentoToProposta([FromBody] AddPropostaTalentoDTO dto)
        {
            try
            {
                _service.AddTalentoToProposta(dto);
                return CreatedAtAction(nameof(GetTalentosByPropostaId), new { propostaId = dto.PropostaId }, dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar talento: {ex.Message}");
            }
        }

        [HttpDelete("remove/{propostaId}/{talentoId}")]
        public ActionResult RemoveTalentoFromProposta(int propostaId, int talentoId)
        {
            try
            {
                _service.RemoveTalentoFromProposta(propostaId, talentoId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao remover talento: {ex.Message}");
            }
        }
    }
}
