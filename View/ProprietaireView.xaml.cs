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
using TravailPratique2.ViewModels;

namespace TravailPratique2.View
{
    /// <summary>
    /// Interaction logic for Connexion.xaml
    /// </summary>
    public partial class ProprietaireView : Window
    {
        public ProprietaireView()
        {
            InitializeComponent();
            DataContext = new ProprietaireVM();
            
        }
        private void AjouterUtilisateur_Click(object sender, RoutedEventArgs e)
        {
           
            var fenetre = new AjoutUtilisateur();
            fenetre.ShowDialog();
        }
        private void ModifierUtilisateur_Click(object sender, RoutedEventArgs e)
        {

            var fenetre = new ModifierUtilisateur();
            fenetre.ShowDialog();
        }
        private void SupprimerUtilisateur_Click(object sender, RoutedEventArgs e)
        {

            var fenetre = new SupprimerUtilisateur();
            fenetre.ShowDialog();
        }
        private void AjouterVoiture_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ProprietaireVM;
            var fenetre = new AjoutVoiture(viewModel);
            fenetre.ShowDialog();
        }
        private void ModifierVoiture_Click(object sender, RoutedEventArgs e)
        {

            var fenetre = new ModifierVoiture();
            fenetre.ShowDialog();
        }
        private void SupprimerVoiture_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ProprietaireVM;
            var fenetre = new SupprimerVoiture(viewModel);
            fenetre.ShowDialog();
        }
        private void AjouterPiece_Click(object sender, RoutedEventArgs e)
        {

            var viewModel = DataContext as ProprietaireVM;
            var fenetre = new Ajoutpiece(viewModel);
            fenetre.ShowDialog();
        }
        private void ModifierPiece_Click(object sender, RoutedEventArgs e)
        {

            var fenetre = new ModifierPiece();
            fenetre.ShowDialog();
        }
        private void SupprimerPiece_Click(object sender, RoutedEventArgs e)
        {

            var viewModel = DataContext as ProprietaireVM;
            var fenetre = new SupprimerPiece(viewModel);
            fenetre.ShowDialog();
        }
    }
}
