namespace AccountingAssistantBackend.DTOs
{
    public record ChatMessageRequest (int SessionChatId, string Content, bool IsFromAssistant) 
    {
        public string TestOtherRevert { get; set; }
    }
}
