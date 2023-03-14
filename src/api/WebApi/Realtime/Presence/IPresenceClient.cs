using System.Threading.Tasks;

namespace WebApi.Realtime.Presence
{
    public interface IPresenceClient
    {
        Task UserConnected(string user);

        Task UserDisconnected(string user);
    }
}
