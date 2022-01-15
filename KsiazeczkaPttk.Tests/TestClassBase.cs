using KsiazeczkaPttk.DAL;
using Microsoft.EntityFrameworkCore;

namespace KsiazeczkaPttk.Tests
{
    public abstract class TestClassBase
    {
        protected readonly KsiazeczkaContext _context;

        public TestClassBase(string dbName)
        {
            var options = new DbContextOptionsBuilder<KsiazeczkaContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

            _context = new KsiazeczkaContext(options);
        }
    }
}
