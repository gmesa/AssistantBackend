namespace AccountingAssistantBackend.Services
{
    public interface IAccountingManager
    {
        Task<string?> GetResponseForAccountingQuery(string query);
    }
}
