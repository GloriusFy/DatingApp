using System.Threading.Tasks;
using Application.Common.Identity;
using Application.Common.Persistence;
using Application.Users.Login.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Login
{
    internal class UserLoginService : IUserLoginService
    {
        private readonly IDatingAppDbContext _dbContext;
        private readonly ITokenService _tokenService;
        private readonly IPasswordValidator _passwordValidator;

        public UserLoginService(IDatingAppDbContext dbContext,
            ITokenService tokenService,
            IPasswordValidator passwordValidator)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _passwordValidator = passwordValidator;
        }

        public async Task<UserLoginResponse?> Login(UserLoginRequest request)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return null;

            var passwordValid = _passwordValidator.Validate(request.Password, user.Password);

            if (!passwordValid)
                return null;

            var loggedInUser = new LoggedInUserDto(user.Id, user.GetMainPhoto()?.Url);

            return new UserLoginResponse(loggedInUser, _tokenService.Generate(user));

        }
    }
}
