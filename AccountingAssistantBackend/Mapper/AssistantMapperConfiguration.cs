using AccountingAssistantBackend.Data.Entity;
using AccountingAssistantBackend.DTOs;
using AutoMapper;
using System.Drawing;

namespace AccountingAssistantBackend.Mapper
{
    public class AssistantMapperConfiguration : Profile
    {
        public AssistantMapperConfiguration()
        {

            CreateMap<SessionChatRequest, SessionChat>(MemberList.None)
                .ForMember(source => source.Id, dest => dest.Ignore());
            CreateMap<SessionChat, SessionChatResponse>(MemberList.None);


            CreateMap<ChatMessageRequest, ChatMessage>(MemberList.None)
                .ForMember(source => source.Id, dest => dest.Ignore());
                
            CreateMap<ChatMessage, ChatMessageResponse>(MemberList.None);
        }
    }
}
