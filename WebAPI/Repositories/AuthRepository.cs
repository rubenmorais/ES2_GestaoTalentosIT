using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebAPI.DTOClasses;
using DbLayer.Context;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;

namespace WebAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUtilizadorRepository _utilizadorRepository;

        public AuthRepository(ApplicationDbContext context, IConfiguration configuration, IUtilizadorRepository utilizadorRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _utilizadorRepository = utilizadorRepository ?? throw new ArgumentNullException(nameof(utilizadorRepository));
        }

        public string AuthenticateAndGenerateToken(LoginDTO loginDto)
        {
            var utilizador = _context.Utilizadores
                .Include(u => u.Tipo)
                .FirstOrDefault(u => u.Email == loginDto.Email);

            if (utilizador == null || !_utilizadorRepository.VerifyPassword(loginDto.PalavraPasse, utilizador.PalavraPasse))
            {
                throw new UnauthorizedAccessException("Email ou senha inválidos.");
            }

            var userDto = new UtilizadorDTO
            {
                Utilizadorid = utilizador.Utilizadorid,
                Nome = utilizador.Nome,
                Email = utilizador.Email,
                Tipoid = utilizador.Tipoid,
                Tipo = utilizador.Tipo?.Tipo1
            };

            return GenerateJwtToken(userDto);
        }

        private string GenerateJwtToken(UtilizadorDTO utilizadorDTO)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, utilizadorDTO.Utilizadorid.ToString()),
                new Claim(ClaimTypes.Name, utilizadorDTO.Nome),
                new Claim(ClaimTypes.Email, utilizadorDTO.Email),
                new Claim("Tipoid", utilizadorDTO.Tipoid.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}