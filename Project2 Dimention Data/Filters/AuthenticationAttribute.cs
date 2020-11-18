using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Project2_Dimention_Data.Filters
{
    public class AuthenticationAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        readonly List<string> UserRole;

        public AuthenticationAttribute(string userRoles = "")
        {
            UserRole = userRoles.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var service = context.HttpContext.RequestServices.GetService(typeof(Services.Authenticate)) as Services.Authenticate;

            if (service.ScopeAuthInfo.IsAuthenticated && !UserRole.Any())
            {
                return; 
            }

            if (!service.ScopeAuthInfo.IsAuthenticated || !UserRole.Any(p => service.ScopeAuthInfo.UserRole.Contains(p)))
            {
                var returnUrl = context.HttpContext.Request.Path;
                context.Result = new RedirectToActionResult("Login", "Accounts", new { returnUrl = returnUrl });
            }
        }
    }
}
