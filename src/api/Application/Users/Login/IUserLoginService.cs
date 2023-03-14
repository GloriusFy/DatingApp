using System.Threading.Tasks;
using Application.Users.Login.Models;

namespace Application.Users.Login
{
    public interface IUserLoginService
    {
        Task<UserLoginResponse?> Login(UserLoginRequest request);
    }
}
