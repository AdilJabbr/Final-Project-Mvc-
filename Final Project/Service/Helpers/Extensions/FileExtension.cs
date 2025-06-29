using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Extensions
{
    public static class FileExtension
    {
        private static readonly List<string> AllowedExtensions = new() { ".jpg", ".jpeg", ".png", ".webp" };
        private const long MaxFileSize = 209715200;

        public static (bool IsSuccess, string ErrorMessage) ValidateImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (false, "Fayl not found.");

            if (file.Length > MaxFileSize)
                return (false, "file 200 MB.");

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!AllowedExtensions.Contains(extension))
                return (false, "(.jpg, .png, .jpeg, .webp).");

            return (true, string.Empty);
        }
        public static bool CheckFileType(this IFormFile file, string pattern)
        {
            return file.ContentType.Contains(pattern);
        }

        public static bool CheckFileSize(this IFormFile file, int size)
        {
            return file.Length / 1024 < size;
        }

        public static async Task SaveFileToLocalAsync(this IFormFile file, string path)
        {
            using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        public static void DeleteFileFromLocal(this string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        public static string GenerateFilePath(this IWebHostEnvironment env, string folder, string fileName)
        {
            return Path.Combine(env.WebRootPath, folder, fileName);
        }
        public static async Task<string> ReadFromFileAsync(this string path)
        {
            using FileStream fs = new(path, FileMode.Open, FileAccess.Read);
            using StreamReader sr = new(fs);
            return await sr.ReadToEndAsync();
        }
    }

}
