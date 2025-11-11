using ETradeAPI.Application.Repositories.ProductRepositories;
using ETradeAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ETradeAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet("get-products")]
        public IActionResult GetProducts()
        {
            var values = _productReadRepository.GetAll();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var value = await _productReadRepository.GetByIdAsync(id);
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct()
        {
            await _productWriteRepository.CreateAsync(new Product { Id = Guid.NewGuid(),CreatedDate=DateTime.UtcNow,Name="p1",Price=100,Stock=190});
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
