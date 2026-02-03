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
    /// Interaction logic for FournisseurView.xaml
    /// </summary>
    public partial class FournisseurView : Window
    {
        private FournisseurVM _viewModel;
        public FournisseurView()
        {
            InitializeComponent();
            DataContext = new ViewModels.FournisseurVM();
        }
        private void AjouterVoiture_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ProprietaireVM;
            var fenetre = new AjoutVoiture(viewModel);
            fenetre.ShowDialog();
        }
        private void AjouterPiece_Click(object sender, RoutedEventArgs e)
        {

            var viewModel = DataContext as ProprietaireVM;
            var fenetre = new Ajoutpiece(viewModel);
            fenetre.ShowDialog();
        }
    }
}
