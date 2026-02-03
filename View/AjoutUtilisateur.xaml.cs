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
    /// Interaction logic for AjoutUtilisateur.xaml
    /// </summary>
    public partial class AjoutUtilisateur : Window
    {
        public AjoutUtilisateur()
        {
            InitializeComponent();
        }
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtFirstName.Text) ||
                string.IsNullOrEmpty(txtRole.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }
            var utilisateur = new Utilisateur
            {
                lastName = txtLastName.Text,
                firstName = txtFirstName.Text,
                role = txtRole.Text,
                email = txtemail.Text,
                password= txtpwd.Text,
                username= txtlogin.Text
            };

            try
            {
               
                using (var db = new AppDbContext())
                {
                    db.Utilisateurs.Add(utilisateur);  
                    db.SaveChanges();  
                }

                
                MessageBox.Show("Utilisateur ajouté avec succès !");
                this.Close();  
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Erreur lors de l'ajout de l'utilisateur: {ex.Message}");
            }
        }
    }
}

