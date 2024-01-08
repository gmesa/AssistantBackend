using AccountingAssistantBackend.DTOs;
using AccountingAssistantBackend.Services;
using Microsoft.AspNetCore.Mvc;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace AccountingAssistantBackend.Controllers.v1
{
    /// <summary>
    /// SessionChat Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SessionChatController : ControllerBase
    {

        private readonly ISessionChatManager _sessionChatManager;

        /// <summary>
        /// Initializes a new instance of <see cref="SessionChatController"/>
        /// </summary>
        /// <param name="sessionChatManager">The SessionChat manager</param>
        public SessionChatController(ISessionChatManager sessionChatManager)
        {
            _sessionChatManager = sessionChatManager;
        }

        /// <summary>
        /// Retrieves all SessionChats.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("user/{userId}")]
        [ProducesResponseType(typeof(List<SessionChatResponse>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSessionChats(int userId, [FromQuery] int count)
        {
            var result = await _sessionChatManager.GetSessionsChatsByUser(userId, count);
            if (result != null)
                return Ok(result);

            return BadRequest();
        }

        /// <summary>
        /// Retrieves a SessionChat by ID
        /// </summary>
        /// <param name="id">The ID of the SessionChat to retrieve.</param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(SessionChatResponse), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSessionChatById(int id)
        {
            var result = await _sessionChatManager.GetSessionChatById(id);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Add SessionChat
        /// </summary>
        /// <param name="message">The SessionChat to be added</param>
        /// <returns></returns>
        [HttpPost, Route("")]
        [ProducesResponseType(typeof(SessionChatResponse), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSessionChat([FromBody] SessionChatRequest message)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            var result = await _sessionChatManager.AddSessionChat(message);

            if (result != null)
                return CreatedAtAction(nameof(GetSessionChatById), new { id = result.Id }, result);

            return BadRequest();

        }
    }
}
