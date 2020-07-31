using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdOut.Point.Model.Database
{
    public class PlanAdPoint
    {
        [ForeignKey(nameof(Plan))]
        public int PlanId { get; set; }

        [ForeignKey(nameof(AdPoint))]
        public int AdPointId { get; set; }

        [Required]
        public virtual Plan Plan { get; set; }

        [Required]
        public virtual AdPoint AdPoint { get; set; }
    }
}
