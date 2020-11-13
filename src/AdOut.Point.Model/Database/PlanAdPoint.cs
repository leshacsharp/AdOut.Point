using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdOut.Point.Model.Database
{
    public class PlanAdPoint
    {
        [ForeignKey(nameof(Plan))]
        public string PlanId { get; set; }

        [ForeignKey(nameof(AdPoint))]
        public string AdPointId { get; set; }

        [Required]
        public virtual Plan Plan { get; set; }

        [Required]
        public virtual AdPoint AdPoint { get; set; }
    }
}
