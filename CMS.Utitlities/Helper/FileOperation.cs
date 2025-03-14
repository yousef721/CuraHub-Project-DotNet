using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Utitlities.Helper
{
    public static class FileOperation
    {
        public static string UploadFile(IFormFile file, string FolderPath)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "./wwwroot\\Files", FolderPath, fileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
        public static void DeleteFile(string fileName, string FolderPath)
        {
            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "./wwwroot\\Files", FolderPath, fileName);
            if (System.IO.File.Exists(oldPath) )
            {
                System.IO.File.Delete(oldPath);
            }
        }
    }
}
