using System.Collections.Generic;
using Domain.Aggregates.Users;
using Domain.Aggregates.Users.Entities;

namespace Infrastructure.Persistence.Repositories
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatingAppDbContext dbContext) : base(dbContext)
        {
        }

        protected override IEnumerable<string> AggregateComponents => new []
        {
            $"{nameof(User.ReceivedMessages)}",
            $"{nameof(User.SentMessages)}"
        };
    }
}
