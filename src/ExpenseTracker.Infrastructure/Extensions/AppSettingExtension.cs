using System;
using Microsoft.Extensions.Configuration;

namespace ExpenseTracker.Infrastructure.Extensions
{
    
    public static class AppSettingExtension
    {
        private const string AppSettingSection = "AppSettings";
        private const string Secret = "Secret";

        public static IConfigurationSection GetAppSettingSection(this IConfiguration configuration) 
            => configuration.GetSection(AppSettingSection);

        public static string GetSecret(this IConfiguration configuration) 
            => configuration.GetAppSettingSection().GetValue<string>(Secret) ?? throw new Exception("Please add Secret Key");
    }
}