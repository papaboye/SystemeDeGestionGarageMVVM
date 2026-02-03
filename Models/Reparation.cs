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
        public string categorie { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string reparation_associee { get; set; }
        public double cout { get; set; }
        public List<Piece> piece = new List<Piece>();
        public List<Voiture> voiture = new List<Voiture>();
    }
}
