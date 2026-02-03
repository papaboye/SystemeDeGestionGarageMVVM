using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravailPratique2.Models;

namespace TravailPratique2.View
{
    /// <summary>
    /// Interaction logic for AcheterVoiture.xaml
    /// </summary>
    public partial class AcheterVoiture : Window
    {
        public AcheterVoiture()
        {
            InitializeComponent();
        }
        private void Acheter_Click(object sender, RoutedEventArgs e)
        {
            string marque = txtMarque.Text.Trim();
            string modele = txtModele.Text.Trim();
            string anneeStr = txtAnnee.Text.Trim();

            if (string.IsNullOrEmpty(marque) || string.IsNullOrEmpty(modele) || string.IsNullOrEmpty(anneeStr))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            if (!int.TryParse(anneeStr, out int annee))
            {
                MessageBox.Show("L'année doit être un nombre.");
                return;
            }

            using (var db = new AppDbContext())
            {
                var voiture = db.Voitures
                    .FirstOrDefault(v => v.marque == marque && v.modele == modele && v.annee == annee);

                if (voiture == null)
                {
                    MessageBox.Show("La voiture n'existe pas en stock.");
                    return;
                }

                
                var devis = new Devis
                {
                    description = $"Achat de voiture {voiture.marque} {voiture.modele} ({voiture.annee})",
                    reparation = "Achat",
                    typeIntervention = "Vente",
                    Total = voiture.prixAproximatif, 
                    estvalidee = true
                };

                db.Devis.Add(devis);
                db.SaveChanges();

                MessageBox.Show($"Voiture trouvée ! Prix : {voiture.prixAproximatif}$. Devis généré avec succès.");
                this.Close();
            }
        }
    }
}

