using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravailPratique2.Models;
using TravailPratique2.View;

namespace TravailPratique2.ViewModels
{
    class PieceVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Piece> _pieces;
        private string _nom_de_piece;
        private double _prixApprox;
        public string NomdePiece
        {
            get { return _nom_de_piece; }
            set { if (_nom_de_piece != value) { _nom_de_piece = value; }OnPropertyChanged(); }
        }
        public double PrixApprox
        {
            get { return _prixApprox; }
            set { if (_prixApprox != value) { _prixApprox = value; }OnPropertyChanged(); }
        }
        public ICommand CommandAjoutPiece { get; }

        public void ChargerPieces()
        {
            var listepiece = Stock.ImporterPiece();
            Pieces = new ObservableCollection<Piece>(listepiece);
        }

        public ObservableCollection<Piece> Pieces
        {
            get => _pieces;
            set
            {
                _pieces = value;
                OnPropertyChanged();
            }
        }
        
        public PieceVM()
        {
            ChargerPieces();
            CommandAjoutPiece = new RelayCommand(
                o=>true,
                o=>Ajouterpiece());
        }
        public void Ajouterpiece()
        {
            var nouveau = new Piece
            {
                nom_de_piece = this.NomdePiece,
                prix_approx = this.PrixApprox
            };
            Pieces.Add(nouveau);
        }
        private void OnPropertyChanged([CallerMemberName] string PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
