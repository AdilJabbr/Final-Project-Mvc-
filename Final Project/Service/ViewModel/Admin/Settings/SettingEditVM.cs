using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.Settings
{
    public class SettingEditVM
    {
        public int Id { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
        public IFormFile? UploadedImage { get; set; }
    }
}
