using AccountingAssistantBackend.Infrastructure.Configuration;
using Microsoft.SemanticKernel;


namespace AccountingAssistantBackend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add CustomExceptionHandlerOptions to DC 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomHandlerExceptionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CustomExceptionHandlerOptions>(configuration.GetSection(CustomExceptionHandlerOptions.SectionName));
            return services;
        }
       
        /// <summary>
        /// Adds a semantic kernel with chat completions to the specified service collection.
        /// </summary>
        /// <param name="services">The service collection to add the semantic kernel to.</param>
        /// <param name="config">The configuration manager to retrieve OpenAI options from</param>
        /// <returns>The modified service collection</returns>
        public static IServiceCollection AddSemanticKernelWithChatCompletions(this IServiceCollection services, IConfigManager config) {

            var openAIOptions = config.OpenAIOptions;

            var kernel = Kernel
                .CreateBuilder()
                .AddOpenAIChatCompletion(openAIOptions.CompletionModel, openAIOptions.ApiKey, openAIOptions.OrganizationId)
                .Build();
            services.AddSingleton(kernel);
            return services;

        }
    }

}
