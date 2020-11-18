using System;
using System.Collections.Generic;

#nullable disable

namespace Project2_Dimention_Data.Models.Entities
{
    public partial class Jobdetail
    {
        public int? EmpNumber { get; set; }
        public string Attrition { get; set; }
        public string BusinessTravel { get; set; }
        public string Department { get; set; }
        public int? EmployeeCount { get; set; }
        public int? JobInvolvement { get; set; }
        public int? JobLevel { get; set; }
        public string JobRole { get; set; }
        public string Overtime { get; set; }
        public int? StandardHours { get; set; }
        public int? StockOptionLevel { get; set; }
        public int? YearsLastPromotion { get; set; }
        public int? YearsCurrentRole { get; set; }
        public int? YearsCurrentManager { get; set; }
        public int? YearsAtCompany { get; set; }
        public int JobDetailsId { get; set; }

        public virtual PrimaryTable EmpNumberNavigation { get; set; }
    }
}
