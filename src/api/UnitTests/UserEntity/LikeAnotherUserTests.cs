using Xunit;

namespace UnitTests.UserEntity
{
    public class LikeAnotherUserTests
    {
        [Fact]
        public void Like_HappyPath_UserLiked()
        {
            // Arrange
            var user = UserGenerator.GenerateUser();
            const int userToLikeId = 1;

            // Act
            user.Like(userToLikeId);

            // Assert
            Assert.Single(user.UserLikes);
            Assert.Contains(user.UserLikes, ul => ul.TargetUserId == userToLikeId);
        }

        [Fact]
        public void Like_ManyLikesToTheSameUser_DoNotCreateManyLikes()
        {
            // Arrange
            var user = UserGenerator.GenerateUser();
            const int userToLikeId = 1;

            // Act
            user.Like(userToLikeId);
            user.Like(userToLikeId);
            user.Like(userToLikeId);

            // Assert
            Assert.Single(user.UserLikes);
            Assert.Contains(user.UserLikes, ul => ul.TargetUserId == userToLikeId);
        }
    }
}
