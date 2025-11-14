namespace ETradeAPI.Application.Features.Queries.Order.GetOrders
{
    public class GetOrdersQueryResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string? Customer { get; set; }
    }
}
