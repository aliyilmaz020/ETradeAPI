using F = ETradeAPI.Domain.Entities;

namespace ETradeAPI.Application.Repositories.FileRepositories
{
    public interface IFileWriteRepository : IWriteRepository<F.File>
    {
    }
}
