using KsiazeczkaPttk.DAL;
using Microsoft.EntityFrameworkCore;
using System;

namespace KsiazeczkaPttk.Tests
{
    public abstract class TestClassBase : IDisposable
    {
        protected readonly KsiazeczkaContext _context;

        public TestClassBase(string dbName)
        {
            var options = new DbContextOptionsBuilder<KsiazeczkaContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

            _context = new KsiazeczkaContext(options);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
