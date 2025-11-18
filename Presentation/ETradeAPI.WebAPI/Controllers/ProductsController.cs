using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using ETradeAPI.Application.Features.Commands.Product.RemoveProduct;
using ETradeAPI.Application.Features.Commands.Product.UpdateProduct;
using ETradeAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETradeAPI.Application.Features.Queries.Product.GetProducts;
using ETradeAPI.Application.Repositories.ProductRepositories;
using ETradeAPI.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETradeAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IValidator<CreateProductCommandRequest> _createProductValidator;
        private readonly IValidator<UpdateProductCommandRequest> _updateProductValidator;
        private readonly IValidator<RemoveProductCommandRequest> _removeProductValidator;
        private readonly IMediator _mediator;
        private readonly IFileService _fileService;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(IValidator<CreateProductCommandRequest> createProductValidator, IMediator mediator, IProductReadRepository productReadRepository, IValidator<RemoveProductCommandRequest> removeProductValidator, IValidator<UpdateProductCommandRequest> updateProductValidator, IFileService fileService)
        {
            _createProductValidator = createProductValidator;
            _mediator = mediator;
            _productReadRepository = productReadRepository;
            _removeProductValidator = removeProductValidator;
            _updateProductValidator = updateProductValidator;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsQueryRequest request)
        {
            var count = _productReadRepository.GetAll(false).Count();
            var values = await _mediator.Send(request);
            return Ok(new
            {
                count,
                Products = values
            });
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProduct([FromRoute] GetByIdProductQueryRequest request)
        {
            var value = await _mediator.Send(request);
            if (value == null)
                return NotFound();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
        {
            var validationResult = await _createProductValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage).ToArray()));
            }
            var response = await _mediator.Send(request);
            return Created("", response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
        {
            var validationResult = await _updateProductValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage).ToArray()));
            }
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] RemoveProductCommandRequest request)
        {
            var validationResult = await _removeProductValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage).ToArray()));
            }
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("[action]")]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public async Task<IActionResult> Upload()
        {
            await _fileService.UploadAsync("resource/product-images", Request.Form.Files);
            return Ok();
        }
    }
}
