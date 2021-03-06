using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Web.Provider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Web.Middleware
{
    public class WorkspaceMiddleware
    {
        private const string WorkspaceCreateUrl = "/Workspace/Create";
        private const string LoginUrl = "/Account/Login";
        private const string LoginApiUrl = "/Authentication/Login";
        private const string RegisterUrl = "/Account/Register";
        private const string IconApiUrl = "/api/icon";


        private static readonly List<string> PathsToAvoid = new()
        {
            LoginUrl,
            LoginApiUrl,
            WorkspaceCreateUrl,
            RegisterUrl,
            IconApiUrl
        };

        private readonly RequestDelegate _next;

        public WorkspaceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IUserProvider userProvider,
            IWorkspaceRepository workspaceRepository)
        {
            var currentRequestPath = httpContext.Request.Path;

            var currentUserId = userProvider.GetCurrentUserId();

            if (!PathsToAvoid.Contains(currentRequestPath) && currentUserId != null)
            {
                var hasDefaultWorkspace = await workspaceRepository.HasDefaultWorkspace(currentUserId);
                if (!hasDefaultWorkspace)
                {
                    httpContext.Response.Redirect(WorkspaceCreateUrl);
                    return;
                }
            }

            await _next(httpContext);
        }
    }

    public static class WorkspaceCheckMiddleware
    {
        public static IApplicationBuilder UseWorkspaceMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WorkspaceMiddleware>();
        }
    }
}