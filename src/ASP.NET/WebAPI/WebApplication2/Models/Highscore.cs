using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApplication2.Models
{
    public class Highscore
    {
        [Key]
        public int ScoreId { get; set; }
        public int TotalPoints { get; set; }
        public int CurrentPoints { get; set; }

    }
}
