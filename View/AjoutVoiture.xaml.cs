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
using TravailPratique2.ViewModels;

namespace TravailPratique2.View
{
    /// <summary>
    /// Interaction logic for AjoutVoiture.xaml
    /// </summary>
    public partial class AjoutVoiture : Window
    {
        private readonly ProprietaireVM _proprietaireVM;

        public AjoutVoiture(ProprietaireVM vm)
        {
            InitializeComponent();
            _proprietaireVM = vm;
        }

        private void AjouterVoiture_Click(object sender, RoutedEventArgs e)
        {
            var voiture = new Voiture
            {
                marque = txtmarque.Text,
                modele = txtmodele.Text,
                annee = int.Parse(txtannee.Text),
                categorie = txtcategorie.Text,
                prixAproximatif = int.Parse(txtprix.Text),
                typeCarburant = txttc.Text,
                kilometrage = int.Parse(txtkilometrage.Text),
                couleur = txtcouleur.Text,
                transmission = txttransmission.Text,
                vin = txtvin.Text,
                proprietaireActuel = txtproprietaire.Text,
                etatGeneral = txtetat.Text,
                dateAchat = DateTime.Parse(txtdatea.Text),
                derniereRevision = DateTime.Parse(txtdater.Text),
                garantitRestant = txtgarantie.Text,
                assurance = txtassurance.Text
            };
            using (var context = new AppDbContext())
            {
                context.Voitures.Add(voiture);
                context.SaveChanges();
            }

           
            _proprietaireVM.AjouterVoiture(voiture);

            this.Close();
        }
    }
    
}
