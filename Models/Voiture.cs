using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique2.Models
{
    public class Voiture 
    {
        public string marque { get; set; }
        public string modele { get; set; }
        public int annee { get; set; }
        public string categorie { get; set; }
        public int prixAproximatif { get; set; }
        public double kilometrage { get; set; }
        public string couleur { get; set; }
        public string typeCarburant { get; set; }
        public string transmission { get; set; }
        public string etatGeneral { get; set; }
        [Key]
        public string vin { get; set; }
        public string proprietaireActuel { get; set; }
        public DateTime dateAchat { get; set; }
        public DateTime derniereRevision { get; set; }
        public string garantitRestant { get; set; }
        public string assurance { get; set; }


        

        
    }
}
