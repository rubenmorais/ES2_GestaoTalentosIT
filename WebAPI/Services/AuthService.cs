using WebAPI.DTOClasses;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class AuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
        }

        public string AuthenticateAndGenerateToken(LoginDTO loginDto)
        {
            return _authRepository.AuthenticateAndGenerateToken(loginDto);
        }
    }
}