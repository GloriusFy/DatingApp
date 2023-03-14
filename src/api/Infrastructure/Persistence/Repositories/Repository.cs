using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal abstract class Repository<T> : IRepository<T>
        where T : class, IAggregateRoot
    {
        private readonly DatingAppDbContext _dbContext;

        protected abstract IEnumerable<string> AggregateComponents { get; }

        public Repository(DatingAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<T>> Find(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return GetAggregate()
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        public async Task<T> Single(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entity = await GetAggregate().SingleOrDefaultAsync(predicate, cancellationToken);

            if (entity == null)
            {
                throw new ResourceNotFoundException();
            }

            return entity;
        }

        private IQueryable<T> GetAggregate()
        {
            var query = _dbContext.Set<T>().AsQueryable();

            return AggregateComponents.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));
        }
    }
}
