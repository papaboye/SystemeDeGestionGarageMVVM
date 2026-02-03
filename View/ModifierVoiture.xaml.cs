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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TravailPratique2.Models;

namespace TravailPratique2.View
{
    /// <summary>
    /// Interaction logic for ModifierVoiture.xaml
    /// </summary>
    public partial class ModifierVoiture : Window
    {
        private DbContextOptionsBuilder _context;
        private Voiture voitureActuelle;
        public ModifierVoiture()
        {
            InitializeComponent();

            _context = new DbContextOptionsBuilder();

        }
        private void RechercherVoiture_Click(object sender, RoutedEventArgs e)
        {
            string vin = txtvin.Text;

            using (var db = new AppDbContext())
            {
                var voiture = db.Voitures.FirstOrDefault(v => v.vin == vin);
                if (voiture != null)
                {
                    
                    txtmarque.Text = voiture.marque;
                    txtmodele.Text = voiture.modele;
                    txtannee.Text = voiture.annee.ToString();
                    txtcategorie.Text = voiture.categorie;
                    txtprix.Text = voiture.prixAproximatif.ToString();
                    txttc.Text = voiture.typeCarburant;
                    txtkilometrage.Text = voiture.kilometrage.ToString();
                    txtcouleur.Text = voiture.couleur;
                    txttransmission.Text = voiture.transmission;
                    txtproprietaire.Text = voiture.proprietaireActuel;
                    txtetat.Text = voiture.etatGeneral;
                    txtdatea.Text = voiture.dateAchat.ToString("yyyy-MM-dd");
                    txtdater.Text = voiture.derniereRevision.ToString("yyyy-MM-dd");
                    txtgarantie.Text = voiture.garantitRestant;
                    txtassurance.Text = voiture.assurance;
                }
                else
                {
                    MessageBox.Show("Voiture introuvable.");
                }
            }
        }

        private void ConfirmerModification_Click(object sender, RoutedEventArgs e)
        {
            string vin = txtvin.Text;

            using (var db = new AppDbContext())
            {
                var voiture = db.Voitures.FirstOrDefault(v => v.vin == vin);
                if (voiture != null)
                {
                    
                    voiture.marque = txtmarque.Text;
                    voiture.modele = txtmodele.Text;
                    voiture.annee = int.Parse(txtannee.Text);
                    voiture.categorie = txtcategorie.Text;
                    voiture.prixAproximatif = int.Parse(txtprix.Text);
                    voiture.typeCarburant = txttc.Text;
                    voiture.kilometrage = int.Parse(txtkilometrage.Text);
                    voiture.couleur = txtcouleur.Text;
                    voiture.transmission = txttransmission.Text;
                    voiture.proprietaireActuel = txtproprietaire.Text;
                    voiture.etatGeneral = txtetat.Text;
                    voiture.dateAchat = DateTime.Parse(txtdatea.Text);
                    voiture.derniereRevision = DateTime.Parse(txtdater.Text);
                    voiture.garantitRestant = txtgarantie.Text;
                    voiture.assurance = txtassurance.Text;

                    db.SaveChanges();
                    MessageBox.Show("Voiture modifiée avec succès.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Voiture non trouvée pour mise à jour.");
                }
            }
        }
    }
}


