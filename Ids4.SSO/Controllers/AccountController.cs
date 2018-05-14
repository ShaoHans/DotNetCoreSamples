using IdentityServer4.Test;
using Ids4.SSO.Data;
using Ids4.SSO.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;

namespace Ids4.SSO.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;

        public AccountController(SignInManager<AppUser> singInManager
            , UserManager<AppUser> userManager
            ,IIdentityServerInteractionService identityServerInteractionService)
        {
            _signInManager = singInManager;
            _userManager = userManager;
            _identityServerInteractionService = identityServerInteractionService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(model.UserName);
                if(user == null)
                {
                    ModelState.AddModelError(nameof(model.UserName), "用户名错误");
                }
                else
                {
                    if(await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        await _signInManager.SignInAsync(
                            user,
                            new AuthenticationProperties
                            {
                                IsPersistent = true,
                                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(45)
                            }
                            );

                        if(_identityServerInteractionService.IsValidReturnUrl(returnUrl))
                        {
                            return RedirectToLocal(returnUrl);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(model.Password), "密码错误");
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToLocal();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult RedirectToLocal(string returnUrl= "")
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
