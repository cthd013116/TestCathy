using CathyTest2025.Data;
using CathyTest2025.Interface;
using CathyTest2025.Repository;
using Microsoft.EntityFrameworkCore;
using static CathyTest2025.Interface.IRepository;

namespace CathyTest2025.Factory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly ApplicationDbContext _context;

        public RepositoryFactory(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<T> CreateRepository<T>() where T : class =>
            new EfRepository<T>(_context);
    }
}
