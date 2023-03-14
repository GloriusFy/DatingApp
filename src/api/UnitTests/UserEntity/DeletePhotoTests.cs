using Domain.Aggregates.Users.ValueObjects;
using Domain.Common;
using Xunit;

namespace UnitTests.UserEntity
{
    public class DeletePhotoTests
    {
        [Fact]
        public void Delete_UnknownPhoto_ThrowsDomainException()
        {
            // Arrange
            var user = UserGenerator.GenerateUser();

            // Act, Assert
            Assert.Throws<DomainException>(() => user.DeletePhoto("photo"));
        }

        [Fact]
        public void Delete_MainPhoto_ThrowsDomainException()
        {
            // Arrange
            var user = UserGenerator.GenerateUser();
            user.AddPhoto(new Photo("photo", "url", isMain: true));

            // Act, Assert
            Assert.Throws<DomainException>(() => user.DeletePhoto("photo"));
        }

        [Fact]
        public void Delete_HappyPath_PhotoSuccessfullyDeleted()
        {
            // Arrange
            var user = UserGenerator.GenerateUser();
            user.AddPhoto(new Photo("photo", "url", isMain: true));
            user.AddPhoto(new Photo("photo 2", "url"));

            // Act
            user.DeletePhoto("photo 2");

            // Assert
            Assert.Single(user.Photos);
        }
    }
}
