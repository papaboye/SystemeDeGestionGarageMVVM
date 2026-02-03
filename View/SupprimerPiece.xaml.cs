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
    /// Interaction logic for SupprimerPiece.xaml
    /// </summary>
    public partial class SupprimerPiece : Window
    {
        private readonly ProprietaireVM _proprietaireVM;
        private Piece pieceTrouvee;

        public SupprimerPiece(ProprietaireVM vm)
        {
            InitializeComponent();
            _proprietaireVM = vm;
            DataContext = _proprietaireVM;
        }

        private void SupprimerPiece_Click(object sender, RoutedEventArgs e)
        {
            string nomRecherche = txtNomPiece.Text.Trim();

            if (string.IsNullOrEmpty(nomRecherche))
            {
                MessageBox.Show("Veuillez entrer le nom de la pièce à supprimer.");
                return;
            }

        }
    
    private void RechercherPiece_Click(object sender, RoutedEventArgs e)
        {
            string nomRecherche = txtNomPiece.Text.Trim();

            if (string.IsNullOrWhiteSpace(nomRecherche))
            {
                MessageBox.Show("Veuillez entrer un nom de pièce.");
                return;
            }

            using (var db = new AppDbContext())
            {
                pieceTrouvee = db.Pieces.FirstOrDefault(p => p.nom_de_piece == nomRecherche);

                if (pieceTrouvee != null)
                {
                    txtResultat.Text = $"Nom : {pieceTrouvee.nom_de_piece}, Prix : {pieceTrouvee.prix_approx}";
                    btnSupprimer.IsEnabled = true;
                }
                else
                {
                    txtResultat.Text = "Aucune pièce trouvée.";
                    btnSupprimer.IsEnabled = false;
                    pieceTrouvee = null;
                }
                db.SaveChanges();
            }
        }
    }
}