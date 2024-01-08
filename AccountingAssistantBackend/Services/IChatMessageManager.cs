using AccountingAssistantBackend.DTOs;

namespace AccountingAssistantBackend.Services
{
    public interface IChatMessageManager
    {
        Task<List<ChatMessageResponse>> GetChatMessagesBySessionChatId(int chatId, int count);

        Task<ChatMessageResponse> GetChatMessageById(int id);

        Task<ChatMessageResponse> AddChatMessage(ChatMessageRequest request);
    }
}
