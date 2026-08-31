using System.Windows;
using TravailPratique2.ViewModels;

namespace TravailPratique2.View;

public partial class ProprietaireView : Window
{
    public ProprietaireView()
    {
        InitializeComponent();
        DataContext = new ProprietaireVM();
    }

    private ProprietaireVM ViewModel => (ProprietaireVM)DataContext;

    private void AjouterUtilisateur_Click(object sender, RoutedEventArgs e)
    {
        new AjoutUtilisateur { Owner = this }.ShowDialog();
        ViewModel.ChargerUtilisateurs();
    }

    private void ModifierUtilisateur_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.UtilisateurSelectionne is null)
        {
            MessageBox.Show("Sélectionnez un utilisateur à modifier.");
            return;
        }

        new ModifierUtilisateur(ViewModel.UtilisateurSelectionne, ViewModel) { Owner = this }.ShowDialog();
    }

    private void SupprimerUtilisateur_Click(object sender, RoutedEventArgs e)
    {
        var utilisateur = ViewModel.UtilisateurSelectionne;
        if (utilisateur is null)
        {
            MessageBox.Show("Sélectionnez un utilisateur à supprimer.");
            return;
        }

        var confirmation = MessageBox.Show(
            $"Supprimer l'utilisateur {utilisateur.firstName} {utilisateur.lastName} ?",
            "Confirmer la suppression",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (confirmation == MessageBoxResult.Yes)
        {
            ViewModel.SupprimerUtilisateur(utilisateur.id);
        }
    }

    private void AjouterVoiture_Click(object sender, RoutedEventArgs e) =>
        new AjoutVoiture(ViewModel) { Owner = this }.ShowDialog();

    private void ModifierVoiture_Click(object sender, RoutedEventArgs e)
    {
        new ModifierVoiture { Owner = this }.ShowDialog();
        ViewModel.ChargerVoitures();
    }

    private void SupprimerVoiture_Click(object sender, RoutedEventArgs e) =>
        new SupprimerVoiture(ViewModel) { Owner = this }.ShowDialog();

    private void AjouterPiece_Click(object sender, RoutedEventArgs e) =>
        new Ajoutpiece(ViewModel) { Owner = this }.ShowDialog();

    private void ModifierPiece_Click(object sender, RoutedEventArgs e)
    {
        new ModifierPiece { Owner = this }.ShowDialog();
        ViewModel.ChargerPieces();
    }

    private void SupprimerPiece_Click(object sender, RoutedEventArgs e) =>
        new SupprimerPiece(ViewModel) { Owner = this }.ShowDialog();
}
