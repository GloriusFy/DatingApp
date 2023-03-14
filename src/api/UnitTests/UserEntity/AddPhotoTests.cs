using Domain.Aggregates.Users.ValueObjects;
using Xunit;

namespace UnitTests.UserEntity
{
    public class AddPhotoTests
    {
        [Fact]
        public void Add_FirstPhoto_BecomesMain()
        {
            // Arrange
            var user = UserGenerator.GenerateUser();
            var photo = new Photo("photo", "url");

            // Act
            user.AddPhoto(photo);

            // Assert
            Assert.Single(user.Photos);
            var mainPhoto = user.GetMainPhoto();
            Assert.NotNull(mainPhoto);
            Assert.Equal(photo.Name, mainPhoto.Name);
            Assert.Equal(photo.Url, mainPhoto.Url);
        }

        [Fact]
        public void Add_ManyPhotosAfterFirst_FirstIsStillMain()
        {
            // Arrange
            var user = UserGenerator.GenerateUser();
            var photo = new Photo("photo", "url");
            var photo2 = new Photo("photo 2", "url2");
            var photo3 = new Photo("photo 3", "url3");

            // Act
            user.AddPhoto(photo);
            user.AddPhoto(photo2);
            user.AddPhoto(photo3);

            // Assert
            Assert.Equal(3, user.Photos.Count);
            var mainPhoto = user.GetMainPhoto();
            Assert.NotNull(mainPhoto);
            Assert.Equal(photo.Name, mainPhoto.Name);
            Assert.Equal(photo.Url, mainPhoto.Url);
        }
    }
}
