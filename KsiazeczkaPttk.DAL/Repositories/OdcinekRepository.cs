using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Repositories
{
    public class OdcinekRepository : IOdcinekRepository
    {
        private readonly KsiazeczkaContext _context;

        public OdcinekRepository(KsiazeczkaContext context)
        {
            _context = context;
        }

        public async Task<PrzebycieOdcinka> GetPrzebytyOdcinekById(int id)
        {
            return await _context.PrzebyteOdcinki.FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
