using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Feature.Request;
using HyBrForex.Application.DTOs.Feature.Response;
using HyBrForex.Application.DTOs.FunctionalMaster.Requests;
using HyBrForex.Application.DTOs.FunctionalMaster.Responses;
using HyBrForex.Application.Wrappers;

namespace HyBrForex.Application.Interfaces.UserInterfaces
{
    public interface IFeatureService
    {
        Task<BaseResult<FeatureResponse>> CreateAsync(FeatureRequest request);
        Task<BaseResult<FeatureResponse>> UpdateAsync(string id, FeatureRequest request);
        Task<BaseResult<bool>> DeleteAsync(string id);
        Task<BaseResult<List<FeatureResponse>>> GetAllAsync();
        Task<BaseResult<FeatureResponse>> GetByIdAsync(string id);
    }
}
