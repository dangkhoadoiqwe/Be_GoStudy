using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Linq;

namespace BE_GOStudy.Hubs
{
    public class UserStatusHub : Hub
    {
        // ConcurrentDictionary để lưu trạng thái người dùng online theo UserId
        private static ConcurrentDictionary<string, string> OnlineUsers = new ConcurrentDictionary<string, string>();

        // Khi người dùng kết nối với userId
      public async Task ConnectWithUserId(int userId)
{
    Console.WriteLine($"User {userId} is trying to connect with ConnectionId: {Context.ConnectionId}");

    // Thêm userId và ConnectionId vào danh sách online
    var added = OnlineUsers.TryAdd(userId.ToString(), Context.ConnectionId);

    if (added)
    {
        Console.WriteLine($"User {userId} successfully added to the online list.");
    }
    else
    {
        Console.WriteLine($"Failed to add user {userId} to the online list.");
    }

    // Thông báo cho tất cả người dùng khác rằng user đã online
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
            if (OnlineUsers.Any())
            {
                Console.WriteLine("Current online users:");
                foreach (var userId in OnlineUsers.Keys)
                {
                    Console.WriteLine($"UserId: {userId}");
                }
            }
            else
            {
                Console.WriteLine("No users are currently online.");
            }

            // Gửi danh sách người dùng online về client
            await Clients.Caller.SendAsync("ReceiveOnlineUsers", OnlineUsers.Keys);
        }



        // Phương thức để kiểm tra trạng thái online của danh sách bạn bè
        public async Task GetFriendsOnlineStatus(string[] friendIds)
        {
            // Lấy danh sách bạn bè đang online
            var friendsOnline = OnlineUsers.Where(u => friendIds.Contains(u.Key)).Select(u => u.Key).ToList();

            // Gửi danh sách bạn bè đang online về client
            await Clients.Caller.SendAsync("ReceiveFriendsOnlineStatus", friendsOnline);
        }
    }
}
