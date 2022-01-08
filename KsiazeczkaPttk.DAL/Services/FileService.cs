using KsiazeczkaPttk.DAL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace KsiazeczkaPttk.DAL.Services
{
    public class FileService : IFileService
    {
        public string SaveFile(IFormFile file)
        {
            return file.FileName;
        }

        public void RemoveFile(string url)
        {
            
        }
    }
}
