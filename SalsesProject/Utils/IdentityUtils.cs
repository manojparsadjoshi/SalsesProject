using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SalsesProject.Models;
using System.Security.Claims;

namespace SalsesProject.Utils
{
    public class IdentityUtils
    {
        public static void AddingClaimIdentity(LogInModel user, string? roles, HttpContext httpcontext)
        {
            //List of claims
            var userClaims = new List<Claim>()
            {
                  new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Username),
                new Claim(ClaimTypes.Role, roles ??"user")
            };

            //create a identity claims
            var claimsIdentity = new ClaimsIdentity(
                userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            //httpcontext , current user is authentic user
            //sing in user to httpcontext
            httpcontext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
                );
        }
    }
}
