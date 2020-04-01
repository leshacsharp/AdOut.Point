using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdOut.Point.Model.Database
{
    [Table("AdPointsDaysOff")]
    public class AdPointDayOff
    {
        [ForeignKey(nameof(AdPoint))]
        public int AdPointId { get; set; }

        [ForeignKey(nameof(DayOff))]
        public int DayOffId { get; set; }

        [Required]
        public virtual AdPoint AdPoint { get; set; }

        [Required]
        public virtual DayOff DayOff { get; set; }
    }
}
