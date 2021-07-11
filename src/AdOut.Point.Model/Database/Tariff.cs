using AdOut.Extensions.Communication.Attributes;
using AdOut.Extensions.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdOut.Point.Model.Database
{
    [Replication]
    [Table("Tariffs")]
    public class Tariff : PersistentEntity
    {
        public Tariff()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public double PriceForMinute { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        [ForeignKey(nameof(AdPoint))]
        public string AdPointId { get; set; }

        [Required]
        public virtual AdPoint AdPoint { get; set; }
    }
}
