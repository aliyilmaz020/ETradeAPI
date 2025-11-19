using ETradeAPI.Infrastructure.Operations;

namespace ETradeAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFileDelegate(string pathOrContainerName, string fileName);
        protected static async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFileDelegate hasFile)
        {
            string newFileName = await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";

                if (hasFile(pathOrContainerName, newFileName))
                {
                    return await FileRenameAsync(pathOrContainerName,
                        $"{Path.GetFileNameWithoutExtension(newFileName)}_{DateTime.Now:ddMMyyyyHHmmss}{extension}",
                        hasFile);
                }
                else
                {
                    return newFileName;
                }
            });
            return newFileName;
        }
    }
}
