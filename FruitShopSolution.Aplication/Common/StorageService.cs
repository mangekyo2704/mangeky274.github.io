using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FruitShopSolution.Application.Common
{
    public class StorageService : IStorageService
    {
        private readonly string _userContentFolder;
        private readonly string _userContentFolder2;
        private const string USER_CONTENT_FOLDER_NAME = "Image/Products";
        public StorageService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
            Console.WriteLine("1"+_userContentFolder);
            _userContentFolder2 = Path.Combine("C:\\Users\\vnngu\\Source\\Repos\\mangeky274.github.io\\FruitShopSolution.UI\\wwwroot\\Image\\Products");
            Console.WriteLine("2" +_userContentFolder2);
        }
        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
/*        public void SetFileURL(string fileName)
        {
            USER_CONTENT_FOLDER_NAME = fileName;
        }*/
        public string GetFileURL(string fileName)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            var filePath2 = Path.Combine(_userContentFolder2, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
             mediaBinaryStream.CopyTo(output);
            string url = _userContentFolder + "/" + fileName;
            string url2 = _userContentFolder2 + "/" + fileName;
            File.Copy(filePath, filePath2, true);
        }
    }
}
