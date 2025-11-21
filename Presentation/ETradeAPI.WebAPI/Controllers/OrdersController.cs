using ETradeAPI.Application.Features.Commands.Order.CreateOrder;
using ETradeAPI.Application.Features.Commands.Order.RemoveOrder;
using ETradeAPI.Application.Features.Commands.Order.UpdateOrder;
using ETradeAPI.Application.Features.Queries.Order.GetByIdOrder;
using ETradeAPI.Application.Features.Queries.Order.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETradeAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(
        IMediator mediator
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetOrdersQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdOrderQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderCommandRequest request)
        {
            await mediator.Send(request);
            return Created("", null);
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateOrderCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveOrderCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
