namespace CathyTest2025.Interface
{
    public interface IRepository
    {
        public interface IRepository<T> where T : class
        {
            Task<T> GetByNameAsync(string name);
            Task<IEnumerable<T>> GetAllAsync();
            Task AddAsync(T entity);
            Task UpdateAsync(T entity);
            Task DeleteAsync(string name);
        }
    }
}
