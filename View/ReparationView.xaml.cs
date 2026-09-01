using System.Windows;
using Microsoft.EntityFrameworkCore;
using TravailPratique2.Models;

namespace TravailPratique2.View;

public partial class ReparationView : Window
{
    public ReparationView()
    {
        InitializeComponent();
        ChargerDonnees();
    }

    private void ChargerDonnees()
    {
        using var db = new AppDbContext();
        cmbVoitures.ItemsSource = db.Voitures.AsNoTracking().ToList();
        cmbPieces.ItemsSource = db.Pieces.AsNoTracking().ToList();
    }

    private void Soumettre_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtCategorie.Text) ||
            string.IsNullOrWhiteSpace(txtType.Text) ||
            string.IsNullOrWhiteSpace(txtDescription.Text))
        {
            MessageBox.Show("Veuillez décrire la réparation demandée.");
            return;
        }

        if (cmbVoitures.SelectedItem is not Voiture voitureSelectionnee)
        {
            MessageBox.Show("Veuillez sélectionner une voiture.");
            return;
        }

        if (cmbPieces.SelectedItem is not Piece pieceSelectionnee)
        {
            MessageBox.Show("Veuillez sélectionner une pièce.");
            return;
        }

        const double coutMainDoeuvre = 200;
        var totalPieces = pieceSelectionnee.prix_approx;
        var montantTotal = totalPieces + coutMainDoeuvre;

        var reparation = new Reparation
        {
            categorie = txtCategorie.Text.Trim(),
            type = txtType.Text.Trim(),
            description = txtDescription.Text.Trim(),
            reparation_associee = txtReparationAssociee.Text.Trim(),
            voiture = [voitureSelectionnee],
            piece = [pieceSelectionnee],
            cout = montantTotal
        };

        var devis = new Devis
        {
            reparation = reparation.type,
            typeIntervention = reparation.categorie,
            description = reparation.description,
            Total = montantTotal,
            estvalidee = false,
            piece = [pieceSelectionnee]
        };

        var fenetreDevis = new DevisView(devis);
        fenetreDevis.ShowDialog();

        using var db = new AppDbContext();
        db.Reparations.Add(reparation);
        db.Devis.Add(devis);

        if (fenetreDevis.estvalide)
        {
            devis.estvalidee = true;
            db.Factures.Add(new Facture
            {
                montantTotal = montantTotal,
                totalpieces = totalPieces,
                coutMain = coutMainDoeuvre,
                modePaiement = "Espèces",
                statut = true,
                p = [pieceSelectionnee],
                reparationAssociee = reparation
            });
        }

        db.SaveChanges();

        MessageBox.Show(
            fenetreDevis.estvalide
                ? "Devis validé et facture générée."
                : "Devis sauvegardé pour plus tard.");
        Close();
    }
}
