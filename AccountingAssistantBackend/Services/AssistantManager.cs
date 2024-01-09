
using Microsoft.SemanticKernel;
using System.Text;
using AccountingAssistantBackend.Utils;
using AccountingAssistantBackend.Infrastructure.Configuration;
using Microsoft.SemanticKernel.Plugins.Core;
using Microsoft.SemanticKernel.ChatCompletion;
using AccountingAssistantBackend.Data.Repository;
using Microsoft.SemanticKernel.Connectors.OpenAI;



namespace AccountingAssistantBackend.Services
{
    /// <summary>
    /// Manager for all assistant related tasks
    /// </summary>
    public class AssistantManager : IAssistantManager
    {
        private readonly ILogger<AssistantManager> _logger;
        private readonly Kernel _kernel;
        private readonly KernelPlugin _functions;
        private readonly IConfigManager _config;
        private readonly IChatMessageRepository _chatMessageRepository;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="AssistantManager"/> class.
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="kernel">The kernel</param>
        public AssistantManager(
            ILogger<AssistantManager> logger,
            Kernel kernel,
            IConfigManager config,
            IChatMessageRepository chatMessageRepository)
        {
            _logger = logger;
            _kernel = kernel;
            _functions = _kernel.CreatePluginFromPromptDirectory(GlobalConstants.PluginsNameDirectory);
            _config = config;
            _chatMessageRepository = chatMessageRepository;
           
            // Add conversation summary plugin in case it has not been added
#pragma warning disable SKEXP0050 
            _kernel.Plugins.TryGetPlugin(nameof(ConversationSummaryPlugin), out KernelPlugin? conversationPlugin);
            if (conversationPlugin == null)
                _kernel.Plugins.AddFromType<ConversationSummaryPlugin>();
#pragma warning restore SKEXP0050



        }

        /// <summary>
        /// Get response from OpenAI for user query 
        /// </summary>
        /// <param name="query">The user query</param>
        /// <param name="sessionChatId">The session chat id</param>
        /// <returns></returns>
        public async Task<string> GetResponseForAccountingQuery(int sessionChatId, string query)
        {
            try
            {
                ChatHistory chatHistory = new();

                // Get chat history
                var chats = await _chatMessageRepository
                    .GetChatMessagesBySessionChatIdAsync(sessionChatId, _config.ChatOptions.HistoryChatQuantity);

                //Order chat history from oldest to newest and create a text that contain the chat history
                string shunkHistory = string.Empty;
                if (chats != null)
                {
                    chats = chats.Reverse();

                    string historyString = string.Join("\n", chats.Select(x =>
                    {
                        string text = (x.IsFromAssistant ? AuthorRole.Assistant : AuthorRole.User) + ": " + x.Content;
                        return text;
                    }));

                    var historyTokens = TextUtils.GetTokensFromText(historyString, _config.OpenAIOptions.CompletionModel);

                    // The text history will be truncated if it is too long
                    shunkHistory =
                        TextUtils.GetTextFromTokens(historyTokens.Count > _config.ChatOptions.MaxTokenForHistory
                             ? historyTokens.Skip(historyTokens.Count - _config.ChatOptions.MaxTokenForHistory)
                             .Take(_config.ChatOptions.MaxTokenForHistory)
                             .ToList()
                             : historyTokens
                        , _config.OpenAIOptions.CompletionModel);
                }

                //Invoke the chat function passing the user query and the chat history
                var result = await _kernel.InvokeAsync(
                        _functions[GlobalConstants.AccountingAssistantFunctionName],
                        new()
                        {
                        { "question", query },
                        { "history", shunkHistory },

                        });
                return result.GetValue<string>() ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get response for accounting query");
                return null!;
            }
        }

        /// <summary>
        /// Retrieves the summary from a PDF file.
        /// </summary>
        /// <param name="pdfFile"> The PDF file to retrieve the summary from.</param>
        /// <returns>A string representing the summary extracted from the PDF file, or null if there was an error.    ///   </returns>
        public async Task<string> GetSummaryFromPdfFile(IFormFile pdfFile)
        {
            string text = string.Empty;

            try
            {
                // Load the PDF file and extract its text
                if (pdfFile == null || pdfFile.Length == 0)
                    return text;

                using (var memoryStream = new MemoryStream())
                {
                    await pdfFile.CopyToAsync(memoryStream);
                    text = PdfUtils.GetPdfText(memoryStream);
                }

                // Split the text into chunks
                var chunks = TextUtils.SplitTextIntoShunks(
                    text,
                    _config.SummaryTextOptions.ShunkSize,
                    _config.OpenAIOptions.CompletionModel);

                // Invoke the summary function for each chunk
                StringBuilder summary = new();
                foreach (var chunk in chunks)
                {

                    var result = await _kernel.InvokeAsync(
                         _functions[GlobalConstants.SummaryFunctionName],
                         new()
                         {
                            { "current_chunk", chunk }
                          });

                    summary.Append(result);
                }

                return summary.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get summary from pdf file");
                return null!;
            }


        }
    }
}
