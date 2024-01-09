using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingAssistantBackend.Infrastructure.Configuration
{
    public class SummaryTextOptions
    {
        public int MaxPromptTokens { get; set; }

        public int ShunkSize { get; set; }
    }
}
