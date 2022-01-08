using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Interfaces
{
    public interface IFileService
    {
        FileStream GetPhoto(string fileName);
        Task<string> SaveFile(IFormFile file);
        void RemoveFile(string url);
    }
}
