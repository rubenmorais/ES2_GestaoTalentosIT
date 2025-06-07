using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using WebAPI.DTOClasses;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly RelatorioService _relatorioService;

        public RelatorioController(RelatorioService relatorioService)
        {
            _relatorioService = relatorioService ?? throw new ArgumentNullException(nameof(relatorioService));
        }

        [HttpGet("preco-medio")]
        public ActionResult<List<RelatorioPrecoMedioDTO>> GetRelatorioPrecoMedio()
        {
            try
            {
                var relatorio = _relatorioService.GetRelatorioPrecoMedio();
                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao gerar relatório: {ex.Message}");
            }
        }
    }
} 