using System.Threading.Tasks;
using HyBrForex.Application.DTOs;
using HyBrForex.Domain.Exchange.DTOs;
using HyBrForex.Domain.Exchange.Entities;

namespace HyBrForex.Application.Interfaces.Repositories;

public interface ICommonTypeMsaterRepository : IGenericRepository<CommonTypeMsater>
{
    Task<PaginationResponseDto<CommonTypeMsaterDto>> GetPagedListAsync(int pageNumber, int pageSize, string name);
}