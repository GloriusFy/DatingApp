using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Messages;
using Application.Messages.Commands;
using Application.Messages.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WebApi.CrossCutting.Auth;

namespace WebApi.Realtime.Messages
{
    [Authorize]
    public class MessagesHub : Hub<IMessageClient>
    {
        private readonly IMediator _mediator;

        public MessagesHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<MessageDto>> GetMessageThread(int chatBuddyId)
        {
            var httpContext = Context.GetHttpContext();
            var user = new AuthenticatedUser(httpContext.User);

            var groupName = GetGroupName(user.Id, chatBuddyId);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            return await _mediator.Send(new GetMessageThreadQuery(user, chatBuddyId));
        }

        public async Task<MessageDto> SendMessage(CreateMessageRequest request)
        {
            var httpContext = Context.GetHttpContext();
            var user = new AuthenticatedUser(httpContext.User);

            var sentMessage = await _mediator.Send(new AddMessageCommand(request.RecipientId, request.Content, user));

            var group = GetGroupName(user.Id, request.RecipientId);
            await Clients.OthersInGroup(group).ReceiveNewMessage(sentMessage);

            return sentMessage;
        }

        private static string GetGroupName(int caller, int other)
        {
            return caller > other ? $"{caller}-{other}" : $"{other}-{caller}";
        }
    }
}
