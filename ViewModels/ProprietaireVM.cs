using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using TravailPratique2.Models;

namespace TravailPratique2.ViewModels;

public sealed class ProprietaireVM : INotifyPropertyChanged
{
    private ObservableCollection<Voiture> _voitures = [];
    private ObservableCollection<Utilisateur> _utilisateurs = [];
    private ObservableCollection<Piece> _pieces = [];
    private Utilisateur? _utilisateurSelectionne;
    private Piece? _pieceSelectionnee;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<Voiture> Voitures
    {
        get => _voitures;
        private set
        {
            _voitures = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Utilisateur> Utilisateurs
    {
        get => _utilisateurs;
        private set
        {
            _utilisateurs = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Piece> Pieces
    {
        get => _pieces;
        private set
        {
            _pieces = value;
            OnPropertyChanged();
        }
    }

    public Utilisateur? UtilisateurSelectionne
    {
        get => _utilisateurSelectionne;
        set
        {
            _utilisateurSelectionne = value;
            OnPropertyChanged();
        }
    }

    public Piece? PieceSelectionnee
    {
        get => _pieceSelectionnee;
        set
        {
            _pieceSelectionnee = value;
            OnPropertyChanged();
        }
    }

    public ProprietaireVM()
    {
        ChargerUtilisateurs();
        ChargerVoitures();
        ChargerPieces();
    }

    public void ChargerUtilisateurs()
    {
        using var db = new AppDbContext();
        var utilisateurs = db.Utilisateurs
            .AsNoTracking()
            .OrderBy(utilisateur => utilisateur.lastName)
            .ThenBy(utilisateur => utilisateur.firstName)
            .ToList();
        Utilisateurs = new ObservableCollection<Utilisateur>(utilisateurs);
    }

    public void ChargerVoitures()
    {
        using var db = new AppDbContext();
        var voitures = db.Voitures
            .AsNoTracking()
            .OrderBy(voiture => voiture.marque)
            .ThenBy(voiture => voiture.modele)
            .ToList();
        Voitures = new ObservableCollection<Voiture>(voitures);
    }

    public void ChargerPieces()
    {
        using var db = new AppDbContext();
        var pieces = db.Pieces
            .AsNoTracking()
            .OrderBy(piece => piece.nom_de_piece)
            .ToList();
        Pieces = new ObservableCollection<Piece>(pieces);
    }

    public void AjouterVoiture(Voiture voiture) => Voitures.Add(voiture);

    public void AjouterPiece(Piece piece) => Pieces.Add(piece);

    public void ModifierVoiture(Voiture voitureModifiee)
    {
        using var db = new AppDbContext();
        var voitureExistante = db.Voitures
            .FirstOrDefault(voiture => voiture.vin == voitureModifiee.vin);

        if (voitureExistante is null)
        {
            MessageBox.Show("Voiture introuvable pour la mise à jour.");
            return;
        }

        voitureExistante.marque = voitureModifiee.marque;
        voitureExistante.modele = voitureModifiee.modele;
        voitureExistante.annee = voitureModifiee.annee;
        voitureExistante.categorie = voitureModifiee.categorie;
        voitureExistante.prixAproximatif = voitureModifiee.prixAproximatif;
        voitureExistante.typeCarburant = voitureModifiee.typeCarburant;
        voitureExistante.kilometrage = voitureModifiee.kilometrage;
        voitureExistante.couleur = voitureModifiee.couleur;
        voitureExistante.transmission = voitureModifiee.transmission;
        voitureExistante.proprietaireActuel = voitureModifiee.proprietaireActuel;
        voitureExistante.etatGeneral = voitureModifiee.etatGeneral;
        voitureExistante.dateAchat = voitureModifiee.dateAchat;
        voitureExistante.derniereRevision = voitureModifiee.derniereRevision;
        voitureExistante.garantitRestant = voitureModifiee.garantitRestant;
        voitureExistante.assurance = voitureModifiee.assurance;

        db.SaveChanges();
        ChargerVoitures();
    }

    public void SupprimerVoiture(string vin)
    {
        using var db = new AppDbContext();
        var voiture = db.Voitures.FirstOrDefault(item => item.vin == vin);
        if (voiture is null)
        {
            MessageBox.Show($"Voiture introuvable avec le VIN : {vin}");
            return;
        }

        db.Voitures.Remove(voiture);
        db.SaveChanges();
        ChargerVoitures();
    }

    public void SupprimerUtilisateur(int id)
    {
        using var db = new AppDbContext();
        var utilisateur = db.Utilisateurs.Find(id);
        if (utilisateur is null)
        {
            MessageBox.Show("Utilisateur introuvable.");
            return;
        }

        db.Utilisateurs.Remove(utilisateur);
        db.SaveChanges();
        ChargerUtilisateurs();
    }

    public void SupprimerPiece()
    {
        if (PieceSelectionnee is null)
        {
            MessageBox.Show("Veuillez sélectionner une pièce à supprimer.");
            return;
        }

        using var db = new AppDbContext();
        var piece = db.Pieces.Find(PieceSelectionnee.id);
        if (piece is null)
        {
            MessageBox.Show("Pièce introuvable.");
            return;
        }

        db.Pieces.Remove(piece);
        db.SaveChanges();
        ChargerPieces();
        MessageBox.Show("Pièce supprimée avec succès.");
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
