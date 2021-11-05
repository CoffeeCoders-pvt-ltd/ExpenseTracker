using System;
using System.Threading.Tasks;
using ExpenseTracker.Core.Dto;
using ExpenseTracker.Core.Dto.Account;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.Controllers.Authentication.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers.Authentication
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login(string returnUrl = "/")
        {
            var authenticationResponseDto = new AuthenticateRequestDto
            {
                ReturnUrl = returnUrl
            };

            return View(authenticationResponseDto);
        }


        /// <summary>
        /// Logout From  The  Application
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirectPreserveMethod("/");
        }

        [HttpGet]
        public IActionResult Register()
            => View(new RegisterViewModel());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return View(viewModel);
                var dto = new RegisterDto(viewModel.FirstName, viewModel.LastName, viewModel.UserName, viewModel.Password);
                await _userService.CreateUser(dto);
                this.AddSuccessMessage("user created");
                return RedirectToAction(nameof(Login));
            }
            catch (Exception e)
            {
                this.AddErrorMessage(e.Message);
                return View(viewModel);
            }
        }
    }
}