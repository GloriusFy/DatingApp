using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Persistence;
using Application.Users;
using Domain.Aggregates.Users;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Application.Messages.Commands
{
    public class AddMessageCommand : IRequest<MessageDto>
    {
        public AddMessageCommand(int recipientId, string content, IAuthenticatedUser user)
        {
            RecipientId = recipientId;
            Content = content;
            User = user;
        }

        public int RecipientId { get; set; }

        public string Content { get; set; }

        public IAuthenticatedUser User { get; set; }
    }

    public class AddMessageCommandHandler : IRequestHandler<AddMessageCommand, MessageDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDatingAppDbContext _dbContext;
        private readonly IClock _clock;

        public AddMessageCommandHandler(
            IUserRepository userRepository,
            IDatingAppDbContext dbContext,
            IClock clock)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
            _clock = clock;
        }

        public async Task<MessageDto> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            if (!await _dbContext.Users.AnyAsync(u => u.Id == request.RecipientId, cancellationToken))
            {
                throw new ResourceNotFoundException();
            }

            var user = await _userRepository.Single(u => u.Id == request.User.Id, cancellationToken);

            var sentMessage = user.SendMessage(request.RecipientId, request.Content, _clock.GetCurrentInstant());

            return sentMessage.Adapt<MessageDto>();
        }
    }
}
