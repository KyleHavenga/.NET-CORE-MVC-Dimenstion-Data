using Project2_Dimention_Data.Models.Entities;
using System;
using System.Collections.Generic;

namespace Project2_Dimention_Data.Services
{
    public class AuthenticationInfo
    {
        public string id { get; set; }
        public int EmpNum { get; set; }
        public string NameUser { get; set; }
        public string SurnameUser { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public string JobRole { get; set; }
        public string Department { get; set; }
        public bool IsAuthenticated => (EmpNum > 0);
    }
}

// This class created a list structure for the information of a logged in user so that access can be restricted.