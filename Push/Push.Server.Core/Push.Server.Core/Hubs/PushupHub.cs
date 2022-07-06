using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Push.Server.Contract.Hub;
using Push.Server.Contract.Message;

namespace Push.Server.Core.Hubs
{
    internal sealed class PushupHub : Hub<IPushupHub> {
        public async Task Send(AnnouncementPull pull) {
            await Clients.All.ReceiveAsync(new AnnouncementPush { 
                Category = pull.Category
            });
        }

        public async Task Push(AnnouncementPush push) {
            await Clients.AllExcept(Context.ConnectionId).PullAsync(new AnnouncementPull { 
                Category = push.Category
            });
        }
    }
}