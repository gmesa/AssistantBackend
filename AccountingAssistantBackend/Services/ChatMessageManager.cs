using AccountingAssistantBackend.Data.Entity;
using AccountingAssistantBackend.Data.Repository;
using AccountingAssistantBackend.DTOs;
using AutoMapper;

namespace AccountingAssistantBackend.Services
{
    public class ChatMessageManager : IChatMessageManager
    {
        private readonly ILogger<ChatMessageManager> _logger;
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IMapper _mapper;
        public ChatMessageManager(
            ILogger<ChatMessageManager> logger,
            IChatMessageRepository chatMessageRepository,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<ChatMessageResponse> GetChatMessageById(int id)
        {
            var chatMessage = await _chatMessageRepository.GetChatMessageByIdAsync(id);
            return _mapper.Map<ChatMessageResponse>(chatMessage);
        }

        public async Task<List<ChatMessageResponse>> GetChatMessagesBySessionChatId(int chatId, int count)
        {
            var chatMessages = await _chatMessageRepository.GetChatMessagesBySessionChatIdAsync(chatId, count);
            return _mapper.Map<List<ChatMessageResponse>>(chatMessages);
        }

        public async Task<ChatMessageResponse> AddChatMessage(ChatMessageRequest request)
        {
            try
            {
                var chatMessage = _mapper.Map<ChatMessage>(request);
                await _chatMessageRepository.AddChatMessageAsync(chatMessage);
                var result = _mapper.Map<ChatMessageResponse>(chatMessage);
                _logger.LogInformation("Successfully created chatMessage entity");
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error adding a ChatMessage");
                return null;
            }

        }

    }
}
