using AccountingAssistantBackend.Data.Entity;
using AccountingAssistantBackend.Data.Repository;
using AccountingAssistantBackend.DTOs;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AccountingAssistantBackend.Services
{
    public class SessionChatManager : ISessionChatManager
    {
        private readonly ILogger<ChatMessageManager> _logger;
        private readonly ISessionChatRepository _sessionChatRepository;
        private readonly IMapper _mapper;

        public SessionChatManager(
            ILogger<ChatMessageManager> logger,
            ISessionChatRepository sessionChatRepository,
            IMapper mapper
            )
        {
            _logger = logger;
            _mapper = mapper;
            _sessionChatRepository = sessionChatRepository;
        }

        public async Task<SessionChatResponse> GetSessionChatById(int id)
        {
            var sessionChat = await _sessionChatRepository.GetSessionsChatByIdAsync(id);
            return _mapper.Map<SessionChatResponse>(sessionChat);
        }

        public async Task<List<SessionChatResponse>> GetSessionsChatsByUser(int userId, int count)
        {
            var sessionsChat = await _sessionChatRepository.GetSessionChatsByUserIdAync(userId, count);
            return _mapper.Map<List<SessionChatResponse>>(sessionsChat);
        }

        public async Task<SessionChatResponse> AddSessionChat(SessionChatRequest sessionChatRequest)
        {
            try
            {
                var sessionChat = _mapper.Map<SessionChat>(sessionChatRequest);
                await _sessionChatRepository.AddSessionsChatAsync(sessionChat);
                var result = _mapper.Map<SessionChatResponse>(sessionChat);
                _logger.LogInformation("Successfully created PhotoBook entity");
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
