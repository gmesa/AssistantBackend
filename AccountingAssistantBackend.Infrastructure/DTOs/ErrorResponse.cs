using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccountingAssistantBackend.Infrastructure.DTOs
{
    public class ErrorResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int StatusCode { get; set; }

        public string ErrorDetails { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
