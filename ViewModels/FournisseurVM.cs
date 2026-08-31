using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using TravailPratique2.Models;

namespace TravailPratique2.ViewModels;

public sealed class FournisseurVM : INotifyPropertyChanged
{
    private ObservableCollection<Voiture> _voitures = [];
    private ObservableCollection<Piece> _pieces = [];

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

    public ObservableCollection<Piece> Pieces
    {
        get => _pieces;
        private set
        {
            _pieces = value;
            OnPropertyChanged();
        }
    }

    public FournisseurVM()
    {
        ChargerVoitures();
        ChargerPieces();
    }

    public void ChargerVoitures()
    {
        using var db = new AppDbContext();
        Voitures = new ObservableCollection<Voiture>(
            db.Voitures.AsNoTracking()
                .OrderBy(voiture => voiture.marque)
                .ThenBy(voiture => voiture.modele)
                .ToList());
    }

    public void ChargerPieces()
    {
        using var db = new AppDbContext();
        Pieces = new ObservableCollection<Piece>(
            db.Pieces.AsNoTracking()
                .OrderBy(piece => piece.nom_de_piece)
                .ToList());
    }

    public void AjouterVoiture(Voiture voiture) => Voitures.Add(voiture);

    public void AjouterPiece(Piece piece) => Pieces.Add(piece);

    private void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
