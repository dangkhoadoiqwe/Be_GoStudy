using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace BE_GOStudy.Hubs
{
    public class UserStatusHub : Hub
    {
        // ConcurrentDictionary để lưu trạng thái người dùng online theo UserId
        private static ConcurrentDictionary<string, string> OnlineUsers = new ConcurrentDictionary<string, string>();

        // Khi người dùng kết nối với userId
        public async Task ConnectWithUserId(int userId)
        {
            // Thêm người dùng vào danh sách online
            OnlineUsers.TryAdd(userId.ToString(), userId.ToString());

            // Thông báo cho tất cả người dùng khác rằng người dùng này đã online
            await Clients.All.SendAsync("UserStatusChanged", userId, true);
        }

        // Khi người dùng ngắt kết nối
        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            string userId = OnlineUsers.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;

            if (!string.IsNullOrEmpty(userId))
            {
                // Xóa người dùng khỏi danh sách online
                OnlineUsers.TryRemove(userId, out _);

                // Thông báo cho tất cả người dùng khác rằng người dùng này đã offline
                await Clients.All.SendAsync("UserStatusChanged", userId, false);
            }

            await base.OnDisconnectedAsync(exception);
        }

        // Trả về danh sách người dùng online hiện tại
        public async Task GetOnlineUsers()
        {
            await Clients.Caller.SendAsync("ReceiveOnlineUsers", OnlineUsers.Keys);
        }
    }
}

 
