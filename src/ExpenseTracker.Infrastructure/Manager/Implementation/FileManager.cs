using System.IO;
using System.Threading.Tasks;
using ExpenseTracker.Core.FileManager.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Infrastructure.Manager.Implementation
{
    public class FileManager : IFileManager
    {
        private readonly IWebHostEnvironment _env;

        public FileManager(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task SaveImage(IFormFile file, string identity, string contentDirectory)
        {
            var path = Path.Combine(_env.WebRootPath, contentDirectory);
            if (Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var extension = Path.GetExtension(file.FileName);
            var fileName = identity + extension;
            var filePath = Path.Combine(path, fileName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream)!;
        }

        public void RemoveImage(string identity, string contentDirectory) => File.Delete(contentDirectory + identity);
    }
}