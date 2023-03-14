using Application.Common.Cryptography;
using Application.Common.Identity;
using Domain.Aggregates.Users.ValueObjects;
using Infrastructure.Identity;
using Moq;
using Xunit;

namespace UnitTests.Infrastructure
{
    public class PasswordValidatorTests
    {
        private readonly Mock<IPasswordHashService> _passwordHashServiceMock = new Mock<IPasswordHashService>();

        private readonly IPasswordValidator _passwordValidator;

        public PasswordValidatorTests()
        {
            _passwordValidator = new PasswordValidator(_passwordHashServiceMock.Object);
        }

        [Fact]
        public void Validate_WrongPassword_ReturnFalse()
        {
            // Arrange
            var hash = new byte[] {0, 72, 123, 11};
            var salt = new byte[] {11, 12, 13, 14};
            var password = new Password(hash, salt);

            var enteredPasswordHash = new byte[] {11, 11, 11, 11};
            _passwordHashServiceMock
                .Setup(s => s.Hash(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(enteredPasswordHash);

            // Act
            var result = _passwordValidator.Validate("some password", password);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_CorrectPassword_ReturnTrue()
        {
            // Arrange
            var hash = new byte[] {0, 72, 123, 11};
            var salt = new byte[] {11, 12, 13, 14};
            var password = new Password(hash, salt);

            _passwordHashServiceMock
                .Setup(s => s.Hash(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(hash);

            // Act
            var result = _passwordValidator.Validate("some password", password);

            // Assert
            Assert.True(result);
        }
    }
}
