using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique2.Models
{
    public class Reparation
    {
        [Key]
        public int id { get; set; }
        public string categorie { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string reparation_associee { get; set; } = string.Empty;
        public double cout { get; set; }
        public List<Piece> piece = new List<Piece>();
        public List<Voiture> voiture = new List<Voiture>();
    }
}
