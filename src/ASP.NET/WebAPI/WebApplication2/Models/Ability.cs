using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Ability
    {
        [Key]
        public int AbilityId { get; set; }
        public int AbilityType { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool isUsed { get; set; }
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
    }
}
