using DTOs;

namespace Logica.Session
{
    public interface ISessionLogic
    {
        LoginResponse Login(LoginDto login);
    }
}
