using PetControlSystem.Domain.Entities;
using System.Linq.Expressions;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity obj);
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Update(TEntity obj);
        Task Remove(Guid id);
        Task<int> SaveChanges();
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);
    }
}
