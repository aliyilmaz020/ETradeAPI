using ETradeAPI.Application.Services;
using ETradeAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETradeAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            List<(string fileName, string path)> datas = [];
            List<bool> results = [];
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(uploadPath, file.FileName);

                datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                results.Add(result);
            }
            if (results.TrueForAll(x => x.Equals(true)))
            {
                return datas;
            }
            return null;
            //todo dosya yükleme hatası olursa exception fırlat
        }
    }
}
