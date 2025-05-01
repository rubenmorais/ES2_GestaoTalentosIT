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
    public class TalentoHabilidadeRepository : ITalentoHabilidadeRepository
    {
        private readonly ApplicationDbContext _context;

        public TalentoHabilidadeRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<TalentoHabilidadeDTO> GetHabilidadesByTalentoId(int talentoId)
        {
            // Verificar primeiro se o talento existe
            var talentoExiste = _context.Talentos.Any(t => t.Talentoid == talentoId);
            if (!talentoExiste)
            {
                throw new ArgumentException($"Talento com ID {talentoId} não encontrado.");
            }

            return _context.TalentosHabilidades
                .Where(th => th.Talentoid == talentoId)
                .Include(th => th.Habilidade)
                .Select(th => new TalentoHabilidadeDTO
                {
                    TalentoId = th.Talentoid,
                    HabilidadeId = th.Habilidadeid,
                    NomeHabilidade = th.Habilidade.Nome,
                    AnosExperiencia = th.AnosExperiencia
                })
                .ToList();
        }

        public void AddHabilidadeToTalento(AddTalentoHabilidadeDTO dto)
        {
            // Verificar se o talento existe
            var talentoExiste = _context.Talentos.Any(t => t.Talentoid == dto.TalentoId);
            if (!talentoExiste)
            {
                throw new ArgumentException($"Talento com ID {dto.TalentoId} não encontrado.");
            }

            // Verificar se a habilidade existe
            var habilidadeExiste = _context.Habilidades.Any(h => h.Habilidadeid == dto.HabilidadeId);
            if (!habilidadeExiste)
            {
                throw new ArgumentException($"Habilidade com ID {dto.HabilidadeId} não encontrada.");
            }

            // Verificar se a relação já existe
            var relacaoExiste = _context.TalentosHabilidades.Any(
                th => th.Talentoid == dto.TalentoId && th.Habilidadeid == dto.HabilidadeId);
            if (relacaoExiste)
            {
                throw new ArgumentException("Esta habilidade já está associada a este talento.");
            }

            // Criar a relação
            var talentoHabilidade = new TalentosHabilidade
            {
                Talentoid = dto.TalentoId,
                Habilidadeid = dto.HabilidadeId,
                AnosExperiencia = dto.AnosExperiencia
            };

            _context.TalentosHabilidades.Add(talentoHabilidade);
            _context.SaveChanges();
        }

        public void UpdateTalentoHabilidade(int talentoId, int habilidadeId, UpdateTalentoHabilidadeDTO dto)
        {
            var talentoHabilidade = _context.TalentosHabilidades
                .FirstOrDefault(th => th.Talentoid == talentoId && th.Habilidadeid == habilidadeId);

            if (talentoHabilidade == null)
            {
                throw new ArgumentException("Relação talento-habilidade não encontrada.");
            }

            talentoHabilidade.AnosExperiencia = dto.AnosExperiencia;
            _context.SaveChanges();
        }

        public void RemoveHabilidadeFromTalento(int talentoId, int habilidadeId)
        {
            var talentoHabilidade = _context.TalentosHabilidades
                .FirstOrDefault(th => th.Talentoid == talentoId && th.Habilidadeid == habilidadeId);

            if (talentoHabilidade == null)
            {
                throw new ArgumentException("Relação talento-habilidade não encontrada.");
            }

            _context.TalentosHabilidades.Remove(talentoHabilidade);
            _context.SaveChanges();
        }
    }
}