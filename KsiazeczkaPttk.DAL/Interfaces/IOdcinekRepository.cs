using KsiazeczkaPttk.Domain.Models;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Interfaces
{
    public interface IOdcinekRepository
    {
        Task<PrzebycieOdcinka> GetPrzebytyOdcinekById(int id);
    }
}
