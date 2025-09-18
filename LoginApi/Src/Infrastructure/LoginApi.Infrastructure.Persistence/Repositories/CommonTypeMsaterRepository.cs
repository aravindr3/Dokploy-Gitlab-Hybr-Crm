using System.Linq;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Domain.Exchange.DTOs;
using HyBrForex.Domain.Exchange.Entities;
using HyBrForex.Infrastructure.Persistence.Contexts;

namespace HyBrForex.Infrastructure.Persistence.Repositories;

public class CommonTypeMsaterRepository(ApplicationDbContext dbContext)
    : GenericRepository<CommonTypeMsater>(dbContext), ICommonTypeMsaterRepository
{
    public async Task<PaginationResponseDto<CommonTypeMsaterDto>> GetPagedListAsync(int pageNumber, int pageSize,
        string TypeName)
    {
        var query = dbContext.CommonTypeMsater.OrderBy(p => p.Created).AsQueryable();

        if (!string.IsNullOrEmpty(TypeName)) query = query.Where(p => p.TypeName.Contains(TypeName));

        return await Paged(
            query.Select(p => new CommonTypeMsaterDto(p.TypeName)),
            pageNumber,
            pageSize);
    }
}