using System;
using System.Collections.Generic;

#nullable disable

namespace Project2_Dimention_Data.Models.Entities
{
    public partial class ManagerRating
    {
        public int? EmpNumber { get; set; }
        public int? PerformanceRating { get; set; }
        public int? TrainingTimesLastYear { get; set; }
        public int RatingId { get; set; }

        public virtual PrimaryTable EmpNumberNavigation { get; set; }
    }
}
