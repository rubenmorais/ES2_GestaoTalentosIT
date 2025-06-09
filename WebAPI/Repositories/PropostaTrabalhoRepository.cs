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
    public class PropostaTrabalhoRepository : IPropostaTrabalhoRepository
    {
        private readonly ApplicationDbContext _context;

        public PropostaTrabalhoRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<PropostaTrabalhoDTO> GetAll()
        {
            return _context.PropostasTrabalhos
                .Include(p => p.Categoria)
                .Include(p => p.Estado)
                .Select(p => new PropostaTrabalhoDTO
                {
                    PropostaId = p.Propostaid,
                    UtilizadorId = p.Utilizadorid,
                    ClienteId = p.Clienteid,
                    Nome = p.Nome,
                    CategoriaId = p.Categoriaid,
                    NomeCategoria = p.Categoria.Categoria,
                    TotalHoras = p.TotalHoras,
                    Descricao = p.Descricao,
                    EstadoId = p.Estadoid,
                    NomeEstado = p.Estado.Estado1 
                })
                .ToList();
        }

        public PropostaTrabalhoDTO GetById(int id)
        {
            return _context.PropostasTrabalhos
                .Include(p => p.Categoria)
                .Include(p => p.Estado)
                .Where(p => p.Propostaid == id)
                .Select(p => new PropostaTrabalhoDTO
                {
                    PropostaId = p.Propostaid,
                    UtilizadorId = p.Utilizadorid,
                    ClienteId = p.Clienteid,
                    Nome = p.Nome,
                    CategoriaId = p.Categoriaid,
                    NomeCategoria = p.Categoria.Categoria,
                    TotalHoras = p.TotalHoras,
                    Descricao = p.Descricao,
                    EstadoId = p.Estadoid,
                    NomeEstado = p.Estado.Estado1
                })
                .FirstOrDefault();
        }

        public PropostasTrabalho Create(CreatePropostaTrabalhoDTO dto)
        {
            var utilizadorExiste = _context.Utilizadores.Any(u => u.Utilizadorid == dto.UtilizadorId);
            if (!utilizadorExiste)
            {
                throw new ArgumentException("O UtilizadorId fornecido não existe.");
            }
            
            var clienteExiste = _context.Clientes.Any(c => c.Clienteid == dto.ClienteId);
            if (!clienteExiste)
            {
                throw new ArgumentException("O ClienteId fornecido não existe.");
            }
            
            var categoriaExiste = _context.CategoriasProfissionais.Any(c => c.Categoriaid == dto.CategoriaId);
            if (!categoriaExiste)
            {
                throw new ArgumentException("A CategoriaId fornecida não existe.");
            }
            
            if (!_context.Estados.Any())
            {
                var estadosPadrao = new List<Estado>
                {
                    new Estado { Estadoid = 1, Estado1 = "Ativo" },
                    new Estado { Estadoid = 2, Estado1 = "Pendente" },
                    new Estado { Estadoid = 3, Estado1 = "Cancelado" },
                };

                _context.Estados.AddRange(estadosPadrao);
                _context.SaveChanges();
            }
            
            var proposta = new PropostasTrabalho
            {
                Utilizadorid = dto.UtilizadorId,
                Clienteid = dto.ClienteId,
                Nome = dto.Nome,
                Categoriaid = dto.CategoriaId,
                TotalHoras = dto.TotalHoras,
                Descricao = dto.Descricao,
                Estadoid = 1,
            };

            _context.PropostasTrabalhos.Add(proposta);
            _context.SaveChanges();

            return proposta;
        }

        public PropostaTrabalhoDTO Update(int id, UpdatePropostaTrabalhoDTO dto)
        {
            var proposta = _context.PropostasTrabalhos.FirstOrDefault(p => p.Propostaid == id);
            if (proposta == null)
            {
                throw new Exception($"Proposta com ID {id} não encontrada.");
            }

            var categoriaExiste = _context.CategoriasProfissionais.Any(c => c.Categoriaid == dto.CategoriaId);
            if (!categoriaExiste)
            {
                throw new ArgumentException("A CategoriaId fornecida não existe.");
            }
            var estadoExiste = _context.Estados.Any(e => e.Estadoid == dto.EstadoId);
            if (!estadoExiste)
            {
                throw new ArgumentException("O EstadoId fornecido não existe.");
            }

            proposta.Nome = dto.Nome;
            proposta.Categoriaid = dto.CategoriaId;
            proposta.Clienteid = dto.ClienteId;
            proposta.TotalHoras = dto.TotalHoras;
            proposta.Descricao = dto.Descricao;
            proposta.Estadoid = dto.EstadoId;
            
            _context.SaveChanges();

            return GetById(id);
        }

        public void Delete(int id)
        {
            var proposta = _context.PropostasTrabalhos.FirstOrDefault(p => p.Propostaid == id);
            if (proposta == null)
            {
                throw new Exception($"Proposta com ID {id} não encontrada.");
            }

            _context.PropostasTrabalhos.Remove(proposta);
            _context.SaveChanges();
        }

        public IQueryable<PropostasTrabalho> PropostasTrabalhos 
            => _context.PropostasTrabalhos;
        
        public IQueryable<Talento> Talentos 
            => _context.Talentos;
    }
}