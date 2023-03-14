using System.Collections.Generic;

namespace WebApi.Realtime.ConnectedUsers
{
    public interface IConnectedUsersTracker
    {
        void AddUserConnection(string userId, string connectionId);

        void RemoveUserConnection(string userId, string connectionId);

        bool IsConnected(string userId);

        IEnumerable<string> GetConnectedUsers();
    }
}
