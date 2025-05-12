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
    public class PropostaHabilidadeRepository : IPropostaHabilidadeRepository
    {
        private readonly ApplicationDbContext _context;

        public PropostaHabilidadeRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<PropostaHabilidadeDTO> GetHabilidadesByPropostaId(int propostaId)
        {
            // Verificar primeiro se a proposta existe
            var propostaExiste = _context.PropostasTrabalhos.Any(p => p.Propostaid == propostaId);
            if (!propostaExiste)
            {
                throw new ArgumentException($"Proposta com ID {propostaId} não encontrada.");
            }

            return _context.PropostasHabilidades
                .Where(ph => ph.Propostaid == propostaId)
                .Include(ph => ph.Habilidade)
                .Select(ph => new PropostaHabilidadeDTO
                {
                    PropostaId = ph.Propostaid,
                    HabilidadeId = ph.Habilidadeid,
                    NomeHabilidade = ph.Habilidade.Nome,
                    MinAnosExp = ph.MinAnosExp
                })
                .ToList();
        }

        public void AddHabilidadeToProposta(AddPropostaHabilidadeDTO dto)
        {
            // Verificar se a proposta existe
            var propostaExiste = _context.PropostasTrabalhos.Any(p => p.Propostaid == dto.PropostaId);
            if (!propostaExiste)
            {
                throw new ArgumentException($"Proposta com ID {dto.PropostaId} não encontrada.");
            }

            // Verificar se a habilidade existe
            var habilidadeExiste = _context.Habilidades.Any(h => h.Habilidadeid == dto.HabilidadeId);
            if (!habilidadeExiste)
            {
                throw new ArgumentException($"Habilidade com ID {dto.HabilidadeId} não encontrada.");
            }

            // Verificar se a relação já existe
            var relacaoExiste = _context.PropostasHabilidades.Any(
                ph => ph.Propostaid == dto.PropostaId && ph.Habilidadeid == dto.HabilidadeId);
            if (relacaoExiste)
            {
                throw new ArgumentException("Esta habilidade já está associada a esta proposta.");
            }

            // Criar a relação
            var propostaHabilidade = new PropostasHabilidade
            {
                Propostaid = dto.PropostaId,
                Habilidadeid = dto.HabilidadeId,
                MinAnosExp = dto.MinAnosExp
            };

            _context.PropostasHabilidades.Add(propostaHabilidade);
            _context.SaveChanges();
        }

        public void UpdatePropostaHabilidade(int propostaId, int habilidadeId, UpdatePropostaHabilidadeDTO dto)
        {
            var propostaHabilidade = _context.PropostasHabilidades
                .FirstOrDefault(ph => ph.Propostaid == propostaId && ph.Habilidadeid == habilidadeId);

            if (propostaHabilidade == null)
            {
                throw new ArgumentException("Relação proposta-habilidade não encontrada.");
            }

            propostaHabilidade.MinAnosExp = dto.MinAnosExp;
            _context.SaveChanges();
        }

        public void RemoveHabilidadeFromProposta(int propostaId, int habilidadeId)
        {
            var propostaHabilidade = _context.PropostasHabilidades
                .FirstOrDefault(ph => ph.Propostaid == propostaId && ph.Habilidadeid == habilidadeId);

            if (propostaHabilidade == null)
            {
                throw new ArgumentException("Relação proposta-habilidade não encontrada.");
            }

            _context.PropostasHabilidades.Remove(propostaHabilidade);
            _context.SaveChanges();
        }
    }
}