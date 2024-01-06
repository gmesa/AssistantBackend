
using Azure.Core;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace AccountingAssistantBackend.Services
{
    /// <summary>
    /// Manager for all accounting related tasks
    /// </summary>
    public class AccountingManager : IAccountingManager
    {
        private readonly ILogger<AccountingManager> _logger;
        private readonly Kernel _kernel;
        private readonly KernelPlugin _functions;


        /// <summary>
        /// Initializes a new instance of the <see cref="AccountingManager"/> class.
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="kernel">The kernel</param>
        public AccountingManager(ILogger<AccountingManager> logger, Kernel kernel)
        {
            _logger = logger;
            _kernel = kernel;
            _functions = _kernel.CreatePluginFromPromptDirectory("Plugins");

        }
       
        /// <summary>
        /// Get response from OpenAI for accounting query 
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string?> GetResponseForAccountingQuery(string query)
        {
            try
            {                
                var result = await _kernel.InvokeAsync(_functions["AccountingAssistant"], new() { { "question", query } });
                return result.GetValue<string>() ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get response for accounting query");
                return null;
            }
        }

        public static string pdfText(string path)
        {
            PdfReader reader = new PdfReader(path);
            PdfDocument pdf = new PdfDocument(reader);
            string text = string.Empty;
            for (int page = 1; page <= pdf.GetNumberOfPages(); page++)
            {
                text += PdfTextExtractor.GetTextFromPage(pdf.GetPage(page));
            }
            reader.Close();
            return text;
        }
    }
}
