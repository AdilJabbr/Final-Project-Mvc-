using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Service.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterVM request);
        Task<SignInResult> LoginAsync(LoginVM request);
        Task ConfirmEmailAsync(string userId, string token);
        Task<bool> ForgotPasswordAsync(ForgotPasswordVM model);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordVM model);
        Task<AppUser> FindUserByIdAsync(string Id);

        Task LogoutAsync();



    }
}
