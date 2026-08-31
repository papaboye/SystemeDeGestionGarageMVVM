using System.Windows;
using TravailPratique2.Services;
using TravailPratique2.ViewModels;

namespace TravailPratique2.View;

public partial class ajoutvoituref : Window
{
    private readonly FournisseurVM _viewModel;

    public ajoutvoituref(FournisseurVM viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
    }

    private void AjouterVoiture_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!InventoryService.TryAddVehicle(CreateInput(), out var voiture, out var message))
            {
                MessageBox.Show(message, "Données invalides", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _viewModel.AjouterVoiture(voiture!);
            MessageBox.Show("Voiture ajoutée avec succès.", "Ajout terminé");
            DialogResult = true;
        }
        catch (Exception exception)
        {
            MessageBox.Show(
                $"Impossible d’ajouter la voiture : {exception.Message}",
                "Erreur",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private VehicleInput CreateInput() => new(
        txtmarque.Text,
        txtmodele.Text,
        txtannee.Text,
        txtcategorie.Text,
        txtprix.Text,
        txtkilometrage.Text,
        txtcouleur.Text,
        txttc.Text,
        txttransmission.Text,
        txtetat.Text,
        txtvin.Text,
        txtproprietaire.Text,
        txtdatea.Text,
        txtdater.Text,
        txtgarantie.Text,
        txtassurance.Text);
}
