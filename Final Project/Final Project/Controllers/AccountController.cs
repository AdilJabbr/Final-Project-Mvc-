﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Service.DTOs.Account;
using Service.Services.Interfaces;

namespace Final_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(IAccountService accountService, RoleManager<IdentityRole> roleManager)
        {
            _accountService = accountService;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM request)
        {

            if (!ModelState.IsValid) return View(request);

            var result = await _accountService.RegisterAsync(request);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(request);
            }

            return RedirectToAction(nameof(VerifyEmail));
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM request)
        {

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _accountService.LoginAsync(request);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login informations is wrong");
                return View(request);
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId is null || token is null)
                return RedirectToAction("Index", "Error");

            try
            {
                await _accountService.ConfirmEmailAsync(userId, token);
            }
            catch
            {
                return RedirectToAction("Index", "Error");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool sent = await _accountService.ForgotPasswordAsync(model);

            if (!sent)
            {
                ModelState.AddModelError("Email", "User is not found.");
                return View(model);
            }

            TempData["Email"] = model.Email;
            return RedirectToAction(nameof(VerifyResetPassword));
        }

        [HttpGet]
        public IActionResult VerifyResetPassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            return View(new ResetPasswordVM { UserId = userId, Token = token });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _accountService.ResetPasswordAsync(model);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            return RedirectToAction(nameof(Login));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }


        //Create Roles Method

        [HttpGet]
        public async Task<IActionResult> CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
            return Ok();
        }
    }
}
