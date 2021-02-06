using AdOut.Extensions.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdOut.Point.Model.Database
{
    public class Plan : PersistentEntity
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<PlanAdPoint> PlanAdPoints { get; set; }
    }
}
