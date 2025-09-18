using LoginApi.Application.Features.Products.Commands.CreateProduct;
using LoginApi.Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using LoginApi.Application.Features.CommonTypeMsaters.Commands.CreateCommonTypeMsater;

namespace LoginApi.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class CommonTypeMsaterController : BaseApiController
    {
        [HttpPost]
        public async Task<UlidBaseResult<Ulid>> CreateProduct(CreateCommonTypeMsaterCommand model)
           => await Mediator.Send(model);

    }
}
