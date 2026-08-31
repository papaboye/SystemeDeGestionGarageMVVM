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
        public string marque { get; set; } = string.Empty;
        public string modele { get; set; } = string.Empty;
        public int annee { get; set; }
        public string categorie { get; set; } = string.Empty;
        public int prixAproximatif { get; set; }
        public double kilometrage { get; set; }
        public string couleur { get; set; } = string.Empty;
        public string typeCarburant { get; set; } = string.Empty;
        public string transmission { get; set; } = string.Empty;
        public string etatGeneral { get; set; } = string.Empty;
        [Key]
        public string vin { get; set; } = string.Empty;
        public string proprietaireActuel { get; set; } = string.Empty;
        public DateTime dateAchat { get; set; }
        public DateTime derniereRevision { get; set; }
        public string garantitRestant { get; set; } = string.Empty;
        public string assurance { get; set; } = string.Empty;


        

        
    }
}
