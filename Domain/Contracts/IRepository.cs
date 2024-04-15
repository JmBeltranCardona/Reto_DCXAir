using Domain.Models.Entity;

namespace Domain.Contracts
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> Get();
        Task<T> GetById(Guid id);
        Task<T> Add(T entity);
        Task<List<T>> AddRange(List<T> entity);
        Task<T> Put(T entity);
        Task<bool> Delete(T entity);
    }
}