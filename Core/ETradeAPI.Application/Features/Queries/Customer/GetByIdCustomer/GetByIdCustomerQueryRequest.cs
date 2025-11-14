using MediatR;

namespace ETradeAPI.Application.Features.Queries.Customer.GetByIdCustomer
{
    public class GetByIdCustomerQueryRequest : IRequest<GetByIdCustomerQueryResponse>
    {
        public string Id { get; set; }
    }
}
