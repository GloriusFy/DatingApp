namespace Application.Users.Login.Models
{
    public class LoggedInUserDto
    {
        public LoggedInUserDto(int id, string? photoUrl)
        {
            Id = id;
            PhotoUrl = photoUrl;
        }

        public int Id { get; }

        public string? PhotoUrl { get; }
    }
}
