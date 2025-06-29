using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.Contact
{
    public class ContactCreateVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string? Answer { get; set; }
        public bool IsAnswer { get; set; }
    }
    //public class ContactCreateVMValidator : AbstractValidator<ContactCreateVM>
    //{
    //    public ContactCreateVMValidator()
    //    {
    //        RuleFor(m => m.Email)
    //            .NotEmpty()
    //            .WithMessage("Email is required");

    //        RuleFor(m => m.Message)
    //         .NotEmpty()
    //         .WithMessage("Message is required")
    //         .MaximumLength(400)
    //         .WithMessage("Message can be max 400 characters");

    //        RuleFor(m => m.Name)
    //       .NotEmpty()
    //       .WithMessage("Name is required")
    //       .MaximumLength(50)
    //       .WithMessage("Name can be max 50 characters");

    //        RuleFor(m => m.Subject)
    //       .NotEmpty()
    //       .WithMessage("Subject is required")
    //       .MaximumLength(100)
    //       .WithMessage("Subject can be max 100 characters");

    //    }
    //}
}
