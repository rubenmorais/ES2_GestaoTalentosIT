using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebAPI.DTOClasses;
using DbLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UtilizadorService _utilizadorService;

        public AuthService(ApplicationDbContext context, IConfiguration configuration, UtilizadorService utilizadorService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _utilizadorService = utilizadorService;
        }

        public string AuthenticateAndGenerateToken(LoginDTO loginDto)
        {
            var utilizador = _context.Utilizadores
                .Include(u => u.Tipo)
                .FirstOrDefault(u => u.Email == loginDto.Email);

            if (utilizador == null || !_utilizadorService.VerifyPassword(loginDto.PalavraPasse, utilizador.PalavraPasse))
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
