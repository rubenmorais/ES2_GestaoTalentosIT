using DbLayer.Context;
using DbLayer.Models;
using WebAPI.DTOClasses;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace WebAPI.Services
{
    public class UtilizadorService
    {
        private readonly ApplicationDbContext _context;

        public UtilizadorService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public List<UtilizadorDTO> GetAllUsers()
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

        public void CreateUtilizador(CreateUtilizadorDTO createUtilizadorDTO)
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
        
        public void DeleteUtilizador(int id)
        {
            try
            {
                var utilizador = _context.Utilizadores.FirstOrDefault(u => u.Utilizadorid == id);
                if (utilizador == null)
                {
                    throw new Exception("Utilizador não encontrado.");
                }
                
                _context.Utilizadores.Remove(utilizador);
                _context.SaveChanges(); 
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao excluir o utilizador.", ex);
            }
        }

        public UtilizadorDTO? GetUserByEmail(string email)
        {
            var utilizador = _context.Utilizadores
                .Include(u => u.Tipo)
                .FirstOrDefault(u => u.Email == email);

            return utilizador == null ? null : new UtilizadorDTO
            {
                Utilizadorid = utilizador.Utilizadorid,
                Nome = utilizador.Nome,
                Email = utilizador.Email,
                Tipoid = utilizador.Tipoid,
                Tipo = utilizador.Tipo?.Tipo1
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

        public bool VerifyPassword(string providedPassword, string storedPasswordHash)
        {
            var providedPasswordHash = HashPassword(providedPassword);
            return providedPasswordHash == storedPasswordHash;
        }
    }
}
