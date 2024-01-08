using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingAssistantBackend.Data.Entity
{
    public class ChatMessage
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsFromAssistant { get; set; }

        public int SessionChatId { get; set; }

        public SessionChat SessionChat { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }


}
