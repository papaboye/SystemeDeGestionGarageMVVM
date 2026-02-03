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
using TravailPratique2.Models;
using TravailPratique2.ViewModels;

namespace TravailPratique2.View
{
    /// <summary>
    /// Interaction logic for SupprimerVoiture.xaml
    /// </summary>
    public partial class SupprimerVoiture : Window
    {
        private readonly ProprietaireVM _proprietaireVM;
        private Voiture voitureTrouvee;
        public SupprimerVoiture(ProprietaireVM vm)
        {
            InitializeComponent();
            _proprietaireVM = vm;
            DataContext = _proprietaireVM;
        }
        
        private void SupprimerVoiture_Click(object sender, RoutedEventArgs e)
        {
            string nomRecherche = txtvin.Text.Trim();

            if (string.IsNullOrEmpty(nomRecherche))
            {
                MessageBox.Show("Veuillez entrer le nom de la voiture à supprimer.");
                return;
            }

        }
        private void RechercherVoiture_Click(object sender, RoutedEventArgs e)
        {
            string nomRecherche = txtvin.Text.Trim();

            if (string.IsNullOrWhiteSpace(nomRecherche))
            {
                MessageBox.Show("Veuillez entrer un nom de pièce.");
                return;
            }

            using (var db = new AppDbContext())
            {
                voitureTrouvee = db.Voitures.FirstOrDefault(p => p.vin == nomRecherche);
            }
        }
       
    }
}
