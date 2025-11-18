using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using ETradeAPI.Application.Features.Commands.Product.RemoveProduct;
using ETradeAPI.Application.Features.Commands.Product.UpdateProduct;
using ETradeAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETradeAPI.Application.Features.Queries.Product.GetProducts;
using ETradeAPI.Application.Repositories.ProductRepositories;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMediator _mediator;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(IValidator<CreateProductCommandRequest> createProductValidator, IMediator mediator, IProductReadRepository productReadRepository, IValidator<RemoveProductCommandRequest> removeProductValidator, IValidator<UpdateProductCommandRequest> updateProductValidator, IWebHostEnvironment webHostEnvironment)
        {
            _createProductValidator = createProductValidator;
            _mediator = mediator;
            _productReadRepository = productReadRepository;
            _removeProductValidator = removeProductValidator;
            _updateProductValidator = updateProductValidator;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Upload()
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");
            IFormFileCollection files = Request.Form.Files;
            if (!files.Any())
            {
                return BadRequest();
            }
            foreach (IFormFile file in files)
            {
                if (!Path.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fullPath = Path.Combine(uploadPath, $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}");
                using FileStream stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(stream);
                await stream.FlushAsync();
            }
            return Ok();
        }
    }
}
