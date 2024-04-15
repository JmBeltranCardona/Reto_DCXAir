using Domain.Contracts;
using Domain.Models.Entity;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        private readonly AplicationDBContext _ctx;
        private DbSet<T> _entities;
        public BaseRepository(AplicationDBContext ctx)
        {
            _ctx = ctx;
            _entities = ctx.Set<T>();
        }

        public IQueryable<T> Get()
        {
            return _entities;
        }

        public async Task<T> GetById(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                _entities.Add(entity);
                var test = await _ctx.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<T>> AddRange(List<T> entity)
        {
            _entities.AddRange(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Put(T entity)
        {
            _entities.Update(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> Delete(T entity)
        {

            _entities.Remove(entity);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}
