using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WebApi.Realtime.ConnectedUsers;

namespace WebApi.Realtime.Presence
{
    [Authorize]
    public class PresenceHub : Hub<IPresenceClient>
    {
        private readonly IConnectedUsersTracker _connectedUsersTracker;

        public PresenceHub(IConnectedUsersTracker connectedUsersTracker)
        {
            _connectedUsersTracker = connectedUsersTracker;
        }

        private string UserId => Context.UserIdentifier!;

        public override async Task OnConnectedAsync()
        {
            if (!_connectedUsersTracker.IsConnected(UserId))
            {
                await Clients.Others.UserConnected(UserId);
            }

            _connectedUsersTracker.AddUserConnection(UserId, Context.ConnectionId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _connectedUsersTracker.RemoveUserConnection(UserId, Context.ConnectionId);

            if (!_connectedUsersTracker.IsConnected(UserId))
            {
                await Clients.Others.UserDisconnected(UserId);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public IEnumerable<string> GetConnectedUsers()
        {
            return _connectedUsersTracker.GetConnectedUsers();
        }
    }
}
