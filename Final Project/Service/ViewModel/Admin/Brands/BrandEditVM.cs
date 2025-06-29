using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.Brands
{
    public class BrandEditVM
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl {  get; set; }

    }
    public class BrandEditVMValidator : AbstractValidator<BrandEditVM>
    {
        public BrandEditVMValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(40)
                .WithMessage("Title can be max 50 characters");
        }
    }
}
