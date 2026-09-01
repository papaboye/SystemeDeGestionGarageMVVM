using System.Globalization;
using System.Windows;
using TravailPratique2.Models;

namespace TravailPratique2.View;

public partial class ModifierVoiture : Window
{
    private string? _vinRecherche;

    public ModifierVoiture()
    {
        InitializeComponent();
    }

    private void RechercherVoiture_Click(object sender, RoutedEventArgs e)
    {
        var vin = txtvin.Text.Trim().ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(vin))
        {
            MessageBox.Show("Saisissez le VIN de la voiture à modifier.");
            return;
        }

        using var db = new AppDbContext();
        var voiture = db.Voitures.FirstOrDefault(item => item.vin == vin);
        if (voiture is null)
        {
            _vinRecherche = null;
            MessageBox.Show("Voiture introuvable.");
            return;
        }

        _vinRecherche = voiture.vin;
        txtvin.Text = voiture.vin;
        txtvin.IsReadOnly = true;
        txtmarque.Text = voiture.marque;
        txtmodele.Text = voiture.modele;
        txtannee.Text = voiture.annee.ToString(CultureInfo.CurrentCulture);
        txtcategorie.Text = voiture.categorie;
        txtprix.Text = voiture.prixAproximatif.ToString(CultureInfo.CurrentCulture);
        txttc.Text = voiture.typeCarburant;
        txtkilometrage.Text = voiture.kilometrage.ToString(CultureInfo.CurrentCulture);
        txtcouleur.Text = voiture.couleur;
        txttransmission.Text = voiture.transmission;
        txtproprietaire.Text = voiture.proprietaireActuel;
        txtetat.Text = voiture.etatGeneral;
        txtdatea.Text = voiture.dateAchat.ToShortDateString();
        txtdater.Text = voiture.derniereRevision.ToShortDateString();
        txtgarantie.Text = voiture.garantitRestant;
        txtassurance.Text = voiture.assurance;
    }

    private void ConfirmerModification_Click(object sender, RoutedEventArgs e)
    {
        if (_vinRecherche is null)
        {
            MessageBox.Show("Recherchez d’abord une voiture à modifier.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtmarque.Text) || string.IsNullOrWhiteSpace(txtmodele.Text))
        {
            MessageBox.Show("La marque et le modèle sont obligatoires.");
            return;
        }

        if (!int.TryParse(txtannee.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out var annee) ||
            annee < 1886 || annee > DateTime.Today.Year + 1 ||
            !int.TryParse(txtprix.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out var prix) || prix < 0 ||
            !double.TryParse(txtkilometrage.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var kilometrage) || kilometrage < 0 ||
            !DateTime.TryParse(txtdatea.Text, CultureInfo.CurrentCulture, DateTimeStyles.None, out var dateAchat) ||
            !DateTime.TryParse(txtdater.Text, CultureInfo.CurrentCulture, DateTimeStyles.None, out var derniereRevision))
        {
            MessageBox.Show("Vérifiez l’année, le prix, le kilométrage et les deux dates.");
            return;
        }

        try
        {
            using var db = new AppDbContext();
            var voiture = db.Voitures.FirstOrDefault(item => item.vin == _vinRecherche);
            if (voiture is null)
            {
                MessageBox.Show("La voiture n’existe plus dans la base de données.");
                return;
            }

            voiture.marque = txtmarque.Text.Trim();
            voiture.modele = txtmodele.Text.Trim();
            voiture.annee = annee;
            voiture.categorie = txtcategorie.Text.Trim();
            voiture.prixAproximatif = prix;
            voiture.typeCarburant = txttc.Text.Trim();
            voiture.kilometrage = kilometrage;
            voiture.couleur = txtcouleur.Text.Trim();
            voiture.transmission = txttransmission.Text.Trim();
            voiture.proprietaireActuel = txtproprietaire.Text.Trim();
            voiture.etatGeneral = txtetat.Text.Trim();
            voiture.dateAchat = dateAchat;
            voiture.derniereRevision = derniereRevision;
            voiture.garantitRestant = txtgarantie.Text.Trim();
            voiture.assurance = txtassurance.Text.Trim();

            db.SaveChanges();
            MessageBox.Show("Voiture modifiée avec succès.");
            DialogResult = true;
        }
        catch (Exception exception)
        {
            MessageBox.Show($"Impossible de modifier la voiture : {exception.Message}");
        }
    }
}
