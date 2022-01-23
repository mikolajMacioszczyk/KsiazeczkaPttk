using System;
using System.IO;
using System.Threading.Tasks;
using KsiazeczkaPttk.DAL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace KsiazeczkaPttk.Logic.Services
{
    public class FileService : IFileService
    {
        private static string FileDirectory(string rootPath) => Path.Combine(rootPath, "images");

        public FileStream GetPhoto(string fileName, string rootPath)
        {
            var discFileName = DiscFilePath(fileName, rootPath);
            if (File.Exists(discFileName))
            {
                return File.OpenRead(discFileName);
            }
            return null;
        }

        public async Task<string> SaveFile(IFormFile file, string rootPath)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var discFilePath = Path.Combine(FileDirectory(rootPath), fileName);

            CreateFileFolderIfNotExists(rootPath);
            using (var fileStream = new FileStream(discFilePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }

        public void RemoveFile(string fileName, string rootPath)
        {
            var discFilePath = DiscFilePath(fileName, rootPath);
            if (File.Exists(discFilePath))
            {
                File.Delete(discFilePath);
            }
        }

        private string DiscFilePath(string fileName, string rootPath) => Path.Combine(FileDirectory(rootPath), fileName);
    
        private void CreateFileFolderIfNotExists(string rootPath)
        {
            if (!Directory.Exists(FileDirectory(rootPath)))
            {
                Directory.CreateDirectory(FileDirectory(rootPath));
            }
        }
    }
}
