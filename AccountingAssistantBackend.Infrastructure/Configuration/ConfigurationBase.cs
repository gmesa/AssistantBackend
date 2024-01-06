using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingAssistantBackend.Infrastructure.Configuration
{
    public class ConfigManagerBase
    {
        private readonly IConfiguration _configuration;

        public ConfigManagerBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected virtual string GetApplicationSettingValue(string key)
        {
            string prefix = "applicationSettings:";
            string defaultSetting = prefix + key;
            return _configuration[defaultSetting] ?? string.Empty;
        }

        protected virtual TSection GetSection<TSection>(string key) where TSection : class, new()
        {
            return _configuration.GetSection(key).Get<TSection>();
        }
    }
}
