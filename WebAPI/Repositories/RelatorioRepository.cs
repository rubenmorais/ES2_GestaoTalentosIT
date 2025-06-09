using DbLayer.Context;
using DbLayer.Models;
using WebAPI.DTOClasses;
using WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Repositories
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly ApplicationDbContext _context;
        private const int HORAS_POR_MES = 176;

        public RelatorioRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<RelatorioPrecoMedioDTO> GetRelatorioPrecoMedio(
            decimal? minPrecoHora = null,
            decimal? maxPrecoHora = null
        )
        {
            var query = _context.Talentos
                .Include(t => t.TalentosHabilidades)
                    .ThenInclude(th => th.Habilidade)
                        .ThenInclude(h => h.Categoria)
                .AsQueryable();

            if (minPrecoHora.HasValue)
                query = query.Where(t => t.PrecoHora >= minPrecoHora.Value);

            if (maxPrecoHora.HasValue)
                query = query.Where(t => t.PrecoHora <= maxPrecoHora.Value);

            var resultado = query
                .GroupBy(t => new 
                {
                    Categoria = t.TalentosHabilidades
                        .Select(th => th.Habilidade.Categoria.Categoria)
                        .FirstOrDefault() 
                        ?? "Sem Categoria",
                    t.Pais
                })
                .Select(g => new RelatorioPrecoMedioDTO
                {
                    Categoria          = g.Key.Categoria,
                    Pais               = g.Key.Pais,
                    PrecoMedioMensal   = g.Average(t => t.PrecoHora * HORAS_POR_MES),
                    QuantidadeTalentos = g.Count()
                })
                .OrderBy(r => r.Categoria)
                .ThenBy(r => r.Pais)
                .ToList();

            return resultado;
        }
    }
}
