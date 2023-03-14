using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Persistence;
using Application.Members.Queries.GetList;
using Application.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Likes.Queries
{
    public class GetCurrentMemberLikesQuery : IRequest<List<MemberSummary>>
    {
        public GetCurrentMemberLikesQuery(IAuthenticatedUser user, LikeType likeType)
        {
            User = user;
            LikeType = likeType;
        }

        public IAuthenticatedUser User { get; }

        public LikeType LikeType { get; }
    }

    public class GetCurrentMemberLikesQueryHandler : IRequestHandler<GetCurrentMemberLikesQuery, List<MemberSummary>>
    {
        private readonly IDatingAppDbContext _dbContext;

        public GetCurrentMemberLikesQueryHandler(IDatingAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<MemberSummary>> Handle(GetCurrentMemberLikesQuery request, CancellationToken cancellationToken)
        {
            var query = request.LikeType switch
            {
                LikeType.Received => _dbContext.Users.Where(su =>
                    _dbContext.Users
                        .SelectMany(u => u.UserLikes)
                        .Where(ul => ul.TargetUserId == request.User.Id)
                        .Select(ul => ul .SourceUserId)
                        .Contains(su.Id)),

                LikeType.Put => _dbContext.Users.Where(tu =>
                    _dbContext.Users
                        .Where(u => u.Id == request.User.Id)
                        .SelectMany(u => u.UserLikes)
                        .Select(u => u.TargetUserId)
                        .Contains(tu.Id)),

                _ => throw new ArgumentOutOfRangeException(nameof(request.LikeType), "Unknown like type")
            };

            return query
                .Select(u => new MemberSummary(u.Id, u.Name, u.City, u.Photos.FirstOrDefault(p => p.IsMain)))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
