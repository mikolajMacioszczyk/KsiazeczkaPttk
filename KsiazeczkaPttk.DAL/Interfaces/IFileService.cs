using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file);
        void RemoveFile(string url);
    }
}
