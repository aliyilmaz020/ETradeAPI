using ETradeAPI.Application.Repositories.InvoiceFileRepositories;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.InvoiceFileRepositories
{
    public class InvoiceFileReadRepository(ETradeApiContext context) : ReadRepository<InvoiceFile>(context), IInvoiceFileReadRepository
    {
    }
}
