using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingAssistantBackend.Data.Entity
{
    public class SessionChat
    {
        public int Id { get; set; }

        public string Title { get; set; }        

        public int UserId { get; set; }

        public User User { get; set; }

        public List<ChatMessage> Messages { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
