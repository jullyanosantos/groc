using Microsoft.EntityFrameworkCore;

namespace Microservices.Application.Helpers.PaginationHelper
{
    public static class Pagination
    {
        public async static Task<PagedResult<T>> GetPaged<T>(this IQueryable<T> query,
                                                 int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = await query.CountAsync()
            };

            var skip = (page - 1) * pageSize;
            result.Items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }
    }
}