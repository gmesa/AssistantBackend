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
    public class ChatMessageRepository : IChatMessageRepository
    {
        public AppDbContext _context { get; set; }
        public ChatMessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ChatMessage> AddChatMessageAsync(ChatMessage message)
        {
            await _context.ChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<ChatMessage> GetChatMessageByIdAsync(int id)
        {
            return await _context.ChatMessages.FindAsync(id);
        }

        public async Task<IEnumerable<ChatMessage>> GetChatMessagesAsync()
        {
           return await _context.ChatMessages.ToListAsync();
        }

        public async Task<IEnumerable<ChatMessage>> GetChatMessagesBySessionChatIdAsync(int sessionChatId, int count)
        {
            return await _context.ChatMessages
                .Where(cm => cm.SessionChatId == sessionChatId)
                .OrderByDescending(cm => cm.CreatedAt).Take(count)
                .ToListAsync();
        }
    }
}
