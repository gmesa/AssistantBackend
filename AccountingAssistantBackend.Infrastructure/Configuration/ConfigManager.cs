using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingAssistantBackend.Infrastructure.Configuration
{
    public class ConfigManager : ConfigManagerBase, IConfigManager
    {

        public ConfigManager(IConfiguration configuration) : base(configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public CustomExceptionHandlerOptions CustomExceptionHandlerOptions { get => GetSection<CustomExceptionHandlerOptions>(CustomExceptionHandlerOptions.SectionName); }

        public OpenAIOptions OpenAIOptions { get => GetSection<OpenAIOptions>(nameof(OpenAIOptions)); }

        public SummaryTextOptions SummaryTextOptions { get => GetSection<SummaryTextOptions>(nameof(SummaryTextOptions)); }

        public ChatOptions ChatOptions { get => GetSection<ChatOptions>(nameof(ChatOptions)); }

    }
}
