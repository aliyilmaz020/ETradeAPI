using ETradeAPI.Application.Abstractions.Storage;
using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using ETradeAPI.Application.Features.Commands.Product.RemoveProduct;
using ETradeAPI.Application.Features.Commands.Product.UpdateProduct;
using ETradeAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETradeAPI.Application.Features.Queries.Product.GetProducts;
using ETradeAPI.Application.Repositories.ProductImageFileRepositories;
using ETradeAPI.Application.Repositories.ProductRepositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETradeAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(
        IValidator<CreateProductCommandRequest> createProductValidator,
        IMediator mediator,
        IProductReadRepository productReadRepository,
        IValidator<RemoveProductCommandRequest> removeProductValidator,
        IValidator<UpdateProductCommandRequest> updateProductValidator,
        IProductImageFileWriteRepository productImageFileWriteRepository,
        IStorageService storageService
        ) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsQueryRequest request)
        {
            var count = productReadRepository.GetAll(false).Count();
            var values = await mediator.Send(request);
            return Ok(new
            {
                count,
                Products = values
            });
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProduct([FromRoute] GetByIdProductQueryRequest request)
        {
            var value = await mediator.Send(request);
            if (value == null)
                return NotFound();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
        {
            var validationResult = await createProductValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage).ToArray()));
            }
            var response = await mediator.Send(request);
            return Created("", response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
        {
            var validationResult = await updateProductValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage).ToArray()));
            }
            var response = await mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] RemoveProductCommandRequest request)
        {
            var validationResult = await removeProductValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage).ToArray()));
            }
            var response = await mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("[action]")]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public async Task<IActionResult> Upload()
        {
            var images = await storageService.UploadAsync("resource/files", Request.Form.Files);

            await productImageFileWriteRepository.AddRangeAsync(images.Select(i => new Domain.Entities.ProductImageFile
            {
                FileName = i.fileName,
                Path = i.path,
                Storage = storageService.StorageName
            }).ToList());
            await productImageFileWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
