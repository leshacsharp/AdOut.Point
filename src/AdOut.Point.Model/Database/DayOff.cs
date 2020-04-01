using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdOut.Point.Model.Database
{
    [Table("DaysOff")]
    public class DayOff
    {
        [Key]
        public int Id { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public virtual ICollection<AdPointDayOff> AdPointsDaysOff { get; set; }
    }
}
