using Project2_Dimention_Data.Models;
using Project2_Dimention_Data.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Project2_Dimention_Data.Services
{
    public class Authenticate
    {
        readonly HttpContext _httpContext;
        readonly Encrypt _cryptography;
        readonly AuthOptions _authConfiguration;
        readonly emp_infoContext _context;

        public Authenticate(
            IHttpContextAccessor contextAccessor, 
            Encrypt cryptography, 
            IOptions<AuthOptions> authConfiguration, 
            emp_infoContext context)
        {
            _httpContext = contextAccessor.HttpContext;
            _cryptography = cryptography;
            _authConfiguration = authConfiguration.Value;
            _context = context;
        }

        private AuthenticationInfo _scopeAuthInfo = null;
        public AuthenticationInfo ScopeAuthInfo
        {
            get
            {
                if (_scopeAuthInfo == null)
                {
                    AuthenticationInfo tokenInfo = null;
                    var cookie = _httpContext.Request.Cookies["AuthToken"];
                    if (!string.IsNullOrEmpty(cookie)) { 
                            tokenInfo = Autthenticateinfo(cookie); } 
                    _scopeAuthInfo = tokenInfo != null ? tokenInfo : new AuthenticationInfo();
                }
                return _scopeAuthInfo;
            }
        }


        public bool AuthenticateSignIn(string email, string password)
        {
            var userinfo = _context.Logins.Where(up => up.UserEmail == email).FirstOrDefault(u => u.UserEmail == email);
            var userjobinfo = _context.Jobdetails.Where(up => up.EmpNumber == userinfo.EmpNum).FirstOrDefault(u => u.EmpNumber == userinfo.EmpNum);
            if (userinfo == null) return false;
            var loggedpassword = _cryptography.PassWordHashing(password + userinfo.Passwordsalt);
            if (loggedpassword != userinfo.Passwordhash) return false; //Compares user pass in DB and user pass inserted
            var userRole = _context.Logins.Where(up => up.UserEmail == email).Select(up => up.UserRole); //Selects the userRole where the inserted email matches the email in the db
            AuthenticationInfo authInfo = new AuthenticationInfo()
            { //Creates a new instance of authInfo where the user info will be stored of the user that is logged in
                id = userinfo.Id,
                NameUser = userinfo.NameUser,
                SurnameUser = userinfo.SurnameUser,
                EmpNum = userinfo.EmpNum ?? default(int),
                UserEmail = userinfo.UserEmail,
                UserRole = userinfo.UserRole,
                JobRole = userjobinfo.JobRole,
                Department = userjobinfo.Department,
            };
            _httpContext.Response.Cookies.Append("AuthToken", AuthInfoToToken(authInfo)); //Created the token after encrypting it and stores it in cookies
            return true;
        }


        public void SignOutAuthUser() { _httpContext.Response.Cookies.Delete("AuthToken"); } // Deletes the token

        private string AuthInfoToToken(AuthenticationInfo authInfo) { //Encrypts the session token
            var serializeAuthInfo = JsonConvert.SerializeObject(authInfo);
            var key = Encoding.UTF8.GetBytes(_authConfiguration.AuthEncryptionKey);
            var iv = Aes.Create().IV;
            var ivBase64 = Convert.ToBase64String(iv);
            var encBytes = _cryptography.EncryptStringToBytes_Aes(serializeAuthInfo, key, iv);
            var result = $"{ivBase64.Length.ToString().PadLeft(3, '0')}{ivBase64}{Convert.ToBase64String(encBytes)}";
            return result; }

        private AuthenticationInfo Autthenticateinfo(string Sessiontoken) { //Decrypts the session token 
            string decryptedToken;
            var ivLength = Convert.ToInt32(Sessiontoken.Substring(0, 3));
            var ivBase64 = Sessiontoken.Substring(3, ivLength);
            var iv = Convert.FromBase64String(ivBase64);
            var encBase64 = Sessiontoken.Substring(ivLength + 3);
            var encBytes = Convert.FromBase64String(encBase64);
            var key = Encoding.UTF8.GetBytes(_authConfiguration.AuthEncryptionKey);
            decryptedToken = _cryptography.DecryptStringFromBytes_Aes(encBytes, key, iv);
            var result = JsonConvert.DeserializeObject<AuthenticationInfo>(decryptedToken);
            return result; }
    }
}
