using System.IO;
using System.Threading.Tasks;
using ExpenseTracker.Common.Constants;
using ExpenseTracker.Core.FileManager.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Infrastructure.Manager.Implementation
{
    public class FileManager : IFileManager
    {
        private readonly IWebHostEnvironment _env;
        private readonly string contentDirectory = "Uploads";

        public FileManager(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveImage(IFormFile? file, string identity, string leafDirectory)
        {
            EnsureDirectoryIsCreated(leafDirectory);
            var extension = Path.GetExtension(file.FileName);
            var fileName = identity + extension;
            var filePath = Path.Combine(_env.ContentRootPath, ContentConstant.Content, contentDirectory, leafDirectory,
                fileName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return fileName;
        }

        public void RemoveImage(string identity, string leafDirectory)
        {
            var directory = Path.Combine(_env.ContentRootPath, ContentConstant.Content, contentDirectory, leafDirectory, identity);
            File.Delete(directory);
        }

        private void EnsureDirectoryIsCreated(string leafDirectory)
        {
            var directory = Path.Combine(_env.ContentRootPath, ContentConstant.Content, contentDirectory,
                leafDirectory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}