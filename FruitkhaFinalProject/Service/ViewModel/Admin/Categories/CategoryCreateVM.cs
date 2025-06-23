using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Admin.Categories
{
    public class CategoryCreateVM
    {
        public string Name { get; set; }

    }
    public class CategoryCreateVMValidator : AbstractValidator<CategoryCreateVM>
    {
        public CategoryCreateVMValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(40)
                .WithMessage("Name can be max 40 characters");


        
        }
    }
}
