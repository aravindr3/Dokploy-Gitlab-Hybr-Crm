using HyBrForex.Application.DTOs.FunctionalMaster.Requests;
using HyBrForex.Application.DTOs.FunctionalMaster.Responses;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Infrastructure.Identity.Services;
using HyBrForex.Application.Wrappers;
using LoginApi.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrForex.Application.DTOs.Feature.Response;
using HyBrForex.Application.DTOs.Feature.Request;

namespace HyBrForex.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class FeatureController(IFeatureService featureService) : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult<FeatureResponse>> Create([FromBody] FeatureRequest request)
        {
            var result = await featureService.CreateAsync(request);
            return (result);
        }

        [HttpPost("{id}")]
        public async Task<BaseResult<FeatureResponse>> UpdateAsync(string id, [FromBody] FeatureRequest request)
        {
            var result = await featureService.UpdateAsync(id, request);
            return (result);
        }

        [HttpDelete("{id}")]
        public async Task<BaseResult<bool>> Delete(string id)
        {
            var Sucess = await featureService.DeleteAsync(id);
            return Sucess;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await featureService.GetAllAsync();
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await featureService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }
    }
}

