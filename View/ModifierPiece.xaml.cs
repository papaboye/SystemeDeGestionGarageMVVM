using System.Globalization;
using System.Windows;
using TravailPratique2.Models;

namespace TravailPratique2.View;

public partial class ModifierPiece : Window
{
    private int? _pieceId;

    public ModifierPiece()
    {
        InitializeComponent();
    }

    private void RechercherPiece_Click(object sender, RoutedEventArgs e)
    {
        var nom = txtpiece.Text.Trim();
        if (string.IsNullOrWhiteSpace(nom))
        {
            MessageBox.Show("Saisissez le nom de la pièce à modifier.");
            return;
        }

        using var db = new AppDbContext();
        var piece = db.Pieces.FirstOrDefault(item => item.nom_de_piece == nom);
        if (piece is null)
        {
            _pieceId = null;
            MessageBox.Show("Pièce introuvable.");
            return;
        }

        _pieceId = piece.id;
        txtpiece.Text = piece.nom_de_piece;
        txtprix.Text = piece.prix_approx.ToString(CultureInfo.CurrentCulture);
    }

    private void ConfirmerModification_Click(object sender, RoutedEventArgs e)
    {
        if (_pieceId is null)
        {
            MessageBox.Show("Recherchez d’abord une pièce à modifier.");
            return;
        }

        var nom = txtpiece.Text.Trim();
        if (string.IsNullOrWhiteSpace(nom) ||
            !double.TryParse(txtprix.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var prix) ||
            prix < 0)
        {
            MessageBox.Show("Saisissez un nom et un prix positif valides.");
            return;
        }

        try
        {
            using var db = new AppDbContext();
            var piece = db.Pieces.Find(_pieceId.Value);
            if (piece is null)
            {
                MessageBox.Show("La pièce n’existe plus dans la base de données.");
                return;
            }

            if (db.Pieces.Any(item => item.id != piece.id && item.nom_de_piece == nom))
            {
                MessageBox.Show("Une autre pièce porte déjà ce nom.");
                return;
            }

            piece.nom_de_piece = nom;
            piece.prix_approx = prix;
            db.SaveChanges();
            MessageBox.Show("Pièce modifiée avec succès.");
            DialogResult = true;
        }
        catch (Exception exception)
        {
            MessageBox.Show($"Impossible de modifier la pièce : {exception.Message}");
        }
    }
}
