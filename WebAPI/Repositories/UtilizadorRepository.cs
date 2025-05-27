using DbLayer.Context;
using DbLayer.Models;
using WebAPI.DTOClasses;
using WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace WebAPI.Repositories
{
    public class UtilizadorRepository : IUtilizadorRepository
    {
        private readonly ApplicationDbContext _context;

        public UtilizadorRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            EnsureTiposExist();    
            EnsureAdminExists();
        }
        private void EnsureAdminExists()
        {
            if (!_context.Utilizadores.Any())  
            {
                var adminTipo = _context.Tipos.FirstOrDefault(t => t.Tipoid == 1);
                if (adminTipo == null)
                {
                    throw new Exception("Tipo de utilizador admin não encontrado na base de dados.");
                }

                var admin = new Utilizadores
                {
                    Nome = "admin",
                    Email = "admin@admin.com",
                    Tipoid = adminTipo.Tipoid,
                    PalavraPasse = HashPassword("admin") 
                };

                _context.Utilizadores.Add(admin);
                _context.SaveChanges();
            }
        }
        private void EnsureTiposExist()
        {
            if (!_context.Tipos.Any())
            {
                var tipos = new List<Tipo>
                {
                    new Tipo { Tipoid = 1, Tipo1 = "Administrador" },
                    new Tipo { Tipoid = 2, Tipo1 = "Utilizador" },
                    new Tipo { Tipoid = 3, Tipo1 = "Gestor de utilizadores" }
                };

                _context.Tipos.AddRange(tipos);
                _context.SaveChanges();
            }
        }

        public List<UtilizadorDTO> GetAll()
        {
            try
            {
                return _context.Utilizadores
                    .Include(u => u.Tipo) 
                    .Select(utilizador => new UtilizadorDTO
                    {
                        Utilizadorid = utilizador.Utilizadorid,
                        Nome = utilizador.Nome,
                        Email = utilizador.Email,
                        Tipoid = utilizador.Tipoid,
                        Tipo = utilizador.Tipo.Tipo1 
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao recuperar os utilizadores.", ex);
            }
        }

        public UtilizadorDTO GetById(int id)
        {
            try
            {
                var utilizador = _context.Utilizadores
                    .Include(u => u.Tipo) 
                    .FirstOrDefault(u => u.Utilizadorid == id); 

                if (utilizador == null)
                {
                    throw new Exception("Utilizador não encontrado.");
                }

                return new UtilizadorDTO
                {
                    Utilizadorid = utilizador.Utilizadorid,
                    Nome = utilizador.Nome,
                    Email = utilizador.Email,
                    Tipoid = utilizador.Tipoid,
                    Tipo = utilizador.Tipo.Tipo1
                };
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Utilizador não encontrado"))
                {
                    throw new Exception("Utilizador não encontrado.");
                }
                else
                {
                    throw new Exception("Ocorreu um erro ao recuperar o utilizador.", ex);
                }
            }
        }

        public void Create(CreateUtilizadorDTO createUtilizadorDTO)
        {
            try
            {
                var existingUser = _context.Utilizadores.FirstOrDefault(u => u.Email == createUtilizadorDTO.Email);
                if (existingUser != null)
                {
                    throw new Exception("Já existe um utilizador com esse e-mail.");
                }
                
                int tipoId = createUtilizadorDTO.Tipoid == 0 ? 2 : createUtilizadorDTO.Tipoid;
                
                var tipoExistente = _context.Tipos.FirstOrDefault(t => t.Tipoid == createUtilizadorDTO.Tipoid);
                if (tipoExistente == null)
                {
                    throw new Exception("Tipo de utilizador inválido.");
                }

                var hashedPassword = HashPassword(createUtilizadorDTO.PalavraPasse);

                var utilizador = new Utilizadores
                {
                    Nome = createUtilizadorDTO.Nome,
                    Email = createUtilizadorDTO.Email,
                    Tipoid = tipoId,
                    PalavraPasse = hashedPassword
                };

                _context.Utilizadores.Add(utilizador);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int utilizadorId, UpdateUtilizadorDTO updateUtilizadorDTO)
        {
            try
            {
                var utilizador = _context.Utilizadores.FirstOrDefault(u => u.Utilizadorid == utilizadorId);
        
                if (utilizador == null)
                {
                    throw new Exception("Utilizador não encontrado.");
                }
                
                var existingEmailUser = _context.Utilizadores
                    .FirstOrDefault(u => u.Email == updateUtilizadorDTO.Email && u.Utilizadorid != utilizadorId);

                if (existingEmailUser != null)
                {
                    throw new Exception("Este e-mail já está a ser utilizado por outro utilizador.");
                }
                
                var tipoExistente = _context.Tipos.FirstOrDefault(t => t.Tipoid == updateUtilizadorDTO.Tipoid);
                if (tipoExistente == null)
                {
                    throw new Exception("Tipo de utilizador inválido.");
                }
                
                utilizador.Nome = updateUtilizadorDTO.Nome;
                utilizador.Email = updateUtilizadorDTO.Email;
                utilizador.Tipoid = updateUtilizadorDTO.Tipoid;
                
                if (!string.IsNullOrEmpty(updateUtilizadorDTO.PalavraPasse))
                {
                    utilizador.PalavraPasse = HashPassword(updateUtilizadorDTO.PalavraPasse);
                }
                
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar o utilizador: {ex.Message}", ex);
            }
        }

        public void UpdateAdmin(int utilizadorId, UpdateUtilizadorAdminDTO updateUtilizadorDTO)
        {
            try
            {
                var utilizador = _context.Utilizadores.FirstOrDefault(u => u.Utilizadorid == utilizadorId);
        
                if (utilizador == null)
                {
                    throw new Exception("Utilizador não encontrado.");
                }
                
                var existingEmailUser = _context.Utilizadores
                    .FirstOrDefault(u => u.Email == updateUtilizadorDTO.Email && u.Utilizadorid != utilizadorId);

                if (existingEmailUser != null)
                {
                    throw new Exception("Este e-mail já está a ser utilizado por outro utilizador.");
                }
                
                var tipoExistente = _context.Tipos.FirstOrDefault(t => t.Tipoid == updateUtilizadorDTO.Tipoid);
                if (tipoExistente == null)
                {
                    throw new Exception("Tipo de utilizador inválido.");
                }
                
                utilizador.Nome = updateUtilizadorDTO.Nome;
                utilizador.Email = updateUtilizadorDTO.Email;
                utilizador.Tipoid = updateUtilizadorDTO.Tipoid;
                
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar o utilizador: {ex.Message}", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var utilizador = _context.Utilizadores.FirstOrDefault(u => u.Utilizadorid == id);
                if (utilizador == null)
                {
                    throw new Exception("Utilizador não encontrado.");
                }
                
                var tabelasAssociadas = new (string Tabela, Func<int, bool> Verificar)[]
                {
                    ("Habilidades", (id) => _context.Habilidades.Any(h => h.Criadorid == id)),
                    ("Talentos", (id) => _context.Talentos.Any(t => t.Utilizadorid == id)),
                    ("PropostasTrabalho", (id) => _context.PropostasTrabalhos.Any(pt => pt.Utilizadorid == id)),
                    ("Cliente", (id) => _context.Clientes.Any(pt => pt.Utilizadorid == id)),
                };
                
                foreach (var tabela in tabelasAssociadas)
                {
                    if (tabela.Verificar(id))
                    {
                        throw new Exception($"O utilizador não pode ser apagado, pois possui registos associados em {tabela.Tabela}.");
                    }
                }
                
                _context.Utilizadores.Remove(utilizador);
                _context.SaveChanges(); 
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Erro interno: {ex.InnerException.Message}");
                }

                throw new Exception($"Erro ao apagar utilizador: {ex.Message}", ex);
            }
        }

        public bool IsAdmin(int utilizadorId)
        {
            try
            {
                var utilizador = _context.Utilizadores
                    .Include(u => u.Tipo) 
                    .FirstOrDefault(u => u.Utilizadorid == utilizadorId);

                if (utilizador == null)
                {
                    throw new Exception($"Utilizador com ID {utilizadorId} não encontrado.");
                }
                
                return utilizador.Tipoid == 1; 
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao verificar se o utilizador é admin.", ex);
            }
        }

        public bool VerifyPassword(string providedPassword, string storedPasswordHash)
        {
            var providedPasswordHash = HashPassword(providedPassword);
            return providedPasswordHash == storedPasswordHash;
        }

        public UpdateUtilizadorDTO GetUpdateUtilizadorDTO(int id)
        {
            var utilizador = _context.Utilizadores.FirstOrDefault(u => u.Utilizadorid == id);
            if (utilizador == null) return null;

            return new UpdateUtilizadorDTO
            {
                Nome = utilizador.Nome,
                Email = utilizador.Email,
                Tipoid = utilizador.Tipoid,
                PalavraPasse = ""
            };
        }

        private string HashPassword(string password)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha512.ComputeHash(passwordBytes);

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}