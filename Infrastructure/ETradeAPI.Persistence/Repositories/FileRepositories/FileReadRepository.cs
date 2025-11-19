using ETradeAPI.Application.Repositories.FileRepositories;
using ETradeAPI.Persistence.Contexts;
using F = ETradeAPI.Domain.Entities;

namespace ETradeAPI.Persistence.Repositories.FileRepositories
{
    public class FileReadRepository(ETradeApiContext context) : ReadRepository<F.File>(context), IFileReadRepository
    {
    }
}
