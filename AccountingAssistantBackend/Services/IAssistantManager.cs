namespace AccountingAssistantBackend.Services
{
    public interface IAssistantManager
    {
        Task<string> GetResponseForAccountingQuery(int sessionChatId, string query);

        Task<string> GetSummaryFromPdfFile(IFormFile pdfFile);
    }
}
