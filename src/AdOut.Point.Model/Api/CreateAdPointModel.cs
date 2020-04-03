using AdOut.Point.Model.Dto;
using System;
using System.Collections.Generic;
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

        //todo: crete adPoint with images
        //public virtual ICollection<Image> Images { get; set; }
    }

}
