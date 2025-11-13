using ETradeAPI.Application.Repositories.ProductRepositories;
using ETradeAPI.Application.ViewModels.Products;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETradeAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IValidator<CreateProductVM> _createProductValidator;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IValidator<CreateProductVM> createProductValidator)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _createProductValidator = createProductValidator;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var values = _productReadRepository.GetAll(false);
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var value = await _productReadRepository.GetByIdAsync(id, false);
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductVM model)
        {
            var validationResult = await _createProductValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage).ToArray()));
            }
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Stock = model.Stock,
                Price = model.Price
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductVM model)
        {
            var product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Name = model.Name;
            product.Stock = model.Stock;
            product.Price = model.Price;
            _productWriteRepository.Update(product);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
