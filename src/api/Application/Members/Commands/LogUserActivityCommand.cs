using System.Threading;
using System.Threading.Tasks;
using Application.Common.Persistence;
using MediatR;
using NodaTime;

namespace Application.Members.Commands
{
    public class LogUserActivityCommand : IRequest
    {
        public LogUserActivityCommand(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }

    public class LogUserActivityCommandHandler : AsyncRequestHandler<LogUserActivityCommand>
    {
        private readonly IDatingAppDbContext _dbContext;
        private readonly IClock _clock;

        public LogUserActivityCommandHandler(
            IDatingAppDbContext dbContext,
            IClock clock)
        {
            _dbContext = dbContext;
            _clock = clock;
        }

        protected override async Task Handle(LogUserActivityCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(new object[]{request.UserId}, cancellationToken);

            user.Active(_clock.GetCurrentInstant());
        }
    }
}
