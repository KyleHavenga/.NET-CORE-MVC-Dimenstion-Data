using System;
using System.Collections.Generic;

#nullable disable

namespace Project2_Dimention_Data.Models.Entities
{
    public partial class Rate
    {
        public int? EmpNumber { get; set; }
        public int? HourlyRate { get; set; }
        public int? MonthlyRate { get; set; }
        public int? DailyRate { get; set; }
        public int RatesId { get; set; }

        public virtual PrimaryTable EmpNumberNavigation { get; set; }
    }
}
