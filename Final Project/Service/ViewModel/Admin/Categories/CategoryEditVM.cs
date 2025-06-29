using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Admin.Categories
{
    public class CategoryEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
    public class CategoryEditDtoValidator : AbstractValidator<CategoryEditVM>
    {
        public CategoryEditDtoValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(40)
                .WithMessage("Name can be max 40 characters");


        }
    }
}
