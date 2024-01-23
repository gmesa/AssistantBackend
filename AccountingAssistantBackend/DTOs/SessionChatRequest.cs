namespace AccountingAssistantBackend.DTOs
{
    public record SessionChatRequest(int UserId, string Title)
    {
        public string TestReverted { get; set; }

        public string KeepChanges { get; set; }
    }
}
