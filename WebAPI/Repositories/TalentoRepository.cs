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
    public class TalentoRepository : ITalentoRepository
    {
        private readonly ApplicationDbContext _context;

        public TalentoRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<TalentoDTO> GetAll()
        {
            return _context.Talentos
                .Include(t => t.TalentosHabilidades)
                    .ThenInclude(th => th.Habilidade)
                .Select(t => new TalentoDTO
                {
                    Talentoid = t.Talentoid,
                    UtilizadorId = t.Utilizadorid,
                    Nome = t.Nome,
                    Pais = t.Pais,
                    Email = t.Email,
                    PrecoPorHora = t.PrecoHora,
                    Visibilidade = t.Visibilidade,
                    Habilidades = t.TalentosHabilidades.Select(th => new TalentoHabilidadeDTO
                    {
                        TalentoId = th.Talentoid,
                        HabilidadeId = th.Habilidadeid,
                        NomeHabilidade = th.Habilidade.Nome,
                        AnosExperiencia = th.AnosExperiencia
                    }).ToList()
                })
                .ToList();
        }

        public TalentoDTO? GetById(int id)
        {
            return _context.Talentos
                .Include(t => t.TalentosHabilidades)
                    .ThenInclude(th => th.Habilidade)
                .Where(t => t.Talentoid == id)
                .Select(t => new TalentoDTO
                {
                    Talentoid = t.Talentoid,
                    UtilizadorId = t.Utilizadorid,
                    Nome = t.Nome,
                    Pais = t.Pais,
                    Email = t.Email,
                    PrecoPorHora = t.PrecoHora,
                    Visibilidade = t.Visibilidade,
                    Habilidades = t.TalentosHabilidades.Select(th => new TalentoHabilidadeDTO
                    {
                        TalentoId = th.Talentoid,
                        HabilidadeId = th.Habilidadeid,
                        NomeHabilidade = th.Habilidade.Nome,
                        AnosExperiencia = th.AnosExperiencia
                    }).ToList()
                })
                .FirstOrDefault();
        }

        public Talento Create(CreateTalentoDTO dto)
        {
            var utilizadorExiste = _context.Utilizadores.Any(u => u.Utilizadorid == dto.UtilizadorId);
            if (!utilizadorExiste)
            {
                throw new ArgumentException("O UtilizadorId fornecido não existe.");
            }
            
            var emailExiste = _context.Talentos.Any(t => t.Email == dto.Email);
            if (emailExiste)
            {
                throw new ArgumentException("O e-mail fornecido já está associado a outro talento.");
            }

            var talento = new Talento
            {
                Utilizadorid = dto.UtilizadorId,
                Nome = dto.Nome,
                Pais = dto.Pais,
                Email = dto.Email,
                PrecoHora = dto.PrecoPorHora,
                Visibilidade = dto.Visibilidade
            };

            _context.Talentos.Add(talento);
            _context.SaveChanges();

            return talento;
        }

        public TalentoDTO Update(int id, UpdateTalentoDTO dto)
        {
            var talento = _context.Talentos
                .Include(t => t.TalentosHabilidades)
                .ThenInclude(th => th.Habilidade)
                .FirstOrDefault(t => t.Talentoid == id);
                
            if (talento == null)
            {
                throw new Exception($"Talento com ID {id} não encontrado.");
            }

            if (dto.Email != talento.Email)
            {
                var emailExiste = _context.Talentos.Any(t => t.Email == dto.Email && t.Talentoid != id);
                if (emailExiste)
                {
                    throw new ArgumentException("O e-mail fornecido já está associado a outro talento.");
                }
                talento.Email = dto.Email;
            }

            talento.Nome = dto.Nome;
            talento.Pais = dto.Pais;
            talento.PrecoHora = dto.PrecoPorHora;
            talento.Visibilidade = dto.Visibilidade;

            _context.SaveChanges();

            return new TalentoDTO
            {
                Talentoid = talento.Talentoid,
                UtilizadorId = talento.Utilizadorid,
                Nome = talento.Nome,
                Pais = talento.Pais,
                Email = talento.Email,
                PrecoPorHora = talento.PrecoHora,
                Visibilidade = talento.Visibilidade,
                Habilidades = talento.TalentosHabilidades.Select(th => new TalentoHabilidadeDTO
                {
                    TalentoId = th.Talentoid,
                    HabilidadeId = th.Habilidadeid,
                    NomeHabilidade = th.Habilidade.Nome,
                    AnosExperiencia = th.AnosExperiencia
                }).ToList()
            };
        }

        public void Delete(int id)
        {
            var talento = _context.Talentos.FirstOrDefault(t => t.Talentoid == id);
            if (talento == null)
            {
                throw new Exception($"Talento com ID {id} não encontrado.");
            }

            _context.Talentos.Remove(talento);
            _context.SaveChanges();
        }
    }
}