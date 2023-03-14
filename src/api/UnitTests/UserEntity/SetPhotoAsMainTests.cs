using Domain.Aggregates.Users.ValueObjects;
using Domain.Common;
using Xunit;

namespace UnitTests.UserEntity
{
    public class SetPhotoAsMainTests
    {
        [Fact]
        public void SetAsMain_UnknownPhoto_ThrowsDomainException()
        {
            // Arrange
            var user = UserGenerator.GenerateUser();

            // Act, Assert
            Assert.Throws<DomainException>(() => user.SetPhotoAsMain("photo"));
        }

        [Fact]
        public void SetAsMain_HappyPath_NewPhotoBecomesMain()
        {
            // Arrange
            var user = UserGenerator.GenerateUser();

            var photo = new Photo("photo", "url");
            user.AddPhoto(photo);

            var newPhoto = new Photo("photo 2", "url2");
            user.AddPhoto(newPhoto);

            // Act
            user.SetPhotoAsMain(newPhoto.Name);

            // Assert
            var mainPhoto = user.GetMainPhoto();
            Assert.NotNull(mainPhoto);
            Assert.Equal(newPhoto.Name, newPhoto.Name);
            Assert.Equal(newPhoto.Url, newPhoto.Url);
        }
    }
}
