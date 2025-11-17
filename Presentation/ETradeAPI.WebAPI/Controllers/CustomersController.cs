using ETradeAPI.Application.Features.Commands.Customer.CreateCustomer;
using ETradeAPI.Application.Features.Commands.Customer.RemoveCustomer;
using ETradeAPI.Application.Features.Commands.Customer.UpdateCustomer;
using ETradeAPI.Application.Features.Queries.Customer.GetByIdCustomer;
using ETradeAPI.Application.Features.Queries.Customer.GetCustomers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETradeAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateCustomerCommandRequest> _createValidator;
        private readonly IValidator<UpdateCustomerCommandRequest> _updateValidator;
        private readonly IValidator<RemoveCustomerCommandRequest> _removeValidator;

        public CustomersController(IMediator mediator, IValidator<CreateCustomerCommandRequest> createValidator, IValidator<UpdateCustomerCommandRequest> updateValidator, IValidator<RemoveCustomerCommandRequest> removeValidator)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _removeValidator = removeValidator;
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
        public async Task<IActionResult> Put(UpdateCustomerCommandRequest request)
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
        public async Task<IActionResult> Delete([FromRoute] RemoveCustomerCommandRequest request)
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
