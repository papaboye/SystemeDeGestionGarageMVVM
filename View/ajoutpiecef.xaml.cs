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
    /// Interaction logic for ajoutpiecef.xaml
    /// </summary>
    public partial class ajoutpiecef : Window
    {
        private readonly FournisseurVM _fournisseurVM;
        public ajoutpiecef(FournisseurVM vm)
        {
            InitializeComponent();
            _fournisseurVM = vm;
        }
        
        
        private void AjouterPiece_Click(object sender, RoutedEventArgs e)
        {
            var piece = new Piece
            {
                nom_de_piece = txtnp.Text,

                prix_approx = int.Parse(txtpp.Text),
            };
            using (var context = new AppDbContext())
            {
                context.Pieces.Add(piece);
                context.SaveChanges();
            }


            _fournisseurVM.AjouterPiece(piece);

            this.Close();
        }
    }
}
