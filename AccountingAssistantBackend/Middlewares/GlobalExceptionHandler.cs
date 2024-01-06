using AccountingAssistantBackend.Infrastructure.Configuration;
using AccountingAssistantBackend.Infrastructure.DTOs;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace AccountingAssistantBackend.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IOptions<CustomExceptionHandlerOptions> _execeptionOptions;

        public GlobalExceptionHandler(
            RequestDelegate next, 
            ILogger<GlobalExceptionHandler> logger, 
            IOptions<CustomExceptionHandlerOptions> execeptionOptions)
        {
            _next = next;
            _logger = logger;
            _execeptionOptions = execeptionOptions;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }
        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var errorResponse = new ErrorResponse
            {
                Success = false
            };

            switch (exception)
            {

                case ApplicationException:
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = exception.Message;
                    break;
                default:
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = exception.Message;
                    break;

            }

            if (_execeptionOptions.Value.AllwaysReturnOK)
                errorResponse.StatusCode = (int)HttpStatusCode.OK;
            if (_execeptionOptions.Value.IncludeDetails)
                errorResponse.ErrorDetails = exception.StackTrace ?? string.Empty;

            context.Response.StatusCode = errorResponse.StatusCode;

            string resp = JsonSerializer.Serialize(errorResponse);
            _logger.LogError(errorResponse.Message);
            await response.WriteAsync(resp);
        }
    }
}
