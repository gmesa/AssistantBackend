using AccountingAssistantBackend.DTOs;
using AccountingAssistantBackend.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AccountingAssistantBackend.Controllers.v1
{
    /// <summary>
    /// Accounting Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AssistantController : ControllerBase
    {

        private readonly IAssistantManager _assistantManager;

        /// <summary>
        /// Initializes a new instance of <see cref="AssistantController"/>
        /// </summary>
        /// <param name="assistantManager">The assistant manager</param>
        public AssistantController(IAssistantManager assistantManager)
        {
            _assistantManager = assistantManager;
        }

        /// <summary>
        /// Get response from OpenAI for accounting query
        /// </summary>
        /// <param name="query">The accounting query</param>
        /// <returns></returns>
        [HttpGet, Route("sessionChatId/{chatMessageId}/query/{query}")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetResponseForAccountingQuery(int chatMessageId, string query)
        {
            var result = await _assistantManager.GetResponseForAccountingQuery(chatMessageId, query);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Summary pdf
        /// </summary>
        /// <param name="file">Pdf file for summary</param>
        /// <returns></returns>
        [HttpPost, Route("documentSummary")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> SummaryFromPdfFile([FromForm] UploadFileRequest file)
        {
            var result = await _assistantManager.GetSummaryFromPdfFile(file.PdfFile);

            if (result != null)
                return Ok(result);

            return BadRequest();
        }
    }
}
