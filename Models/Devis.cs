using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique2.Models
{
    public class Devis
    {
        [Key]
        public int id { get; set; }
        public double Total { get; set; }
        public string reparation { get; set; }
        public string typeIntervention { get; set; }
        public string description { get; set; }
        public bool estvalidee { get; set; }
        public List<Piece> piece = new List<Piece>();
       

       
    }
}
