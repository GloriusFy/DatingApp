namespace Application.Users.Registration.Models
{
    public class UserRegistrationResponse
    {
        public UserRegistrationResponse(RegisteredUserDto user, string accessToken)
        {
            User = user;
            AccessToken = accessToken;
        }

        public RegisteredUserDto User { get; }

        public string AccessToken { get; }
    }
}
