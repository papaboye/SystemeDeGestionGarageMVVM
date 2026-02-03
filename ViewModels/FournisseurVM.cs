using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravailPratique2.Models;
using TravailPratique2.View;

namespace TravailPratique2.ViewModels
{
    public class FournisseurVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Voiture> _voiture;
        private ObservableCollection<Piece> _piece;
        public ICommand CommandOuvrirAV { get; }
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
        public FournisseurVM()
        {
            ChargerVoitures();
            ChargerPieces();
          
        }


        public void ChargerVoitures()
        {
            try
            {
                using (var db = new AppDbContext())
                {

                    var voituresCsv = Stock.ImporterVoiture();
                    var voituresEnBase = db.Voitures.ToList();
                    foreach (var voiture in voituresCsv)
                    {
                        if (!voituresEnBase.Any(v => v.vin == voiture.vin))
                        {
                            db.Voitures.Add(voiture);
                            voituresEnBase.Add(voiture);
                        }
                    }

                    db.SaveChanges();

                    Voitures = new ObservableCollection<Voiture>(voituresEnBase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des voitures : " + ex.Message);
            }
        }
        public void ChargerPieces()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var piecesCsv = Stock.ImporterPiece();
                    var piecedb = db.Pieces.ToList();
                    foreach (var piece in piecesCsv)
                    {
                        db.Pieces.Add(piece);
                        piecedb.Add(piece);
                    }
                    db.SaveChanges();
                    Pieces = new ObservableCollection<Piece>(piecedb);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur lors du chargement des pieces : " + e.Message);
            }
        }
        public void AjouterVoiture(Voiture voiture)
        {
            Voitures.Add(voiture);
        }
        public void AjouterPiece(Piece piece)
        {
            Pieces.Add(piece);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }

}
