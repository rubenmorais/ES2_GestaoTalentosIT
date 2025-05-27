using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.DTOClasses;
using System.Collections.Generic;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly EstadoService _estadoService;

        public EstadosController(EstadoService estadoService)
        {
            _estadoService = estadoService;
        }
        
        [HttpGet]
        public ActionResult<List<EstadoDTO>> GetEstados()
        {
            var estados = _estadoService.GetAllEstados();
            return Ok(estados);
        }
    }
}