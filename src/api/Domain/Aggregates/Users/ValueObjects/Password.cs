namespace Domain.Aggregates.Users.ValueObjects
{
    public class Password
    {
        public Password(byte[] hash, byte[] salt)
        {
            Hash = hash;
            Salt = salt;
        }

        public byte[] Hash { get; private set; }

        public byte[] Salt { get; private set; }
    }
}
