using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Likes.Queries;
using Application.Members.Commands;
using Application.Members.Commands.Photos;
using Application.Members.Common;
using Application.Members.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.CrossCutting.Auth;
using WebApi.CrossCutting.UserActivityLogging;

namespace WebApi.Controllers.Members.Current
{
    [ApiController]
    [Route("api/members/current")]
    [ServiceFilter(typeof(LogUserActivityActionFilter))]
    public class CurrentMemberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrentMemberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("info")]
        public async Task<IActionResult> UpdateInfo(UpdateMemberInfoRequest request, CancellationToken cancellationToken)
        {
            var user = new AuthenticatedUser(User);

            await _mediator.Send(new UpdateMemberInfoCommand(
                user,
                request.BriefDescription,
                request.LookingFor,
                request.Interests,
                request.City,
                request.Country
            ), cancellationToken);

            return NoContent();
        }

        [HttpPost("photos")]
        public Task<PhotoDto> AddPhoto(IFormFile formFile, CancellationToken cancellationToken)
        {
            var user = new AuthenticatedUser(User);

            return _mediator.Send(new AddPhotoCommand(user, formFile), cancellationToken);
        }

        [HttpPut("photos/{photoName}/main")]
        public Task SetPhotoAsMain(string photoName, CancellationToken cancellationToken)
        {
            var user = new AuthenticatedUser(User);

            return _mediator.Send(new SetPhotoAsMainCommand(user, photoName), cancellationToken);
        }

        [HttpDelete("photos/{photoName}")]
        public Task DeletePhoto(string photoName, CancellationToken cancellationToken)
        {
            var user = new AuthenticatedUser(User);

            return _mediator.Send(new DeletePhotoCommand(user, photoName), cancellationToken);
        }

        [HttpGet("likes")]
        public Task<List<MemberSummary>> GetLikes(LikeType likeType, CancellationToken cancellationToken)
        {
            var user = new AuthenticatedUser(User);

            return _mediator.Send(new GetCurrentMemberLikesQuery(user, likeType), cancellationToken);
        }
    }
}
