using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IFriendRequestsRepository
    {
        Task AddFriendRequest(int requesterId, int recipientId);
        Task<FriendRequest?> GetFriendRequest(int requesterId, int recipientId);

        Task UpdateFriendRequestStatus(int friendRequestId, string status);
        Task<FriendRequest> GetByIdAsync(int id);
    }
    public class FriendRequestsRepository : IFriendRequestsRepository
    {
        private readonly GOStudyContext _context;

        public FriendRequestsRepository(GOStudyContext context)
        {
            _context = context;
        }
        public async Task<FriendRequest?> GetFriendRequest(int requesterId, int recipientId)
        {
            return await _context.Set<FriendRequest>()
                .FirstOrDefaultAsync(fr => fr.RequesterId == requesterId && fr.RecipientId == recipientId);
        }

        public async Task<FriendRequest> GetByIdAsync(int id)
        {
            return await _context.Set<FriendRequest>().FindAsync(id);
        }

        public async Task UpdateFriendRequestStatus(int id, string status)
        {
            var friendRequest = await GetByIdAsync(id);
            if (friendRequest != null)
            {
                friendRequest.Status = status;
                await _context.SaveChangesAsync();
            }
        }

       
        public async Task AddFriendRequest(int requesterId, int recipientId)
        {
            var friendRequest = new FriendRequest
            {
                RequesterId = requesterId,
                RecipientId = recipientId,
                Status = "Pending", // Giả sử trạng thái ban đầu là "Pending"
                SentAt = DateTime.UtcNow
            };

            _context.Set<FriendRequest>().Add(friendRequest);
            await _context.SaveChangesAsync();
        }
    }
}
