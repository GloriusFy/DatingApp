using Domain.Aggregates.Users.ValueObjects;

namespace Application.Common.Identity
{
    public interface IPasswordValidator
    {
        bool Validate(string enteredPassword, Password correctPassword);
    }
}
