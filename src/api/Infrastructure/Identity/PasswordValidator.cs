using System.Linq;
using Application.Common.Cryptography;
using Application.Common.Identity;
using Domain.Aggregates.Users.ValueObjects;

namespace Infrastructure.Identity
{
    public class PasswordValidator : IPasswordValidator
    {
        private readonly IPasswordHashService _passwordHashService;

        public PasswordValidator(IPasswordHashService passwordHashService)
        {
            _passwordHashService = passwordHashService;
        }

        public bool Validate(string enteredPassword, Password correctPassword)
        {
            var computedHash = _passwordHashService.Hash(enteredPassword, correctPassword.Salt);

            return !computedHash
                .Where((computedHashByte, i) => computedHashByte != correctPassword.Hash[i])
                .Any();
        }
    }
}
