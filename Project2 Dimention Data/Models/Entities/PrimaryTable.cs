using System;
using System.Collections.Generic;

#nullable disable

namespace Project2_Dimention_Data.Models.Entities
{
    public partial class PrimaryTable
    {
        public PrimaryTable()
        {
            EmployeePerves = new HashSet<EmployeePerf>();
            Jobdetails = new HashSet<Jobdetail>();
            Logins = new HashSet<Login>();
            ManagerRatings = new HashSet<ManagerRating>();
            Rates = new HashSet<Rate>();
            Salaries = new HashSet<Salary>();
        }

        public int EmpNumber { get; set; }
        public string MaritalStatus { get; set; }
        public int? Age { get; set; }
        public string Over18 { get; set; }
        public int? NumCompaniesWorked { get; set; }
        public int? NumWorkingYears { get; set; }
        public int? DistanceFromHome { get; set; }
        public int? Education { get; set; }
        public string EducationField { get; set; }
        public string Gender { get; set; }

        public virtual ICollection<EmployeePerf> EmployeePerves { get; set; }
        public virtual ICollection<Jobdetail> Jobdetails { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<ManagerRating> ManagerRatings { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
    }
}
