﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.Brands
{
    public class BrandVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }    
        public string? ImageUrl { get; set; } 
    }
}
