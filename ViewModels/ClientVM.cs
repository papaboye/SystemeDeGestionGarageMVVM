using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TravailPratique2.Models;

namespace TravailPratique2.ViewModels
{
    class ClientVM
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Voiture> _voiture;
        private ObservableCollection<Piece> _piece;
        public ObservableCollection<Voiture> Voitures

        {
            get { return _voiture; }
            set { if (_voiture != value) { _voiture = value; OnPropertyChanged(); } }
        }
        public ObservableCollection<Piece> Pieces
        {
            get { return _piece; }
            set { if (_piece != value) { _piece = value; OnPropertyChanged(); } }
        }
        public ClientVM()
        {
            ChargerPieces();
            ChargerVoitures();
        }
        public void ChargerVoitures()
        {
            var listevoiture = Stock.ImporterVoiture();
            Voitures = new ObservableCollection<Voiture>(listevoiture);
        }
        
        public void ChargerPieces()
        {
            var listepiece = Stock.ImporterPiece();
            Pieces = new ObservableCollection<Piece>(listepiece);

        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
