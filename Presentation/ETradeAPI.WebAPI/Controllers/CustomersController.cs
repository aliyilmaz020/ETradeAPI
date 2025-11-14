using ETradeAPI.Application.Features.Commands.Customer.CreateCustomer;
using ETradeAPI.Application.Features.Commands.Customer.RemoveCustomer;
using ETradeAPI.Application.Features.Queries.Customer.GetByIdCustomer;
using ETradeAPI.Application.Features.Queries.Customer.GetCustomers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ETradeAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCustomersQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdCustomerQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerCommandRequest request)
        {
            await _mediator.Send(request);
            return Created("", null);
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateCustomerCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveCustomerCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
