using System.Linq.Expressions;

namespace El_Mooo_Clinic.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id, string[] includes = null);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, string[] includes = null);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
