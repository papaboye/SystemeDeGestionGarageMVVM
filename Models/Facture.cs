using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique2.Models
{
    class Facture
    {
        [Key]
        public int id { get; set; }
        public double montantTotal { get; set; }
        public string modePaiement { get; set; } = string.Empty;
        public double totalpieces { get; set; }
        public double coutMain { get; set; }
        public bool statut { get; set; }
        public List<Piece> p = new List<Piece>();
        public Reparation reparationAssociee { get; set; } = null!;
    }
}
