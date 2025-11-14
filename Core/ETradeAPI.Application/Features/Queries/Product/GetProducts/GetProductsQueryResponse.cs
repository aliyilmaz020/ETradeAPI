namespace ETradeAPI.Application.Features.Queries.Product.GetProducts
{
    public class GetProductsQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
