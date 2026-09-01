using System.Windows;
using TravailPratique2.Services;
using TravailPratique2.ViewModels;

namespace TravailPratique2.View;

public partial class Ajoutpiece : Window
{
    private readonly ProprietaireVM _viewModel;

    public Ajoutpiece(ProprietaireVM viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
    }

    private void AjouterPiece_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!InventoryService.TryAddPiece(txtnp.Text, txtpp.Text, out var piece, out var message))
            {
                MessageBox.Show(message, "Données invalides", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _viewModel.AjouterPiece(piece!);
            MessageBox.Show("Pièce ajoutée avec succès.", "Ajout terminé");
            DialogResult = true;
        }
        catch (Exception exception)
        {
            MessageBox.Show(
                $"Impossible d’ajouter la pièce : {exception.Message}",
                "Erreur",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
