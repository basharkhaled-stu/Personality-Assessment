using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace PersonalityAssessment.Application.Common.DTOS
{
    public static class IQueryableExtensions
    {
        public static async Task<PagedResult<TDestination>> ToPagedResultAsync<TEntity, TDestination>(
            this IQueryable<TEntity> query,
            IMapper mapper,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
            where TEntity : class
        {

            var totalCount = await query.CountAsync(cancellationToken);


            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TDestination>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);


            return new PagedResult<TDestination>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
