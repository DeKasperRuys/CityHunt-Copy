using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string TeamNaam { get; set; }
        public ICollection<Users> Users { get; set; }
        public ICollection<Vragen> Vragen { get; set; }
        public ICollection<Ability> Ability { get; set; }
    }
}
