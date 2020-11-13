using AdOut.Point.Model.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdOut.Point.Model.Database
{
    [Table("ComputerStates")]
    public class ComputerState
    {
        public ComputerState()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public DateTime StateDateTime { get; set; }

        public ComputerStatus Status { get; set; }

        public double CPUUsage { get; set; }

        public double CPUTemperature { get; set; }

        public double RAMUsage { get; set; }

        public double DiskUsage { get; set; }

        [ForeignKey(nameof(AdPoint))]
        public string AdPointId { get; set; }

        [Required]
        public virtual AdPoint AdPoint { get; set; }
    }
}
