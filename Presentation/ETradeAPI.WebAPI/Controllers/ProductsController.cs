using ETradeAPI.Application.Repositories.OrderRepositories;
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

        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
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
            await _productWriteRepository.CreateAsync(new Product { Id = Guid.NewGuid(), CreatedDate = DateTime.UtcNow, Name = "p1", Price = 100, Stock = 190 });
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpGet("get-order")]
        public async Task<IActionResult> GetOrder()
        {
            //await _orderWriteRepository.CreateAsync(new()
            //{
            //    Address = "Test",
            //    Description = "Deneme"
            //});
            var value = await _orderReadRepository.GetByIdAsync("019a7345-1565-7a62-8a22-29040a001feb");
            value.Address = "....Test";
            value.Description = "aaaa";
            await _orderWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
