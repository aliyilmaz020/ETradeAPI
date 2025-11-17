using AutoMapper;
using ETradeAPI.Application.Features.Commands.Customer.CreateCustomer;
using ETradeAPI.Application.Features.Commands.Customer.UpdateCustomer;
using ETradeAPI.Application.Features.Commands.Order.CreateOrder;
using ETradeAPI.Application.Features.Commands.Order.UpdateOrder;
using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using ETradeAPI.Application.Features.Commands.Product.UpdateProduct;
using ETradeAPI.Application.Features.Queries.Customer.GetByIdCustomer;
using ETradeAPI.Application.Features.Queries.Customer.GetCustomers;
using ETradeAPI.Application.Features.Queries.Order.GetByIdOrder;
using ETradeAPI.Application.Features.Queries.Order.GetOrders;
using ETradeAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETradeAPI.Application.Features.Queries.Product.GetProducts;
using ETradeAPI.Domain.Entities;

namespace ETradeAPI.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Product, CreateProductCommandRequest>().ReverseMap();
            CreateMap<Product, UpdateProductCommandRequest>().ReverseMap();
            CreateMap<Product, GetByIdProductQueryResponse>().ReverseMap();
            CreateMap<Product, GetProductsQueryResponse>().ReverseMap();

            CreateMap<Customer, CreateCustomerCommandRequest>().ReverseMap();
            CreateMap<Customer, UpdateCustomerCommandRequest>().ReverseMap();
            CreateMap<Customer, GetCustomersQueryResponse>().ReverseMap();
            CreateMap<Customer, GetByIdCustomerQueryResponse>().ReverseMap();

            CreateMap<Order, CreateOrderCommandRequest>().ReverseMap();
            CreateMap<Order, UpdateOrderCommandRequest>().ReverseMap();
            CreateMap<Order, GetByIdOrderQueryResponse>().ForMember(x => x.Customer, y => y.MapFrom(z => $"{z.Customer.Name} {z.Customer.Surname}")).ReverseMap();
            CreateMap<Order, GetOrdersQueryResponse>().ForMember(x => x.Customer, y => y.MapFrom(z => $"{z.Customer.Name} {z.Customer.Surname}")).ReverseMap();

        }
    }
}
