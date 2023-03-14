using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IRepository<T> where T : class, IAggregateRoot
    {
        Task<List<T>> Find(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task<T> Single(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
