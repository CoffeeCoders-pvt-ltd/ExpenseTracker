using ExpenseTracker.Common.DBAL;
using ExpenseTracker.Core.Crypter;
using ExpenseTracker.Core.Manager;
using ExpenseTracker.Core.Manager.Interface;
using ExpenseTracker.Core.Services.Implementation;
using ExpenseTracker.Core.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Core
{
    public static class DiConfig
    {
        public static void InjectCoreServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionCategoryService, TransactionCategoryService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IWorkspaceService, WorkspaceService>();
            services.AddScoped<IUow, Uow>();
        }

        public static void InjectCrypterServices(this IServiceCollection services)
            => services.AddScoped<ICrypter, Crypter.Crypter>();

        public static void InjectCoreManager(this IServiceCollection services)
            => services.AddScoped<ITransactionManager, TransactionManager>();
    }
}