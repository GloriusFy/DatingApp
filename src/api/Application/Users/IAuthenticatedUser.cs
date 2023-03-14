using Application.Users.Registration.Models;

namespace Application.Users
{
    public interface IAuthenticatedUser
    {
        public int Id { get; }

        public GenderDto Gender { get; set; }
    }
}
