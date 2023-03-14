using NodaTime;

namespace Application.Users.Registration.Models
{
    public class UserRegistrationRequest
    {
        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public GenderDto Gender { get; set; }

        public LocalDate DateOfBirth { get; set; }

        public string Password { get; set; } = null!;
    }
}
