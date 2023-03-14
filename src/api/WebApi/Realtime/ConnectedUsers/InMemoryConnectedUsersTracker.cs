using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace WebApi.Realtime.ConnectedUsers
{
    public class InMemoryConnectedUsersTracker : IConnectedUsersTracker
    {
        private static readonly ConcurrentDictionary<string, ImmutableHashSet<string>> ConnectedUsers =
            new ConcurrentDictionary<string, ImmutableHashSet<string>>();

        public void AddUserConnection(string userId, string connectionId)
        {
            ConnectedUsers.AddOrUpdate(userId,
                addValue: ImmutableHashSet.Create(connectionId),
                updateValueFactory: (_, connections) => connections.Add(connectionId)
            );
        }

        public void RemoveUserConnection(string userId, string connectionId)
        {
            var addedOrUpdateValue = ConnectedUsers.AddOrUpdate(userId,
                addValue: ImmutableHashSet<string>.Empty,
                updateValueFactory: (_, connections) => connections.Remove(connectionId)
            );

            // TODO: not thread-safe
            if (addedOrUpdateValue.IsEmpty)
            {
                ConnectedUsers.Remove(userId, out _);
            }
        }

        public bool IsConnected(string userId)
        {
            return ConnectedUsers.ContainsKey(userId);
        }

        public IEnumerable<string> GetConnectedUsers() => ConnectedUsers.Select(kv => kv.Key);
    }
}
