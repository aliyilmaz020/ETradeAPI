using ETradeAPI.Application.Features.Commands.Order.CreateOrder;
using ETradeAPI.Application.Features.Commands.Order.RemoveOrder;
using ETradeAPI.Application.Features.Commands.Order.UpdateOrder;
using ETradeAPI.Application.Features.Queries.Order.GetByIdOrder;
using ETradeAPI.Application.Features.Queries.Order.GetOrders;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETradeAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(
        IMediator mediator,
        IValidator<CreateOrderCommandRequest> createValidator,
        IValidator<UpdateOrderCommandRequest> updateValidator,
        IValidator<RemoveOrderCommandRequest> removeValidator
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
            var validationResult = await createValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage).ToArray()));
            }
            await mediator.Send(request);
            return Created("", null);
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateOrderCommandRequest request)
        {
            var validationResult = await updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage).ToArray()));
            }
            var response = await mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveOrderCommandRequest request)
        {
            var validationResult = await removeValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage).ToArray()));
            }
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
