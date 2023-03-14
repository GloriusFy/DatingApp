using Domain.Aggregates.Users.Entities;

namespace Application.Common.Identity
{
    public interface ITokenService
    {
        string Generate(User user);
    }
}
