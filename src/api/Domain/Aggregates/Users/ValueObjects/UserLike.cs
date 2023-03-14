using Domain.Aggregates.Users.Entities;

namespace Domain.Aggregates.Users.ValueObjects
{
    public class UserLike
    {
        public UserLike(int sourceUserId, int targetUserId)
        {
            SourceUserId = sourceUserId;
            TargetUserId = targetUserId;
        }

        public int SourceUserId { get; }

        public int TargetUserId { get; }

        public User SourceUser { get; private set; } = null!;
    }
}
