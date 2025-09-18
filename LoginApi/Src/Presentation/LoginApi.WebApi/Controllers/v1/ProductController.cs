using System;
using System.Threading.Tasks;
using LoginApi.Application.Features.Products.Commands.CreateProduct;
using LoginApi.Application.Features.Products.Commands.DeleteProduct;
using LoginApi.Application.Features.Products.Commands.UpdateProduct;
using LoginApi.Application.Features.Products.Queries.GetPagedListProduct;
using LoginApi.Application.Features.Products.Queries.GetProductById;
using LoginApi.Application.Wrappers;
using LoginApi.Domain.Products.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginApi.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class ProductController : BaseApiController
    {

        [HttpGet]
        public async Task<PagedResponse<ProductDto>> GetPagedListProduct([FromQuery] GetPagedListProductQuery model)
            => await Mediator.Send(model);

        [HttpGet]
        public async Task<BaseResult<ProductDto>> GetProductById([FromQuery] GetProductByIdQuery model)
            => await Mediator.Send(model);

        [HttpPost, Authorize]
        public async Task<BaseResult<Ulid>> CreateProduct(CreateProductCommand model)
            => await Mediator.Send(model);

        [HttpPut, Authorize]
        public async Task<BaseResult> UpdateProduct(UpdateProductCommand model)
            => await Mediator.Send(model);

        [HttpDelete, Authorize]
        public async Task<BaseResult> DeleteProduct([FromQuery] DeleteProductCommand model)
            => await Mediator.Send(model);

    }
}
