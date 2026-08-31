using System.Windows;
using TravailPratique2.Models;
using TravailPratique2.ViewModels;

namespace TravailPratique2.View;

public partial class ModifierUtilisateur : Window
{
    private readonly int _utilisateurId;
    private readonly ProprietaireVM _viewModel;

    public ModifierUtilisateur(Utilisateur utilisateur, ProprietaireVM viewModel)
    {
        InitializeComponent();
        _utilisateurId = utilisateur.id;
        _viewModel = viewModel;

        txtFirstName.Text = utilisateur.firstName;
        txtLastName.Text = utilisateur.lastName;
        txtEmail.Text = utilisateur.email;
        txtRole.Text = utilisateur.role;
        txtLogin.Text = utilisateur.username;
    }

    private void Enregistrer_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
            string.IsNullOrWhiteSpace(txtLastName.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(txtRole.Text) ||
            string.IsNullOrWhiteSpace(txtLogin.Text))
        {
            MessageBox.Show("Veuillez remplir tous les champs.");
            return;
        }

        using var db = new AppDbContext();
        var utilisateur = db.Utilisateurs.Find(_utilisateurId);
        if (utilisateur is null)
        {
            MessageBox.Show("Utilisateur introuvable.");
            return;
        }

        utilisateur.firstName = txtFirstName.Text.Trim();
        utilisateur.lastName = txtLastName.Text.Trim();
        utilisateur.email = txtEmail.Text.Trim();
        utilisateur.role = txtRole.Text.Trim();
        utilisateur.username = txtLogin.Text.Trim();
        db.SaveChanges();

        _viewModel.ChargerUtilisateurs();
        MessageBox.Show("Utilisateur modifié avec succès.");
        Close();
    }
}
