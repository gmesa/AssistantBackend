using AccountingAssistantBackend.DTOs;

namespace AccountingAssistantBackend.Services
{
    public interface ISessionChatManager
    {
        Task<List<SessionChatResponse>> GetSessionsChatsByUser(int userId, int count);

        Task<SessionChatResponse> GetSessionChatById(int id);

        public Task<SessionChatResponse> AddSessionChat(SessionChatRequest sessionChatRequest);
    }
}
