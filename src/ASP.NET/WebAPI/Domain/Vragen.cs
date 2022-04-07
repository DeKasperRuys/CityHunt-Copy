using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Vragen
    {
        [Key]
        public int VraagId { get; set; }
        public string Title { get; set; }
        public string JuisteAntwoord { get; set; }
        public string Antwoord1 { get; set; }
        public string Antwoord2 { get; set; }
        public string Antwoord3 { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Hint { get; set; }
        public int Points { get; set; }
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
    }
}
