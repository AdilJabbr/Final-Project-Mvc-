using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICloudinaryManager
    {
        Task<string> FileCreateAsync(IFormFile file);
        Task<bool> FileDeleteAsync(string filePath);
    }
}
