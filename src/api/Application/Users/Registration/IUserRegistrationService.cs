using System.Threading.Tasks;
using Application.Users.Registration.Models;

namespace Application.Users.Registration
{
    public interface IUserRegistrationService
    {
        Task<UserRegistrationResponse> Register(UserRegistrationRequest request);
    }
}
