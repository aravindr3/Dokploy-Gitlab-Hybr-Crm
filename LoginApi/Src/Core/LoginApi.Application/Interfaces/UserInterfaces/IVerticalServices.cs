using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Application.DTOs.Domain.Request;
using HyBrCRM.Application.DTOs.Verticals.Data;
using HyBrCRM.Application.DTOs.Verticals.Request;
using HyBrCRM.Application.DTOs.Verticals.Response;
using HyBrForex.Application.DTOs.Branch.Data;
using HyBrForex.Application.DTOs.Branch.Request;
using HyBrForex.Application.DTOs.Branch.Response;
using HyBrForex.Application.Wrappers;

namespace HyBrCRM.Application.Interfaces.UserInterfaces
{
    public interface IVerticalServices
    {
        Task<BaseResult<string>> CreateVertical (VerticalRequest model);
        Task<BaseResult> DeleteVertical(string id);
        Task<BaseResult> UpdateVertical(string id, VerticalRequest model);

        Task<BaseResult<VerticalData>> GetVerticalById(string id);
        Task<IReadOnlyList<VerticalResponse>> GetAllAsync();

        Task<BaseResult<VerticalResponse>> GetAllVerticals();




    }
}
