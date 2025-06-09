using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using WebAPI.DTOClasses;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropostaTrabalhoController : ControllerBase
    {
        private readonly PropostaTrabalhoService _propostaService;

        public PropostaTrabalhoController(PropostaTrabalhoService propostaService)
        {
            _propostaService = propostaService;
        }
        
        [HttpGet("index")]
        public ActionResult<List<PropostaTrabalhoDTO>> GetAllPropostas()
        {
            try
            {
                return _propostaService.GetAllPropostas(); 
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao recuperar propostas de trabalho: {ex.Message}"); 
            }
        }
        
        [HttpPost("create")]
        public ActionResult CreateProposta([FromBody] CreatePropostaTrabalhoDTO createPropostaDTO)
        {
            try
            {
                var proposta = _propostaService.CriarProposta(createPropostaDTO);
                
                return CreatedAtAction(nameof(GetPropostaPorId), new { id = proposta.Propostaid }, createPropostaDTO); 
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar a proposta de trabalho: {ex.Message}"); 
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdatePropostaTrabalhoDTO dto)
        {
            try
            {
                var updatedProposta = _propostaService.UpdateProposta(id, dto);
                return Ok(updatedProposta);
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
        public ActionResult GetPropostaPorId(int id)
        {
            try
            {
                var proposta = _propostaService.GetPropostaPorId(id); 
                
                if (proposta == null)
                    return NotFound($"Proposta de trabalho com ID {id} não encontrada.");

                return Ok(proposta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao procurar proposta de trabalho: {ex.Message}"); 
            }
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeleteProposta(int id)
        {
            try
            {
                _propostaService.DeleteProposta(id); 
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return NotFound($"Erro ao apagar proposta de trabalho: {ex.Message}");
            }
        }
        
        [HttpGet("{id}/talentos-elegiveis")]
        public ActionResult<List<TalentosElegiveisDTO>> GetTalentosElegiveis(int id)
        {
            try
            {
                var lista = _propostaService.GetTalentosElegiveis(id)
                            ?? new List<TalentosElegiveisDTO>();
                return Ok(lista);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao recuperar talentos elegíveis: {ex.Message}");
            }
        }
        
        
    }
}