using Microsoft.AspNetCore.Authorization;
using WebAPI.Services;
using WebAPI.DTOClasses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizadorController : ControllerBase
    {
        private readonly UtilizadorService _utilizadorService;
        
        public UtilizadorController(UtilizadorService utilizadorService)
        {
            _utilizadorService = utilizadorService;
        }
        
        [HttpGet("index")]
        public ActionResult<List<UtilizadorDTO>> GetAllUsers()
        {
            try
            {
                return _utilizadorService.GetAllUsers();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
        
        [HttpPost("create")]
        public ActionResult CreateUtilizador([FromBody] CreateUtilizadorDTO createUtilizadorDTO)
        {
            try
            {
                _utilizadorService.CreateUtilizador(createUtilizadorDTO);
                return CreatedAtAction(nameof(GetAllUsers), new { nome = createUtilizadorDTO.Nome }, createUtilizadorDTO);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Já existe um utilizador com esse e-mail"))
                {
                    return Conflict(ex.Message); 
                }
                return BadRequest($"Erro ao criar o utilizador: {ex.Message}"); 
            }
        }
        
        [HttpPut("{id}")]
        public ActionResult UpdateUtilizador(int id, [FromBody] UpdateUtilizadorDTO updateUtilizadorDTO)
        {
            try
            {
                _utilizadorService.UpdateUtilizador(id, updateUtilizadorDTO);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Este e-mail já está a ser utilizado"))
                {
                    return Conflict(ex.Message); 
                }
                if (ex.Message.Contains("Utilizador não encontrado"))
                {
                    return NotFound(ex.Message); 
                }
                return BadRequest($"Erro ao atualizar o utilizador: {ex.Message}");
            }
        }
        
        [HttpPut("admin/{id}")]
        public ActionResult UpdateUtilizadorAdmin(int id, [FromBody] UpdateUtilizadorAdminDTO updateUtilizadorDTO)
        {
            try
            {
                _utilizadorService.UpdateUtilizadorAdmin(id, updateUtilizadorDTO);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Este e-mail já está a ser utilizado"))
                {
                    return Conflict(ex.Message); 
                }
                if (ex.Message.Contains("Utilizador não encontrado"))
                {
                    return NotFound(ex.Message); 
                }
                return BadRequest($"Erro ao atualizar o utilizador: {ex.Message}");
            }
        }
        
        [HttpGet("{id}")]
        public ActionResult<UtilizadorDTO> GetUserById(int id)
        {
            try
            {
                var utilizadorDTO = _utilizadorService.GetUserById(id);
                return Ok(utilizadorDTO);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Utilizador não encontrado.")
                {
                    return NotFound(ex.Message);  
                }
                return BadRequest($"Erro ao recuperar o utilizador: {ex.Message}"); 
            }
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeleteUtilizador(int id)
        {
            try
            {
                _utilizadorService.DeleteUtilizador(id);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return NotFound($"{ex.Message}"); 
            }
        }
        
        [HttpGet("isadmin/{id}")]
        public ActionResult<bool> IsAdmin(int id)
        {
            try
            {
                bool isAdmin = _utilizadorService.IsAdmin(id);
                return Ok(isAdmin); 
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao verificar se o utilizador é admin: {ex.Message}");
            }
        }
    }
}
