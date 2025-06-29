using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.Brands
{
    public class BrandCreateVM
    {
        public string Name { get; set; } = null!;
        public IFormFile ImageFile { get; set; } = null!;
    }
    public class BrandCreateVMValidator : AbstractValidator<BrandCreateVM>
    {
        public BrandCreateVMValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(40)
                .WithMessage("Name can be max 40 characters");


        }
    }
}
