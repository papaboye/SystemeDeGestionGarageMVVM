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
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientView : Window
    {
        public ClientView()
        {
            InitializeComponent();
            DataContext = new ClientVM();
        }
        private void Acheter_Click(object sender, RoutedEventArgs e)
        {
            //mettre fenetre pour acheter bvoiture
            var fenetre = new AcheterVoiture();//AcheterVoiture
            fenetre.ShowDialog();
        }
        private void AcheterPiece_Click(object sender, RoutedEventArgs e)
        {
            //mettre fenetre pour acheter bvoiture
            var fenetre = new AjoutUtilisateur();
            fenetre.ShowDialog();
        }
        private void DemanderReparation_Click(object sender, RoutedEventArgs e)
        {
            
            var fenetre = new ReparationView();
            fenetre.ShowDialog();
        }
    }
}
