namespace Application.Users.Registration.Models
{
    public class RegisteredUserDto
    {
        public RegisteredUserDto(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
