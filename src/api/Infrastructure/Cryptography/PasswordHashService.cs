using System.Security.Cryptography;
using Application.Common.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Cryptography
{
    internal class PasswordHashService : IPasswordHashService
    {
        public (byte[] hash, byte[] salt) Generate(string password)
        {
            var salt = GenerateSalt();

            var passwordHash = Hash(password, salt);

            return (passwordHash, salt);
        }

        public byte[] Hash(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(salt);

            return salt;
        }
    }
}
