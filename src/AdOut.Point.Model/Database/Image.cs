using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdOut.Point.Model.Database
{
    [Table("Images")]
    public class Image
    {
        public Image()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Path { get; set; }

        public DateTime AddedDateTime { get; set; } 

        [ForeignKey(nameof(AdPoint))]
        public string AdPointId { get; set; }

        [Required]
        public virtual AdPoint AdPoint { get; set; }
    }
}
