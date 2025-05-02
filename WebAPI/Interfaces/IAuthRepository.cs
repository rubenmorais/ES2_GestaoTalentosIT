using WebAPI.DTOClasses;

namespace WebAPI.Interfaces
{
    public interface IAuthRepository
    {
        string AuthenticateAndGenerateToken(LoginDTO loginDto);
    }
}