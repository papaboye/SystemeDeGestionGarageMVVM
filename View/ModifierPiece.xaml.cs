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
    /// Interaction logic for ModifierPiece.xaml
    /// </summary>
    public partial class ModifierPiece : Window
    {
        public ModifierPiece()
        {
            InitializeComponent();
        }
        private void RechercherPiece_Click(object sender, RoutedEventArgs e)
        {
            string nom = txtpiece.Text;

            using (var db = new AppDbContext())
            {
                var piece = db.Pieces.FirstOrDefault(p => p.nom_de_piece == nom);
                if (piece != null)
                {

                    txtpiece.Text = piece.nom_de_piece;
                    
                    txtprix.Text = piece.prix_approx.ToString();
                    
                }
                else
                {
                    MessageBox.Show("Piece introuvable.");
                }
            }
        }
        private void ConfirmerModification_Click(object sender, RoutedEventArgs e)
        {
            string nom = txtpiece.Text;

            using (var db = new AppDbContext())
            {
                var piece = db.Pieces.FirstOrDefault(v => v.nom_de_piece == nom);
                if (piece != null)
                {

                   piece.nom_de_piece = txtpiece.Text;
                    piece.prix_approx = int.Parse(txtprix.Text);
                   

                    db.SaveChanges();
                    MessageBox.Show("Piece modifiée avec succès.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Piece non trouvée pour mise à jour.");
                }
            }
        }
    }
}
