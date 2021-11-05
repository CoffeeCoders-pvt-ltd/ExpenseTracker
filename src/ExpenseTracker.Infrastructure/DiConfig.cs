using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Crypter.Interface;
using ExpenseTracker.Infrastructure.Repositories.Implementation;
using ExpenseTracker.Infrastructure.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Infrastructure
{
    public static class DiConfig
    {
        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        public static void InjectCrypter(this IServiceCollection services)
        {
            services.AddScoped<ICrypter, Crypter.Crypter>();
        }
    }
}