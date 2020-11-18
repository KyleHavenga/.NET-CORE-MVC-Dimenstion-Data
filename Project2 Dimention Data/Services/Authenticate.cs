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
        readonly Cryptography _cryptography;
        readonly AuthOptions _authConfiguration;
        readonly emp_infoContext _context;

        public Authenticate(
            IHttpContextAccessor contextAccessor, 
            Cryptography cryptography, 
            IOptions<AuthOptions> authConfiguration, 
            emp_infoContext context)
        {
            _httpContext = contextAccessor.HttpContext;
            _cryptography = cryptography;
            _authConfiguration = authConfiguration.Value;
            _context = context;
        }

        private AuthInfo _scopeAuthInfo = null;
        public AuthInfo ScopeAuthInfo
        {
            get
            {
                if (_scopeAuthInfo == null)
                {
                    AuthInfo tokenAuthInfo = null;
                    var cookieValue = _httpContext.Request.Cookies["AuthToken"];
                    if (!string.IsNullOrEmpty(cookieValue))
                    {
                        try
                        {
                            tokenAuthInfo = AuthInfoFromToken(cookieValue);
                        }
                        catch
                        {
                        }
                    }
                    _scopeAuthInfo = tokenAuthInfo != null ? tokenAuthInfo : new AuthInfo();

                }
                return _scopeAuthInfo;
            }
        }

        public bool SignIn(string email, string password)
        {
            var user = _context.Logins.Where(up => up.UserEmail == email).FirstOrDefault(u => u.UserEmail == email); //can be an issue
            var userjobinfo = _context.Jobdetails.Where(up => up.EmpNumber == user.EmpNum).FirstOrDefault(u => u.EmpNumber == user.EmpNum);
            if (user == null) return false;

            var userCredential = user;
            var claimedPasswordHashed = _cryptography.HashSHA256(password + userCredential.Passwordsalt); 

            if(claimedPasswordHashed != userCredential.Passwordhash) return false;

            var userRole =_context.Logins.Where(up => up.UserEmail == email).Select(up => up.UserRole);

            AuthInfo authInfo = new AuthInfo()
            {
                id = user.Id,
                NameUser = user.NameUser,
                SurnameUser = user.SurnameUser,
                EmpNum = user.EmpNum ?? default(int),
                UserEmail = user.UserEmail,
                UserRole = user.UserRole,
                JobRole = userjobinfo.JobRole, 
                Department = userjobinfo.Department,
            };

            _httpContext.Response.Cookies.Append("AuthToken", AuthInfoToToken(authInfo));
            return true;
        }

        public void SignOut()
        {
            _httpContext.Response.Cookies.Delete("AuthToken");
        }

        private string AuthInfoToToken(AuthInfo authInfo)
        {
            var serializedAuthInfo = JsonConvert.SerializeObject(authInfo);
            var key = Encoding.UTF8.GetBytes(_authConfiguration.AuthEncryptionKey);
            var iv = Aes.Create().IV;
            var ivBase64 = Convert.ToBase64String(iv);
            var encBytes = _cryptography.EncryptStringToBytes_Aes(serializedAuthInfo, key, iv);
            var result = $"{ivBase64.Length.ToString().PadLeft(3, '0')}{ivBase64}{Convert.ToBase64String(encBytes)}";
            return result;
        }

        private AuthInfo AuthInfoFromToken(string token)
        {
            string decryptedToken;
            var ivLength = Convert.ToInt32(token.Substring(0, 3));
            var ivBase64 = token.Substring(3, ivLength);
            var iv = Convert.FromBase64String(ivBase64);
            var encBase64 = token.Substring(ivLength + 3);
            var encBytes = Convert.FromBase64String(encBase64);
            var key = Encoding.UTF8.GetBytes(_authConfiguration.AuthEncryptionKey);
            decryptedToken = _cryptography.DecryptStringFromBytes_Aes(encBytes, key, iv);
            var result = JsonConvert.DeserializeObject<AuthInfo>(decryptedToken);
            return result;
        }


    }
}
