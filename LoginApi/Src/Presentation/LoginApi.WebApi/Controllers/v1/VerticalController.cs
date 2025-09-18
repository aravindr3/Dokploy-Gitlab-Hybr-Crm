using HyBrCRM.Application.DTOs.Domain.Request;
using System.Threading.Tasks;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrCRM.Application.DTOs.Verticals.Request;
using HyBrForex.Infrastructure.Identity.Services;
using HyBrForex.Application.DTOs.Branch.Request;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.DTOs.Branch.Response;
using HyBrCRM.Application.DTOs.Verticals.Response;

namespace HyBrCRM.WebApi.Controllers.v1
{
    [ApiVersion("1")]

    public class VerticalController(IVerticalServices verticalServices) : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult<string>> CreateVertical(VerticalRequest model)
        {
            return await verticalServices.CreateVertical(model);
        }

        [HttpDelete]
        public async Task<BaseResult> DeleteVertical(string id)
        {
            return await verticalServices.DeleteVertical(id);
        }

        [HttpPut]
        public async Task<BaseResult> UpdateVertical(string id, VerticalRequest model)
        {
            return await verticalServices.UpdateVertical(id, model);
        }

        [HttpGet]
        public async Task<BaseResult> GetVerticalById(string id)
        {
            return await verticalServices.GetVerticalById(id);
        }

        [HttpGet]

        public async Task<BaseResult<VerticalResponse>> GetAllVertical()
        {
            return await verticalServices.GetAllVerticals();
        }
    }
}
