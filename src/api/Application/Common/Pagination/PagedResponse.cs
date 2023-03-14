using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Pagination
{
    public class PagedResponse<T>
    {
        public PagedResponse(IReadOnlyCollection<T> items, int pageSize, int currentPage, int totalCount)
        {
            Items = items;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalCount = totalCount;
        }

        public int TotalCount { get; }

        public int CurrentPage { get; }

        public int PageSize { get; }

        public IReadOnlyCollection<T> Items { get; }

        public static async Task<PagedResponse<T>> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalCount = await source.CountAsync();
            var items = await source.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<T>(items, pageSize, pageNumber, totalCount);
        }
    }
}
