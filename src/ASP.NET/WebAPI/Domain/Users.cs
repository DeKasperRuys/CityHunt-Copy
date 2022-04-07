using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Naam { get; set; }
        public string Achternaam { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Passwoord { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public bool isGedetineerde { get; set; }

        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }

        public int ScoreId { get; set; }


    }
}
