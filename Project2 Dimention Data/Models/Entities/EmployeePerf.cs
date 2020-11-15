using System;
using System.Collections.Generic;

#nullable disable

namespace Project2_Dimention_Data.Models.Entities
{
    public partial class EmployeePerf
    {
        public int? EmpNumber { get; set; }
        public int? EnvironmentSat { get; set; }
        public int? JobSat { get; set; }
        public int? RelationshipSat { get; set; }
        public int? WorkLifeBalance { get; set; }

        public virtual PrimaryTable EmpNumberNavigation { get; set; }
    }
}
