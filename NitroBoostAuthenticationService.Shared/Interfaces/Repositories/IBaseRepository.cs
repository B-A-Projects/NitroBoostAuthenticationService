using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NitroBoostAuthenticationService.Shared.Interfaces.Repositories
{
    public interface IBaseRepository<TType> : IAsyncDisposable
    {
        Task<IEnumerable<TType>> Find(Expression<Func<TType, bool>> predicate);
        Task<IEnumerable<TType>> GetAll();
        Task<TType> Add(TType entity);
        Task<IEnumerable<TType>> AddMany(IEnumerable<TType> entities);
        void Update(TType entity);
        void UpdateMany(IEnumerable<TType> entities);
        void Delete(TType entity);
        void DeleteMany(IEnumerable<TType> entities);
    }
}
