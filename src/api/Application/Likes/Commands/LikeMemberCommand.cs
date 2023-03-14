using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Persistence;
using Application.Users;
using Domain.Aggregates.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Likes.Commands
{
    public class LikeMemberCommand : IRequest
    {
        public LikeMemberCommand(IAuthenticatedUser user, int targetUserId)
        {
            User = user;
            TargetUserId = targetUserId;
        }

        public IAuthenticatedUser User { get; }

        public int TargetUserId { get; }
    }

    public class LikeMemberCommandHandler : AsyncRequestHandler<LikeMemberCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDatingAppDbContext _dbContext;

        public LikeMemberCommandHandler(IUserRepository userRepository, IDatingAppDbContext dbContext)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
        }

        protected override async Task Handle(LikeMemberCommand request, CancellationToken cancellationToken)
        {
            if (!await _dbContext.Users.AnyAsync(u => u.Id == request.TargetUserId, cancellationToken))
            {
                throw new ResourceNotFoundException();
            }

            var user = await _userRepository.Single(u => u.Id == request.User.Id, cancellationToken);

            user.Like(request.TargetUserId);
        }
    }
}
