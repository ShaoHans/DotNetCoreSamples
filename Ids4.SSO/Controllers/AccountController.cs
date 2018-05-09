using IdentityServer4.Test;
using Ids4.SSO.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ids4.SSO.Controllers
{
    public class AccountController : Controller
    {
        private readonly TestUserStore _userStore;

        public AccountController(TestUserStore userStore)
        {
            _userStore = userStore;
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
                TestUser user = _userStore.FindByUsername(model.UserName);
                if(user == null)
                {
                    ModelState.AddModelError(nameof(model.UserName), "用户名错误");
                }
                else
                {
                    if(_userStore.ValidateCredentials(user.Username, model.Password))
                    {
                        await Microsoft.AspNetCore.Http.AuthenticationManagerExtensions.SignInAsync(
                            HttpContext,
                            user.SubjectId,
                            user.Username,
                            new AuthenticationProperties
                            {
                                IsPersistent = true,
                                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(45)
                            }
                            );
                        return RedirectToLocal(returnUrl);
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
            await HttpContext.SignOutAsync();
            return RedirectToLocal();
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
