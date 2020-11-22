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

        //Displays the Login Page
        public IActionResult Login(string returnUrl = null) // Will direct a user to the login page if the login button is clikced
        {
            ViewData["ReturnUrl"] = returnUrl; //Saves the page the user was on when he was redirected to the login page so that the user will be redirected back to that page after logging in
            return View(); //Redirect to /Accounts/Login.cshtml
        }


        //Logs A user in 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AccountLogin model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var loggedIn = _authenticate.AuthenticateSignIn(model.email, model.password); //Passes the email and password inserted in the login page to be authenticated
                if (loggedIn) // Will return true if the user is logged in
                {
                    return RedirectToLocal(returnUrl); //Will redirect to the page that you tried to enter before being redirected to the login page
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt"); //Throws an exeption if the user input is invalid
                    return View(model);
                }
            
            }

            return View(model);
               
        }

        //Logs a user out by calling SignOutAuthUser that will delete the session token
        public IActionResult Logout()
        {
            _authenticate.SignOutAuthUser(); // Calls SignOutAuthUser to delete session token
            return RedirectToAction("Index", "Home"); //Returns the user to the home page
        }

        //Will redirect a user to the home page thus calling the \Home\Index.cshtml
        //Can be found at C:\Users\Kyle\source\repos\IT DEV Project 2\Project2 Dimention Data\Views\Home\Index.cshtml
        private IActionResult RedirectToLocal(string returnUrl) //Function is called to redirect users
        {
            if (Url.IsLocalUrl(returnUrl) && !(returnUrl.Contains("Login"))) // If the user clicked on login page directly they will not be redirected to login page again if logged in
                                                                             // but will be redirected to home page.
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home"); // references to (\Home\Index.cshtml) Redirects to home page
            }
        }
    }
}
