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
    /// Interaction logic for ReparationView.xaml
    /// </summary>
    public partial class ReparationView : Window
    {
        Devis devis = new Devis();
        public ReparationView()
        {
            InitializeComponent();
            ChargerDonnees();
        }
        private void ChargerDonnees()
        {
            using (var db = new AppDbContext())
            {
                cmbVoitures.ItemsSource = db.Voitures.ToList();
                cmbPieces.ItemsSource = db.Pieces.ToList();
            }
        }

        private void Soumettre_Click(object sender, RoutedEventArgs e)
        {
            if (cmbVoitures.SelectedItem is not Voiture voitureSelectionnee)
            {
                MessageBox.Show("Veuillez sélectionner une voiture.");
                return;
            }
            if (cmbPieces.SelectedItem is not Piece pieceselectionne) {
                MessageBox.Show("Veuillez sélectionner une voiture.");
                return;
            }

            double coutMain = 200; 
            double totalPieces = pieceselectionne.prix_approx;
            double montantTotal = totalPieces + coutMain;

            var reparation = new Reparation
            {
                categorie = txtCategorie.Text,
                type = txtType.Text,
                description = txtDescription.Text,
               

                voiture = new List<Voiture> { voitureSelectionnee },
                piece = new List<Piece> { pieceselectionne },
                cout = montantTotal

            };
            var devis = new Devis 
            {
                
                reparation=  reparation.type,
                typeIntervention =  reparation.categorie ,
                description = reparation.description,
                Total = montantTotal,
                estvalidee = false,
                piece = new List<Piece> { pieceselectionne }
            };

            var fenetre = new DevisView();
            fenetre.ShowDialog();

            using (var db = new AppDbContext())
            {
                db.Reparations.Add(reparation);

                if (fenetre.estvalide)
                {
                    devis.estvalidee = true;

                    var facture = new Facture
                    {
                        montantTotal = montantTotal,
                        totalpieces = totalPieces,
                        coutMain = coutMain,
                        modePaiement = "Espèces", 
                        statut = true,
                        p = new List<Piece> { pieceselectionne }
                        
                    };

                    db.Devis.Add(devis);
                    db.Factures.Add(facture);
                    db.SaveChanges();

                    MessageBox.Show("Devis validé et facture générée !");
                }
                else
                {
                    db.Devis.Add(devis);
                    db.SaveChanges();
                    MessageBox.Show("Devis non validé, sauvegardé pour plus tard.");
                }
            }

            Close();
        }
    }
    
}
    