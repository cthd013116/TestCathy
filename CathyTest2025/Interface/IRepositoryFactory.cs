using static CathyTest2025.Interface.IRepository;

namespace CathyTest2025.Interface
{
    public interface IRepositoryFactory
    {
        IRepository<T> CreateRepository<T>() where T : class;
    }
}
