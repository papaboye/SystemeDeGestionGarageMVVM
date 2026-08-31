using System.Globalization;
using TravailPratique2.Models;

namespace TravailPratique2.Services;

internal sealed record VehicleInput(
    string Marque,
    string Modele,
    string Annee,
    string Categorie,
    string Prix,
    string Kilometrage,
    string Couleur,
    string TypeCarburant,
    string Transmission,
    string EtatGeneral,
    string Vin,
    string Proprietaire,
    string DateAchat,
    string DerniereRevision,
    string Garantie,
    string Assurance);

internal static class InventoryService
{
    public static bool TryAddVehicle(
        VehicleInput input,
        out Voiture? voiture,
        out string validationMessage)
    {
        voiture = null;
        validationMessage = string.Empty;

        var marque = input.Marque.Trim();
        var modele = input.Modele.Trim();
        var vin = input.Vin.Trim().ToUpperInvariant();

        if (string.IsNullOrWhiteSpace(marque) ||
            string.IsNullOrWhiteSpace(modele) ||
            string.IsNullOrWhiteSpace(vin))
        {
            validationMessage = "La marque, le modèle et le VIN sont obligatoires.";
            return false;
        }

        if (!int.TryParse(input.Annee, NumberStyles.Integer, CultureInfo.CurrentCulture, out var annee) ||
            annee < 1886 || annee > DateTime.Today.Year + 1)
        {
            validationMessage = $"L’année doit être comprise entre 1886 et {DateTime.Today.Year + 1}.";
            return false;
        }

        if (!int.TryParse(input.Prix, NumberStyles.Integer, CultureInfo.CurrentCulture, out var prix) || prix < 0)
        {
            validationMessage = "Le prix doit être un nombre entier positif.";
            return false;
        }

        if (!double.TryParse(input.Kilometrage, NumberStyles.Number, CultureInfo.CurrentCulture, out var kilometrage) ||
            kilometrage < 0)
        {
            validationMessage = "Le kilométrage doit être un nombre positif.";
            return false;
        }

        if (!DateTime.TryParse(input.DateAchat, CultureInfo.CurrentCulture, DateTimeStyles.None, out var dateAchat) ||
            !DateTime.TryParse(input.DerniereRevision, CultureInfo.CurrentCulture, DateTimeStyles.None, out var derniereRevision))
        {
            validationMessage = "Les dates d’achat et de révision doivent être valides.";
            return false;
        }

        using var db = new AppDbContext();
        if (db.Voitures.Any(item => item.vin == vin))
        {
            validationMessage = $"Une voiture portant le VIN {vin} existe déjà.";
            return false;
        }

        voiture = new Voiture
        {
            marque = marque,
            modele = modele,
            annee = annee,
            categorie = input.Categorie.Trim(),
            prixAproximatif = prix,
            kilometrage = kilometrage,
            couleur = input.Couleur.Trim(),
            typeCarburant = input.TypeCarburant.Trim(),
            transmission = input.Transmission.Trim(),
            etatGeneral = input.EtatGeneral.Trim(),
            vin = vin,
            proprietaireActuel = input.Proprietaire.Trim(),
            dateAchat = dateAchat,
            derniereRevision = derniereRevision,
            garantitRestant = input.Garantie.Trim(),
            assurance = input.Assurance.Trim()
        };

        db.Voitures.Add(voiture);
        db.SaveChanges();
        return true;
    }

    public static bool TryAddPiece(
        string nom,
        string prixSaisi,
        out Piece? piece,
        out string validationMessage)
    {
        piece = null;
        validationMessage = string.Empty;
        var nomNormalise = nom.Trim();

        if (string.IsNullOrWhiteSpace(nomNormalise))
        {
            validationMessage = "Le nom de la pièce est obligatoire.";
            return false;
        }

        if (!double.TryParse(prixSaisi, NumberStyles.Number, CultureInfo.CurrentCulture, out var prix) || prix < 0)
        {
            validationMessage = "Le prix doit être un nombre positif.";
            return false;
        }

        using var db = new AppDbContext();
        if (db.Pieces.Any(item => item.nom_de_piece == nomNormalise))
        {
            validationMessage = $"La pièce « {nomNormalise} » existe déjà.";
            return false;
        }

        piece = new Piece
        {
            nom_de_piece = nomNormalise,
            prix_approx = prix
        };

        db.Pieces.Add(piece);
        db.SaveChanges();
        return true;
    }
}
