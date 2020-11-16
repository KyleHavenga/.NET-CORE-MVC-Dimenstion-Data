using System;
using System.Collections.Generic;

#nullable disable

namespace Project2_Dimention_Data.Models.Entities
{
    public partial class Login
    {
        public int? EmpNum { get; set; }
        public string Passwordhash { get; set; }
        public string Passwordsalt { get; set; }
        public string NameUser { get; set; }
        public string SurnameUser { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public string Id { get; set; }

        public virtual PrimaryTable EmpNumNavigation { get; set; }
    }
}
