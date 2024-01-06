using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingAssistantBackend.Infrastructure.Configuration
{
    /// <summary>
    /// Contains options to configure the OpenAI API
    /// </summary>
    public class OpenAIOptions
    {
        public string ApiKey { get; set; }

        public string CompletionModel { get; set; }

        public string OrganizationId { get; set; }
    }
}
