using System;
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

        public async Task<string> SaveImage(IFormFile? file, string identity, string contentDirectory)
        {
            EnsureDirectoryIsCreated(contentDirectory);
            var extension = Path.GetExtension(file.FileName);
            var fileName = identity + extension;
            var filePath = Path.Combine(_env.WebRootPath, contentDirectory, fileName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return fileName;
        }

        public void RemoveImage(string identity, string contentDirectory)
        {
            var x = (_env.WebRootPath + "\\" + contentDirectory) + "\\" + identity;
            File.Delete((_env.WebRootPath + "\\" + contentDirectory) + "\\" + identity);
        }

        private void EnsureDirectoryIsCreated(string contentDirectory)
        {
            var dir = _env.WebRootPath + "\\" + contentDirectory;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}