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
    /// Interaction logic for DevisView.xaml
    /// </summary>
    public partial class DevisView : Window
    {
        public Devis Devis { get; private set; }
        public bool estvalide { get; private set; }
        public DevisView()
        {
            InitializeComponent();
            DataContext = new Devis();
        }
        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            estvalide = true;
            this.Close();
        }
        private void Annuler_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("L'action a été annulée.");

            this.Close();
        }
    }
}
