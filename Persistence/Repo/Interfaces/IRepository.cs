using System.Linq.Expressions;

namespace Persistence.Repo.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<TOutput>> GetAllAsync<TOutput>();
        Task<TOutput> GetByIdAsync<TOutput>(Expression<Func<TOutput, bool>> expression) where TOutput : class;
        Task<bool> CreateAsync<TInput>(TInput input) where TInput : class;
        Task<bool> UpdateAsync<TInput>(TInput input) where TInput : class;
    }
}
