using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique2.Models
{
   public class Piece
    {
        [Key]
        public int id { get; set; }
        public string nom_de_piece { get; set; }
        public double prix_approx { get; set; }
    }
}
