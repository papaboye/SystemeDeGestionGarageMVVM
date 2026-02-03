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
    class ReparationVM
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Voiture> _voiture;
        private ObservableCollection<Piece> _piece;
        private ObservableCollection<Reparation> _reparations;
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
        public ObservableCollection<Reparation> Reparations
        {
            get { return _reparations; }
            set { if (_reparations != value) { _reparations = value; OnPropertyChanged(); } }
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void ChargerReparatrions()
        {
            var listereparation = Stock.ImporterReparation();
            Reparations = new ObservableCollection<Reparation>(listereparation);

        }
    }
}
