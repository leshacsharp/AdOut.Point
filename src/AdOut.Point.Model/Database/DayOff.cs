using AdOut.Extensions.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdOut.Point.Model.Database
{
    [Table("DaysOff")]
    public class DayOff : PersistentEntity
    {
        [Key]
        public string Id { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public virtual ICollection<AdPointDayOff> AdPointsDaysOff { get; set; }
    }
}
