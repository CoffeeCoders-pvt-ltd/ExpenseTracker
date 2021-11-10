using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Web.Provider;
using ExpenseTracker.Web.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ExpenseTracker.Web
{
    public static class DiConfig
    {
        public static IServiceCollection UseExpenseTracker(this IServiceCollection services)
        {
            services.AddScoped<IUserProvider, UserProvider>();

            services.AddSingleton<Icons>((sp) =>
            {
                var rootPath = Directory.GetCurrentDirectory();
                var path = Path.Combine(rootPath, "IconsList.json");
                var icons = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(path));
                return new Icons(icons);
            });

            return services;
        }
    }
}