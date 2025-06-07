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

        public List<RelatorioPrecoMedioDTO> GetRelatorioPrecoMedio()
        {
            return _context.Talentos
                .Include(t => t.TalentosHabilidades)
                    .ThenInclude(th => th.Habilidade)
                        .ThenInclude(h => h.Categoria)
                .Where(t => t.PrecoHora.HasValue)
                .GroupBy(t => new { 
                    Categoria = t.TalentosHabilidades
                        .Select(th => th.Habilidade.Categoria.Categoria)
                        .FirstOrDefault() ?? "Sem Categoria",
                    t.Pais 
                })
                .Select(g => new RelatorioPrecoMedioDTO
                {
                    Categoria = g.Key.Categoria,
                    Pais = g.Key.Pais,
                    PrecoMedioMensal = g.Average(t => t.PrecoHora.Value * HORAS_POR_MES),
                    QuantidadeTalentos = g.Count()
                })
                .OrderBy(r => r.Categoria)
                .ThenBy(r => r.Pais)
                .ToList();
        }
    }
} 