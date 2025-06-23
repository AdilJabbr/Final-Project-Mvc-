using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.DTOs.Account;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;

        public AccountService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailService emailService,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;

            // Create UrlHelper for generating URLs
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterVM request)
        {
            AppUser user = new()
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded) return result;

            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string url = _urlHelper.Action("ConfirmEmail", "Account", new
            {
                userId = user.Id,
                token
            }, _httpContextAccessor.HttpContext.Request.Scheme);

            string subject = "Welcome to FruitKha";
            string emailHtml = File.ReadAllText("wwwroot/templates/register-confirm.html")
                                   .Replace("{{link}}", url)
                                   .Replace("{{fullName}}", user.FullName);

            _emailService.Send(user.Email, subject, emailHtml);

            return result;
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> LoginAsync(LoginVM request)
        {
            AppUser dbUser = await _userManager.FindByEmailAsync(request.EmailOrUsername);

            if (dbUser is null)
            {
                dbUser = await _userManager.FindByNameAsync(request.EmailOrUsername);
            }

            if (dbUser is null)
            {
                return Microsoft.AspNetCore.Identity.SignInResult.Failed;
            }

            var result = await _signInManager.PasswordSignInAsync(dbUser, request.Password, request.IsRememberMe, false);

            return result;
        }

        public async Task ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) throw new ArgumentException("User not found");

            await _userManager.ConfirmEmailAsync(user, token);
            await _signInManager.SignInAsync(user, false);
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordVM model)
        {
            var existUser = await _userManager.FindByEmailAsync(model.Email);

            if (existUser is null || !existUser.EmailConfirmed)
            {
                return false;
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);

            string link = _urlHelper.Action("ResetPassword", "Account", new
            {
                userId = existUser.Id,
                token
            }, _httpContextAccessor.HttpContext.Request.Scheme);

            string subject = "Reset Password";
            string html = File.ReadAllText("wwwroot/templates/reset-password.html");

            html = html.Replace("{{fullName}}", existUser.FullName)
                       .Replace("{{link}}", link);

            _emailService.Send(existUser.Email, subject, html);

            return true;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            var passwordHasher = new PasswordHasher<AppUser>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                return IdentityResult.Failed(new IdentityError { Description = "New password cannot be the same as the old password." });
            }

            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return IdentityResult.Failed(new IdentityError { Description = "New password can't be same as old password." });
            }

            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return IdentityResult.Failed(new IdentityError { Description = "New password can't be same as old password" });
            }

            return await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task CreateRoleAsync()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }

        public async Task<AppUser> FindUserByIdAsync(string userId)
        {
            if (userId == null)
            {
                throw new NotFoundException("not found");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new NotFoundException("not found");

            }
            return user;
        }

    
    }
}
