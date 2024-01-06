using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingAssistantBackend.Infrastructure.Configuration
{
    public class CustomExceptionHandlerOptions
    {
        public const string SectionName = "CustomExceptionHandlerOptions";
        public bool AllwaysReturnOK { get; set; }

        public bool IncludeDetails { get; set; }
    }
}
