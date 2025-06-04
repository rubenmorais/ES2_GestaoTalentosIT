using DbLayer.Context;
using DbLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.DTOClasses;
using WebAPI.Interfaces;

namespace WebAPI.Repositories
{
    public class PropostaTalentoRepository : IPropostaTalentoRepository
    {
        private readonly ApplicationDbContext _context;

        public PropostaTalentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PropostaTalentoDTO> GetTalentosByPropostaId(int propostaId)
        {
            var proposta = _context.PropostasTrabalhos
                .Include(p => p.Talentos)
                .FirstOrDefault(p => p.Propostaid == propostaId);

            if (proposta == null)
                throw new ArgumentException($"Proposta com ID {propostaId} não encontrada.");

            return proposta.Talentos.Select(t => new PropostaTalentoDTO
            {
                PropostaId = propostaId,
                TalentoId = t.Talentoid,
                NomeTalento = t.Nome
            }).ToList();
        }

        public void AddTalentoToProposta(AddPropostaTalentoDTO dto)
        {
            var proposta = _context.PropostasTrabalhos
                .Include(p => p.Talentos)
                .FirstOrDefault(p => p.Propostaid == dto.PropostaId);

            if (proposta == null)
                throw new ArgumentException($"Proposta com ID {dto.PropostaId} não encontrada.");

            var talento = _context.Talentos.Find(dto.TalentoId);
            if (talento == null)
                throw new ArgumentException($"Talento com ID {dto.TalentoId} não encontrado.");

            if (!proposta.Talentos.Contains(talento))
            {
                proposta.Talentos.Add(talento);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Talento já está associado à proposta.");
            }
        }

        public void RemoveTalentoFromProposta(int propostaId, int talentoId)
        {
            var proposta = _context.PropostasTrabalhos
                .Include(p => p.Talentos)
                .FirstOrDefault(p => p.Propostaid == propostaId);

            if (proposta == null)
                throw new ArgumentException($"Proposta com ID {propostaId} não encontrada.");

            var talento = _context.Talentos.Find(talentoId);
            if (talento == null)
                throw new ArgumentException($"Talento com ID {talentoId} não encontrado.");

            if (proposta.Talentos.Contains(talento))
            {
                proposta.Talentos.Remove(talento);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Talento não está associado à proposta.");
            }
        }
    }
}
