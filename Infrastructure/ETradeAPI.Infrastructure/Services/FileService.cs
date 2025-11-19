using ETradeAPI.Infrastructure.Operations;

namespace ETradeAPI.Infrastructure.Services
{
    public class FileService
    {
        async private Task<string> FileRenameAsync(string path, string fileName)
        {
            string newFileName = await Task.Run<string>(async () =>
               {
                   string extension = Path.GetExtension(fileName);
                   string oldName = Path.GetFileNameWithoutExtension(fileName);
                   string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";

                   if (File.Exists($"{path}\\{newFileName}"))
                   {
                       return await FileRenameAsync(path, $"{Path.GetFileNameWithoutExtension(newFileName)}_{DateTime.Now:ddMMyyyyHHmmss}{extension}");
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
