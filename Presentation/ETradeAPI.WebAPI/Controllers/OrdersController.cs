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
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateOrderCommandRequest> _createValidator;
        private readonly IValidator<UpdateOrderCommandRequest> _updateValidator;
        private readonly IValidator<RemoveOrderCommandRequest> _removeValidator;

        public OrdersController(IMediator mediator, IValidator<CreateOrderCommandRequest> createValidator, IValidator<UpdateOrderCommandRequest> updateValidator, IValidator<RemoveOrderCommandRequest> removeValidator)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _removeValidator = removeValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetOrdersQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdOrderQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderCommandRequest request)
        {
            var validationResult = await _createValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage).ToArray()));
            }
            await _mediator.Send(request);
            return Created("", null);
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateOrderCommandRequest request)
        {
            var validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage).ToArray()));
            }
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveOrderCommandRequest request)
        {
            var validationResult = await _removeValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage).ToArray()));
            }
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
