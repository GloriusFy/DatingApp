namespace Application.Common.Cryptography
{
    public interface IPasswordHashService
    {
        (byte[] hash, byte[] salt) Generate(string password);

        byte[] Hash(string password, byte[] salt);
    }
}
