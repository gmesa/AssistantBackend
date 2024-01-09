using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingAssistantBackend.Infrastructure.Configuration
{
    public interface IConfigManager
    {

        /// <summary>
        /// Gets the Exception handler options
        /// </summary>
        CustomExceptionHandlerOptions CustomExceptionHandlerOptions { get; }

        /// <summary>
        /// Gets the OpenAI options
        /// </summary>
        OpenAIOptions OpenAIOptions { get; }

        /// <summary>
        /// Gets the options for summary text
        /// </summary>
        SummaryTextOptions SummaryTextOptions { get; }

        /// <summary>
        /// Gets the options for chat messages
        /// </summary>
        ChatOptions ChatOptions { get; }


    }
}
