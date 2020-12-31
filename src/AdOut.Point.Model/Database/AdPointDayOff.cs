using AdOut.Extensions.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdOut.Point.Model.Database
{
    [Table("AdPointsDaysOff")]
    public class AdPointDayOff : PersistentEntity
    {
        [ForeignKey(nameof(AdPoint))]
        public string AdPointId { get; set; }

        [ForeignKey(nameof(DayOff))]
        public string DayOffId { get; set; }

        [Required]
        public virtual AdPoint AdPoint { get; set; }

        [Required]
        public virtual DayOff DayOff { get; set; }
    }
}
