using System;
using System.Collections.Generic;

#nullable disable

namespace Project2_Dimention_Data.Models.Entities
{
    public partial class Salary
    {
        public int? EmpNumber { get; set; }
        public int? MonthlyIncome { get; set; }
        public int? PercentSalaryHike { get; set; }

        public virtual PrimaryTable EmpNumberNavigation { get; set; }
    }
}
