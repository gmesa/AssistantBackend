using AccountingAssistantBackend.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingAssistantBackend.Data.Repository
{
    public interface ISessionChatRepository
    {
        Task<IEnumerable<SessionChat>> GetSessionChatsByUserIdAync(int userId, int count);

        Task<SessionChat> GetSessionsChatByIdAsync(int id);

        Task<SessionChat> AddSessionsChatAsync(SessionChat sessionChat); 
    }
}
