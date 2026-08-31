using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using TravailPratique2.Models;

namespace TravailPratique2.ViewModels;

internal sealed class ClientVM : INotifyPropertyChanged
{
    private ObservableCollection<Voiture> _voitures = [];
    private ObservableCollection<Piece> _pieces = [];
    private ObservableCollection<Reparation> _reparations = [];

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

    public ObservableCollection<Reparation> Reparations
    {
        get => _reparations;
        private set
        {
            _reparations = value;
            OnPropertyChanged();
        }
    }

    public ClientVM() => ChargerDonnees();

    public void ChargerDonnees()
    {
        using var db = new AppDbContext();
        Voitures = new ObservableCollection<Voiture>(
            db.Voitures.AsNoTracking().OrderBy(voiture => voiture.marque).ToList());
        Pieces = new ObservableCollection<Piece>(
            db.Pieces.AsNoTracking().OrderBy(piece => piece.nom_de_piece).ToList());
        Reparations = new ObservableCollection<Reparation>(
            db.Reparations.AsNoTracking().OrderByDescending(reparation => reparation.id).ToList());
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
