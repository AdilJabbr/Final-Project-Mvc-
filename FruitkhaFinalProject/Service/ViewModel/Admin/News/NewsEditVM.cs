using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.News
{
    public class NewsEditVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }

        public string? Image { get; set; }
        public IFormFile UploadImage { get; set; }
    }
    public class NewsEditVMValidator : AbstractValidator<NewsEditVM>
    {
        public NewsEditVMValidator()
        {
            RuleFor(m => m.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(70)
                .WithMessage("Title can be max 50 characters");
            RuleFor(m => m.Tags)
              .NotEmpty()
              .WithMessage("Title is required")
              .MaximumLength(70)
              .WithMessage("Title can be max 50 characters");

            RuleFor(m => m.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MaximumLength(1000)
                .WithMessage("Description can be max 200 characters");

            RuleFor(m => m.UploadImage)
                .NotNull()
                .WithMessage("Image is required")
                .Must(p => p.ContentType.Contains("image/"))
                .When(m => m.UploadImage is not null)
                .WithMessage("File must be image type")
                .Must(p => p.Length / 1024 < 500)
                .WithMessage("Image size cannot exceed 500Kb")
                .When(m => m.UploadImage is not null);
        }
    }
}
