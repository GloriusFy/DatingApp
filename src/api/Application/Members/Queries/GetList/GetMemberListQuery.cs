using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Pagination;
using Application.Common.Persistence;
using Application.Users;
using Application.Users.Registration.Models;
using Domain;
using Domain.Aggregates.Users.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Application.Members.Queries.GetList
{
    public class GetMemberListQuery : IRequest<PagedResponse<MemberSummary>>
    {
        public GetMemberListQuery(IAuthenticatedUser user, int pageSize, int pageNumber,
            GenderDto? gender, int? minAge, int? maxAge, SortableField? orderBy)
        {
            User = user;
            PageSize = pageSize;
            PageNumber = pageNumber;
            Gender = gender;
            MinAge = minAge;
            MaxAge = maxAge;
            OrderBy = orderBy;
        }

        public IAuthenticatedUser User { get; }

        public int PageSize { get; }

        public int PageNumber { get; }

        public GenderDto? Gender { get; }

        public int? MinAge { get; }

        public int? MaxAge { get; }

        public SortableField? OrderBy { get; }
    }

    internal class GetMemberListQueryHandler : IRequestHandler<GetMemberListQuery, PagedResponse<MemberSummary>>
    {
        private readonly IDatingAppDbContext _datingAppDbContext;

        public GetMemberListQueryHandler(
            IDatingAppDbContext datingAppDbContext)
        {
            _datingAppDbContext = datingAppDbContext;
        }

        public Task<PagedResponse<MemberSummary>> Handle(GetMemberListQuery request, CancellationToken cancellationToken)
        {
            var query = _datingAppDbContext.Users.Where(u => u.Id != request.User.Id);
            query = ApplyGenderFilter(query, request.Gender, request.User.Gender);
            query = ApplyAgeFilter(query, request.MinAge, request.MaxAge);

            IOrderedQueryable<User> orderedQuery = request.OrderBy switch
            {
                SortableField.Created => query.OrderByDescending(u => u.Created),
                SortableField.LastActive => query.OrderByDescending(u => u.LastActive),
                _ => query.OrderByDescending(u => u.LastActive)
            };

            var projectedQuery = orderedQuery
                .ThenBy(m => m.Id)
                .AsNoTracking()
                .Select(u => new MemberSummary(
                    u.Id,
                    u.Name,
                    u.City,
                    u.Photos.FirstOrDefault(p => p.IsMain)
                ));

            return PagedResponse<MemberSummary>.Create(projectedQuery, request.PageNumber, request.PageSize);
        }

        private static IQueryable<User> ApplyGenderFilter(IQueryable<User> currentQuery, GenderDto? genderFromRequest, GenderDto currentUserGender)
        {
            Gender genderToFilterBy;

            if (!genderFromRequest.HasValue)
            {
                genderToFilterBy = currentUserGender.Adapt<Gender>() == Gender.Male ? Gender.Female : Gender.Male;
            }
            else
            {
                genderToFilterBy = genderFromRequest.Adapt<Gender>();
            }

            return currentQuery.Where(u => u.Gender == genderToFilterBy);
        }

        private static IQueryable<User> ApplyAgeFilter(IQueryable<User> currentQuery, int? minAge, int? maxAge)
        {
            var now = DateTimeOffset.UtcNow;

            if (minAge.HasValue)
            {
                var minDateOfBirth = new LocalDate(now.Year - minAge.Value, now.Month, now.Day);

                currentQuery = currentQuery.Where(u => u.DateOfBirth < minDateOfBirth);
            }

            if (maxAge.HasValue)
            {
                var maxDateOfBirth = new LocalDate(now.Year - maxAge.Value, now.Month, now.Day);;

                currentQuery = currentQuery.Where(u => u.DateOfBirth > maxDateOfBirth);
            }

            return currentQuery;
        }
    }
}
