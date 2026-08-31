using System.Windows;
using System.Windows.Controls;
using TravailPratique2.Models;

namespace TravailPratique2.View;

public partial class AjoutUtilisateur : Window
{
    public AjoutUtilisateur() => InitializeComponent();

    private void Ajouter_Click(object sender, RoutedEventArgs e)
    {
        var role = (cmbRole.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(txtLastName.Text) ||
            string.IsNullOrWhiteSpace(txtFirstName.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(txtLogin.Text) ||
            string.IsNullOrWhiteSpace(txtPassword.Password) ||
            string.IsNullOrWhiteSpace(role))
        {
            MessageBox.Show("Veuillez remplir tous les champs.");
            return;
        }

        using var db = new AppDbContext();
        var login = txtLogin.Text.Trim();
        if (db.Utilisateurs.Any(utilisateur => utilisateur.username == login))
        {
            MessageBox.Show("Ce nom d'utilisateur existe déjà.");
            return;
        }

        db.Utilisateurs.Add(new Utilisateur
        {
            lastName = txtLastName.Text.Trim(),
            firstName = txtFirstName.Text.Trim(),
            role = role,
            email = txtEmail.Text.Trim(),
            password = txtPassword.Password,
            username = login
        });
        db.SaveChanges();

        MessageBox.Show("Utilisateur ajouté avec succès.");
        Close();
    }
}
