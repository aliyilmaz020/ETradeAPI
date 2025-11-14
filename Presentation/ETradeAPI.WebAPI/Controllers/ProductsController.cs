using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using ETradeAPI.Application.Features.Commands.Product.RemoveProduct;
using ETradeAPI.Application.Features.Commands.Product.UpdateProduct;
using ETradeAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETradeAPI.Application.Features.Queries.Product.GetProducts;
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
        private readonly IMediator _mediator;

        public ProductsController(IValidator<CreateProductCommandRequest> createProductValidator, IMediator mediator)
        {
            _createProductValidator = createProductValidator;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsQueryRequest request)
        {
            var values = await _mediator.Send(request);

            return Ok(values);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProduct([FromRoute] GetByIdProductQueryRequest request)
        {
            var value = await _mediator.Send(request);
            if(value == null)
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
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] RemoveProductCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
