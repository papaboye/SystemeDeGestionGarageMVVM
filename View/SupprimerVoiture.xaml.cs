using System.Windows;
using TravailPratique2.Models;
using TravailPratique2.ViewModels;

namespace TravailPratique2.View;

public partial class SupprimerVoiture : Window
{
    private readonly ProprietaireVM _viewModel;
    private Voiture? _voitureTrouvee;

    public SupprimerVoiture(ProprietaireVM viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
    }

    private void RechercherVoiture_Click(object sender, RoutedEventArgs e)
    {
        var vin = txtvin.Text.Trim().ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(vin))
        {
            MessageBox.Show("Saisissez le VIN de la voiture à rechercher.");
            return;
        }

        using var db = new AppDbContext();
        _voitureTrouvee = db.Voitures.FirstOrDefault(item => item.vin == vin);

        btnSupprimer.IsEnabled = _voitureTrouvee is not null;
        txtResultat.Text = _voitureTrouvee is null
            ? "Aucune voiture trouvée."
            : $"{_voitureTrouvee.marque} {_voitureTrouvee.modele} ({_voitureTrouvee.annee}) — VIN {_voitureTrouvee.vin}";
    }

    private void SupprimerVoiture_Click(object sender, RoutedEventArgs e)
    {
        if (_voitureTrouvee is null)
        {
            MessageBox.Show("Recherchez d’abord une voiture.");
            return;
        }

        var confirmation = MessageBox.Show(
            $"Supprimer définitivement {_voitureTrouvee.marque} {_voitureTrouvee.modele} ?",
            "Confirmer la suppression",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (confirmation != MessageBoxResult.Yes)
        {
            return;
        }

        _viewModel.SupprimerVoiture(_voitureTrouvee.vin);
        DialogResult = true;
    }
}
