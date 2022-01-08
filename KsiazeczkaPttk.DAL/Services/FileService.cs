using KsiazeczkaPttk.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Services
{
    public class FileService : IFileService
    {
        private static readonly string FileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "images");

        public async Task<string> SaveFile(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var discFilePath = Path.Combine(FileDirectory, fileName);

            CreateFileFolderIfNotExists();
            using (var fileStream = new FileStream(discFilePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }

        public void RemoveFile(string fileName)
        {
            var discFilePath = DiscFilePath(fileName);
            if (File.Exists(discFilePath))
            {
                File.Delete(discFilePath);
            }
        }

        private string DiscFilePath(string fileName) => Path.Combine(FileDirectory, fileName);
    
        private void CreateFileFolderIfNotExists()
        {
            if (!Directory.Exists(FileDirectory))
            {
                Directory.CreateDirectory(FileDirectory);
            }
        }
    }
}
