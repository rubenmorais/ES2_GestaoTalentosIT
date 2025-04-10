using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using WebAPI.DTOClasses;
using System;
using System.Collections.Generic;
using WebAPI.DtoClasses;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;

        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService ?? throw new ArgumentNullException(nameof(categoriaService));
        }
        
        [HttpGet]
        public ActionResult<List<CategoriaDTO>> GetAll()
        {
            try
            {
                var categorias = _categoriaService.GetAllCategorias();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        public ActionResult<CategoriaDTO> GetById(int id)
        {
            try
            {
                var categoria = _categoriaService.GetCategoriaById(id);
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] CreateCategoriaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _categoriaService.CreateCategoria(dto);
                return Ok("Categoria criada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateCategoriaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _categoriaService.UpdateCategoria(id, dto);
                return Ok("Categoria atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _categoriaService.DeleteCategoria(id);
                return Ok("Categoria removida com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
    }
}
