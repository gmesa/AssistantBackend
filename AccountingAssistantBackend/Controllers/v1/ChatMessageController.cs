using AccountingAssistantBackend.DTOs;
using AccountingAssistantBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountingAssistantBackend.Controllers.v1
{
    /// <summary>
    /// ChatMessage Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {
        private readonly IChatMessageManager _chatMessageManager;

        public ChatMessageController(IChatMessageManager chatMessageManager)
        {
            _chatMessageManager = chatMessageManager;
        }

        /// <summary>
        /// Retrieves all chats message for specific chat.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("{sessionChatId}/messages")]
        [ProducesResponseType(typeof(List<ChatMessageResponse>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetChatMessages(int sessionChatId, [FromQuery] int count)
        {
            var result = await _chatMessageManager.GetChatMessagesBySessionChatId(sessionChatId, count);
            if (result != null)
                return Ok(result);

            return BadRequest();
        }

        /// <summary>
        /// Retrieves a chat message by ID
        /// </summary>
        /// <param name="id">The ID of the chat message to retrieve.</param>
        /// <returns></returns>
        [HttpGet, Route("{id}", Name = "GetChatMessageById")]
        [ProducesResponseType(typeof(ChatMessageResponse), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetChatMessageById(int id)
        {
            var result = await _chatMessageManager.GetChatMessageById(id);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Add chat message
        /// </summary>
        /// <param name="message">The chat message to be added</param>
        /// <returns></returns>
        [HttpPost, Route("")]
        [ProducesResponseType(typeof(ChatMessageResponse), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddChatMessage([FromBody] ChatMessageRequest message)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            var result = await _chatMessageManager.AddChatMessage(message);

            if (result != null)
                return CreatedAtAction(nameof(GetChatMessageById), new { id = result.Id}, result);
            
            return BadRequest();

        }
    }
}
