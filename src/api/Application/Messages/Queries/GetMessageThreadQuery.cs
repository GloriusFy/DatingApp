using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Persistence;
using Application.Users;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Messages.Queries
{
    public class GetMessageThreadQuery : IRequest<List<MessageDto>>
    {
        public GetMessageThreadQuery(IAuthenticatedUser user, int chatBuddyId)
        {
            User = user;
            ChatBuddyId = chatBuddyId;
        }

        public IAuthenticatedUser User { get; }

        public int ChatBuddyId { get; }
    }

    public class GetMessageThreadQueryHandler : IRequestHandler<GetMessageThreadQuery, List<MessageDto>>
    {
        private readonly IDatingAppDbContext _datingAppDbContext;

        public GetMessageThreadQueryHandler(IDatingAppDbContext datingAppDbContext)
        {
            _datingAppDbContext = datingAppDbContext;
        }

        public Task<List<MessageDto>> Handle(GetMessageThreadQuery request, CancellationToken cancellationToken)
        {
            return _datingAppDbContext.Messages
                .Where(m =>
                    m.SenderId == request.User.Id && m.RecipientId == request.ChatBuddyId ||
                    m.RecipientId == request.User.Id && m.SenderId == request.ChatBuddyId)
                .OrderBy(m => m.SendDate)
                .AsNoTracking()
                .ProjectToType<MessageDto>()
                .ToListAsync(cancellationToken);
        }
    }
}
