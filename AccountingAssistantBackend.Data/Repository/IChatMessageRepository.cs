using AccountingAssistantBackend.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingAssistantBackend.Data.Repository
{
    public interface IChatMessageRepository
    {
        Task<IEnumerable<ChatMessage>> GetChatMessagesAsync();

        Task<ChatMessage> GetChatMessageByIdAsync(int id);

        Task<IEnumerable<ChatMessage>> GetChatMessagesBySessionChatIdAsync(int sessionChatId, int count);

        Task<ChatMessage> AddChatMessageAsync(ChatMessage message);
    }
}
