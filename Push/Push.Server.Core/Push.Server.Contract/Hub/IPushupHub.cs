using Push.Server.Contract.Message;
using System.Threading.Tasks;

namespace Push.Server.Contract.Hub
{
    public interface IPushupHub {
        Task ReceiveAsync(AnnouncementPush announcement);
        Task PullAsync(AnnouncementPull announcement);
    }
}