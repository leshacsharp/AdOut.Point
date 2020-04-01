using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdOut.Point.Model.Database
{
    [Table("Images")]
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        public DateTime AddedDateTime { get; set; } 

        [ForeignKey(nameof(AdPoint))]
        public int AdPointId { get; set; }

        [Required]
        public virtual AdPoint AdPoint { get; set; }
    }
}
