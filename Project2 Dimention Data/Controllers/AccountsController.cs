using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project2_Dimention_Data.Models.ViewModels;
using Project2_Dimention_Data.Services;

namespace Project2_Dimention_Data.Controllers
{
    public class AccountsController : Controller
    {
        private readonly Authenticate _authenticate;
        public AccountsController(Authenticate authenticate)
        {
            _authenticate = authenticate;
        
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AccountLogin model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var loggedIn = _authenticate.SignIn(model.email, model.password);
                if (loggedIn)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    return View(model);
                }
            
            }

            return View(model);
               
        }

        public IActionResult Logout()
        {
            _authenticate.SignOut();
            return RedirectToAction("Index", "Home");
        }


        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
