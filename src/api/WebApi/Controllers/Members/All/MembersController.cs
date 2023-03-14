using System.Threading;
using System.Threading.Tasks;
using Application.Common.Pagination;
using Application.Likes.Commands;
using Application.Members.Queries.GetList;
using Application.Members.Queries.GetMember;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.CrossCutting.Auth;
using WebApi.CrossCutting.UserActivityLogging;

namespace WebApi.Controllers.Members.All
{
    [ApiController]
    [Authorize]
    [Route("api/members")]
    [ServiceFilter(typeof(LogUserActivityActionFilter))]
    public class MembersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MembersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public Task<PagedResponse<MemberSummary>> List(
            [FromQuery] GetMemberListRequest request,
            CancellationToken cancellationToken)
        {
            var user = new AuthenticatedUser(User);

            return _mediator.Send(new GetMemberListQuery(
                    user,
                    request.PageSize,
                    request.PageNumber,
                    request.Gender,
                    request.MinAge,
                    request.MaxAge,
                    request.OrderBy
                ),
                cancellationToken);
        }

        [HttpGet("{id}")]
        public Task<MemberDto> Get(int id, CancellationToken cancellationToken)
        {
            return _mediator.Send(new GetMemberQuery(id), cancellationToken);
        }

        [HttpPut("{id}/likes")]
        public Task Like(int id, CancellationToken cancellationToken)
        {
            var user = new AuthenticatedUser(User);

            return _mediator.Send(new LikeMemberCommand(user, id), cancellationToken);
        }
    }
}
