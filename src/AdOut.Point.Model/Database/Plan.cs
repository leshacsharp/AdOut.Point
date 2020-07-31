using System.ComponentModel.DataAnnotations;

namespace AdOut.Point.Model.Database
{
    public class Plan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
