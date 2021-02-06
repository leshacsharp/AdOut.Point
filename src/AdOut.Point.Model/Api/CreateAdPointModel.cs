using System;
using System.ComponentModel.DataAnnotations;

namespace AdOut.Point.Model.Api
{
    public class CreateAdPointModel
    {
        [Required]
        [MaxLength(50)]
        public string Location { get; set; }

        public TimeSpan StartWorkingTime { get; set; }

        public TimeSpan EndWorkingTime { get; set; }

        public int ScreenWidthCm { get; set; }

        public int ScreenHeightCm { get; set; }
    }
}
