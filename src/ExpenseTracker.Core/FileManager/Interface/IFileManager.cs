using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Core.FileManager.Interface
{
    public interface IFileManager
    {
        Task<string> SaveImage(IFormFile? file, string identity, string leafDirectory);
        void RemoveImage(string identity, string contentDirectory);
    }
}