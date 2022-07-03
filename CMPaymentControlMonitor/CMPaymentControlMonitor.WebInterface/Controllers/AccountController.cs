using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMPaymentControlMonitor.WebInterface.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMPaymentControlMonitor.WebInterface.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userMgr,
            SignInManager<IdentityUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        public ActionResult Login(string returnUrl)
        {
            return View("Login" , new LoginModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // Checks if there's a user with the specified username
                var user = await userManager.FindByNameAsync(loginModel.Username);
                if (user != null)
                {
                    // Signs the user out, so that the user is logged out before logging in.
                    // Nothing happens if the user isn't logged in yet.
                    await signInManager.SignOutAsync();

                    // Checks if the given password is correct. 
                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/");
                    }
                }
            }
            ModelState.AddModelError("Username", "Invalid username or password");
            return View();
        }

        [Authorize]
        public async Task<RedirectResult> LogOut(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}