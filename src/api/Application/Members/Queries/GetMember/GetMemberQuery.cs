using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Persistence;
using Domain.Aggregates.Users.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Members.Queries.GetMember
{
    public class GetMemberQuery : IRequest<MemberDto>
    {
        public GetMemberQuery(int memberId)
        {
            MemberId = memberId;
        }

        public int MemberId { get; private set; }
    }

    internal class GetMemberQueryHandler : IRequestHandler<GetMemberQuery, MemberDto>
    {
        private readonly IDatingAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMemberQueryHandler(IDatingAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MemberDto> Handle(GetMemberQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == request.MemberId, cancellationToken);

            if (user == null)
            {
                throw new ResourceNotFoundException();
            }

            return _mapper.Map<User, MemberDto>(user);
        }
    }
}
