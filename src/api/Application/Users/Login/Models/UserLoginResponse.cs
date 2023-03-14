namespace Application.Users.Login.Models
{
    public class UserLoginResponse
    {
        public UserLoginResponse(LoggedInUserDto loggedInUser, string accessToken)
        {
            LoggedInUser = loggedInUser;
            AccessToken = accessToken;
        }

        public LoggedInUserDto LoggedInUser { get; }

        public string AccessToken { get; }
    }
}
