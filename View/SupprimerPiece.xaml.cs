using System.Windows;
using TravailPratique2.Models;
using TravailPratique2.ViewModels;

namespace TravailPratique2.View;

public partial class SupprimerPiece : Window
{
    private readonly ProprietaireVM _viewModel;
    private Piece? _pieceTrouvee;

    public SupprimerPiece(ProprietaireVM viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
    }

    private void RechercherPiece_Click(object sender, RoutedEventArgs e)
    {
        var nom = txtNomPiece.Text.Trim();
        if (string.IsNullOrWhiteSpace(nom))
        {
            MessageBox.Show("Saisissez le nom de la pièce à rechercher.");
            return;
        }

        using var db = new AppDbContext();
        _pieceTrouvee = db.Pieces.FirstOrDefault(item => item.nom_de_piece == nom);

        btnSupprimer.IsEnabled = _pieceTrouvee is not null;
        txtResultat.Text = _pieceTrouvee is null
            ? "Aucune pièce trouvée."
            : $"{_pieceTrouvee.nom_de_piece} — {_pieceTrouvee.prix_approx:C}";
    }

    private void SupprimerPiece_Click(object sender, RoutedEventArgs e)
    {
        if (_pieceTrouvee is null)
        {
            MessageBox.Show("Recherchez d’abord une pièce.");
            return;
        }

        var confirmation = MessageBox.Show(
            $"Supprimer définitivement la pièce « {_pieceTrouvee.nom_de_piece} » ?",
            "Confirmer la suppression",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (confirmation != MessageBoxResult.Yes)
        {
            return;
        }

        try
        {
            using var db = new AppDbContext();
            var piece = db.Pieces.Find(_pieceTrouvee.id);
            if (piece is not null)
            {
                db.Pieces.Remove(piece);
                db.SaveChanges();
            }

            _viewModel.ChargerPieces();
            MessageBox.Show("Pièce supprimée avec succès.");
            DialogResult = true;
        }
        catch (Exception exception)
        {
            MessageBox.Show($"Impossible de supprimer la pièce : {exception.Message}");
        }
    }
}
