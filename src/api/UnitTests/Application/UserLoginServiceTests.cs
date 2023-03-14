using System.Threading.Tasks;
using Application.Common.Identity;
using Application.Users.Login;
using Application.Users.Login.Models;
using Domain.Aggregates.Users.Entities;
using Domain.Aggregates.Users.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;
using UnitTests.UserEntity;
using Xunit;

namespace UnitTests.Application
{
    public class UserLoginServiceTests
    {
        private readonly Mock<ITokenService> _tokenServiceMock = new Mock<ITokenService>();
        private readonly Mock<IPasswordValidator> _passwordValidatorMock = new Mock<IPasswordValidator>();

        private readonly DbContextOptions<DatingAppDbContext> _options =
            new DbContextOptionsBuilder<DatingAppDbContext>()
                .UseInMemoryDatabase(nameof(UserLoginServiceTests))
                .Options;

        private readonly IUserLoginService _userLoginService;

        public UserLoginServiceTests()
        {
            var dbContext = new DatingAppDbContext(_options);
            dbContext.Users.RemoveRange(dbContext.Users);
            dbContext.SaveChanges();

            _userLoginService = new UserLoginService(dbContext,
                _tokenServiceMock.Object,
                _passwordValidatorMock.Object);
        }

        [Fact]
        public async Task Login_NonexistentEmail_ReturnNull()
        {
            // Arrange, Act
            var result = await _userLoginService.Login(new UserLoginRequest
            {
                Email = "email",
                Password = "password"
            });

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Login_InvalidPassword_ReturnNull()
        {
            // Arrange
            var user = UserGenerator.GenerateUser();

            using (var dbContext = new DatingAppDbContext(_options))
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }

            _passwordValidatorMock
                .Setup(v => v.Validate(It.IsAny<string>(), It.IsAny<Password>()))
                .Returns(false);

            // Act
            var result = await _userLoginService.Login(new UserLoginRequest
            {
                Email = user.Email,
                Password = "password"
            });

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Login_HappyPath_ReturnUserWithToken()
        {
            // Arrange
            var user = UserGenerator.GenerateUser();

            using (var dbContext = new DatingAppDbContext(_options))
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }

            _passwordValidatorMock
                .Setup(v => v.Validate(It.IsAny<string>(), It.IsAny<Password>()))
                .Returns(true);

            _tokenServiceMock
                .Setup(s => s.Generate(It.IsAny<User>()))
                .Returns("token");

            // Arrange
            var result = await _userLoginService.Login(new UserLoginRequest
            {
                Email = user.Email,
                Password = "password"
            });

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.AccessToken);
            Assert.NotNull(result.LoggedInUser);
        }
    }
}
