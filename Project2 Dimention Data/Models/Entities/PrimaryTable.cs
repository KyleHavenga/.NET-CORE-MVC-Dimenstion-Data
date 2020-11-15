using System;
using System.Collections.Generic;

#nullable disable

namespace Project2_Dimention_Data.Models.Entities
{
    public partial class PrimaryTable
    {
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
    }
}
