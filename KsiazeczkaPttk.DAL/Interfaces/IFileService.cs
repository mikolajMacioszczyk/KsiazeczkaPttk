using Microsoft.AspNetCore.Http;

namespace KsiazeczkaPttk.DAL.Interfaces
{
    public interface IFileService
    {
        string SaveFile(IFormFile file);
        void RemoveFile(string url);
    }
}
