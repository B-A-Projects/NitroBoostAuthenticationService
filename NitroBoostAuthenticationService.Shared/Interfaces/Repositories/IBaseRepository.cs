using System.Linq.Expressions;

namespace NitroBoostAuthenticationService.Shared.Interfaces.Repositories;

public interface IBaseRepository<TType> : IAsyncDisposable
{
    Task<TType> Add(TType entity);

    Task<IEnumerable<TType>> AddMany(IEnumerable<TType> entities);

    void Delete(TType entity);

    void DeleteMany(IEnumerable<TType> entities);

    Task<IEnumerable<TType>> Find(Expression<Func<TType, bool>> predicate);

    Task<IEnumerable<TType>> GetAll();

    void Update(TType entity);

    void UpdateMany(IEnumerable<TType> entities);
}