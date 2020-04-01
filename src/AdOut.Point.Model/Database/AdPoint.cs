﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdOut.Point.Model.Database
{
    [Table("AdPoints")]
    public class AdPoint
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Location { get; set; }

        [Required]
        public string IpAdress { get; set; }

        public TimeSpan StartWorkingTime { get; set; }

        public TimeSpan EndWorkingTime { get; set; }

        public int ScreenWidthCm { get; set; }

        public int ScreenHeightCm { get; set; }

        public virtual ICollection<Tariff> Tariffs { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<AdPointDayOff> AdPointsDaysOff { get; set; }

        public virtual ICollection<ComputerState> ComputerStates { get; set; }    
    }
}
