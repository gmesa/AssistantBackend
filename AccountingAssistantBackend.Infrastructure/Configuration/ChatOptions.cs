using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingAssistantBackend.Infrastructure.Configuration
{
    public class ChatOptions
    {
        public int HistoryChatQuantity { get; set; }

        public int MaxTokenForHistory { get; set; }
    }
}
