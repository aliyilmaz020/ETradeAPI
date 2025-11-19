using ETradeAPI.Application.Abstractions.Storage.Azure;
using Microsoft.AspNetCore.Http;

namespace ETradeAPI.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage : IAzureStorage
    {
        public Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFiles(string pathOrContainerName)
        {
            throw new NotImplementedException();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<List<(string fileName, string path)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            throw new NotImplementedException();
        }
    }
}
