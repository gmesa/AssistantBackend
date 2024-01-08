using AccountingAssistantBackend.Data.Context;
using AccountingAssistantBackend.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingAssistantBackend.Data.Repository
{
    public class SessionChatRepository : ISessionChatRepository
    {
        public AppDbContext _context { get; set; }

        public SessionChatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SessionChat>> GetSessionChatsByUserIdAync(int userId, int count)
        {
            return await _context.SessionsChat
               .Where(cm => cm.UserId == userId)
               .OrderByDescending(cm => cm.CreatedAt).Take(count)
               .ToListAsync();
        }

        public async Task<SessionChat> GetSessionsChatByIdAsync(int id)
        {
            return await _context.SessionsChat.FindAsync(id);
        }

        public async Task<SessionChat> AddSessionsChatAsync(SessionChat sessionChat)
        {
            await _context.SessionsChat.AddAsync(sessionChat);
            await _context.SaveChangesAsync();
            return sessionChat;
        }
    }
}
