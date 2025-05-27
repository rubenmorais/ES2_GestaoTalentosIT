using DbLayer.Context;
using DbLayer.Models;
using WebAPI.DTOClasses;
using WebAPI.Interfaces;

namespace WebAPI.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<ClienteDTO> GetAll()
        {
            try
            {
                return _context.Clientes
                    .Select(c => new ClienteDTO
                    {
                        ClienteId = c.Clienteid,
                        UtilizadorId = c.Utilizadorid,
                        Nome = c.Nome,
                        Email = c.Email,
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ClienteDTO GetById(int id)
        {
            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.Clienteid == id);
                if (cliente == null)
                {
                    throw new Exception("Cliente não encontrado.");
                }

                return new ClienteDTO
                {
                    ClienteId = cliente.Clienteid,
                    UtilizadorId = cliente.Utilizadorid,
                    Nome = cliente.Nome,
                    Email = cliente.Email,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Create(CreateClienteDTO dto)
        {
            try
            {
                var utilizadorExistente = _context.Utilizadores
                    .FirstOrDefault(u => u.Utilizadorid == dto.UtilizadorId);

                if (utilizadorExistente == null)
                {
                    throw new Exception("O utilizador com o ID fornecido não existe.");
                }

                var cliente = new Cliente
                {
                    Utilizadorid = dto.UtilizadorId,
                    Nome = dto.Nome,
                    Email = dto.Email
                };

                _context.Clientes.Add(cliente);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar cliente: " + ex.Message);
            }
        }
        
        public void Update(int id, UpdateClienteDTO dto)
        {
            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.Clienteid == id);
                if (cliente == null)
                {
                    throw new Exception("Cliente não encontrado.");
                }

                cliente.Nome = dto.Nome;
                cliente.Email = dto.Email;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar cliente: " + ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.Clienteid == id);
                if (cliente == null)
                {
                    throw new Exception("Cliente não encontrado.");
                }
                
                var tabelasAssociadas = new (string Tabela, Func<int, bool> Verificar)[]
                {
                    ("PropostasTrabalho", (clienteId) => 
                        _context.PropostasTrabalhos.Any(pt => pt.Clienteid == clienteId)),
                };

                foreach (var tabela in tabelasAssociadas)
                {
                    if (tabela.Verificar(id))
                    {
                        throw new Exception(
                            $"O cliente não pode ser apagado, pois possui registos associados em '{tabela.Tabela}'.");
                    }
                }

                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao apagar cliente: " + ex.Message, ex);
            }
        }
    }
}