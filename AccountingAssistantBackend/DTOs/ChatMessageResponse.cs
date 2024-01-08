namespace AccountingAssistantBackend.DTOs
{
    public record class ChatMessageResponse (int Id, int SessionChatId, string Content, bool IsFromAssistant, DateTime CreatedAt)
    {

    }
}
