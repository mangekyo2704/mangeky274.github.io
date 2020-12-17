using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FruitShopSolution.Application.Common
{
    public interface IStorageService
    {
        string GetFileURL(string fileName);
/*        void SetFileURL(string fileName);*/
        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
        Task DeleteFileAsync(string fileName);
    }
}
