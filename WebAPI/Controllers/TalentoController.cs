using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using WebAPI.DTOClasses;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalentoController : ControllerBase
    {
        private readonly TalentoService _talentoService;

        public TalentoController(TalentoService talentoService)
        {
            _talentoService = talentoService;
        }
        
        [HttpGet("index")]
        public ActionResult<List<TalentoDTO>> GetAllTalentos()
        {
            try
            {
                return _talentoService.GetAllTalentos(); 
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao recuperar talentos: {ex.Message}"); 
            }
        }
        
        [HttpPost("create")]
        public ActionResult CreateTalento([FromBody] CreateTalentoDTO createTalentoDTO)
        {
            try
            {
                var talento = _talentoService.CriarTalento(createTalentoDTO);
                
                return CreatedAtAction(nameof(GetTalentoPorId), new { id = talento.Talentoid }, createTalentoDTO); 
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar o talento: {ex.Message}"); 
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateTalentoDTO dto)
        {
            try
            {
                var updatedTalento = _talentoService.UpdateTalento(id, dto);
                return Ok(updatedTalento);
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
        public ActionResult GetTalentoPorId(int id)
        {
            try
            {
                var talento = _talentoService.GetTalentoPorId(id); 
                
                if (talento == null)
                    return NotFound($"Talento com ID {id} não encontrado.");

                return Ok(talento);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao procurar talento: {ex.Message}"); 
            }
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeleteTalento(int id)
        {
            try
            {
                _talentoService.DeleteTalento(id); 
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return NotFound($"Erro ao apagar talento: {ex.Message}");
            }
        }
    }
}
