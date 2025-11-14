using MediatR;

namespace ETradeAPI.Application.Features.Queries.Customer.GetCustomers
{
    public class GetCustomersQueryRequest : IRequest<IEnumerable<GetCustomersQueryResponse>>
    {
        public int Page { get; init; } = 0;
        public int Size { get; init; } = 5;
    }
}
