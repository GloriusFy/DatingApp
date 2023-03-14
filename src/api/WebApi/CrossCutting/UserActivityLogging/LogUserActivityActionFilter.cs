using System.Security.Claims;
using System.Threading.Tasks;
using Application.Members.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.CrossCutting.UserActivityLogging
{
    public class LogUserActivityActionFilter : IAsyncActionFilter
    {
        private readonly IMediator _mediator;

        public LogUserActivityActionFilter(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var user = resultContext.HttpContext.User;

            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                var request = new LogUserActivityCommand(int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)));

                await _mediator.Send(request);
            }
        }
    }
}
