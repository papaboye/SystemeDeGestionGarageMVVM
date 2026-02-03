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
using Microsoft.Identity.Client;
using TravailPratique2.Models;
using TravailPratique2.View;

namespace TravailPratique2.ViewModels
{
    public class ProprietaireVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        
        private ObservableCollection<Voiture> _voiture { get; set; } = new ObservableCollection<Voiture>();
        private ObservableCollection<Utilisateur> _utilisateurs;
        private ObservableCollection<Piece> _pieces;
        public ICommand CommandAjouterVoiture { get; }
        public ICommand CommandModifierVoiture { get; }
        public ICommand CommandSupprimerVoiture { get; }
        public ICommand CommandAjouterPiece { get; }
        public ICommand CommandModifierPiece { get; }
        public ICommand CommandSupprimerPiece { get; }
        public ICommand CommandAjouterUtilisateur { get; }
        public ICommand CommandModifierUtilisateur { get; }
        public ICommand CommandSupprimerUtilisateur { get; }

        
        
        public ObservableCollection<Voiture> Voitures
        {
            get { return _voiture; }
            set { if (_voiture != value) { _voiture = value; OnPropertyChanged(); } }
        }
        
        
        public ObservableCollection<Utilisateur> Utilisateurs
        {
            get => _utilisateurs;
            set
            {
                _utilisateurs = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Piece> Pieces
        {
            get { return _pieces; }
            set { if (_pieces != value){ _pieces = value; OnPropertyChanged();} }
        }
        private Piece _pieceSelectionnee;
        public Piece PieceSelectionnee
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
        private async void ChargerUtilisateurs()
        {
            
                    var api = new UserAPI();
                    var liste = await api.GetUtilisateursAsync(); 
                
                
                    
                   
                    Utilisateurs = new ObservableCollection<Utilisateur>(liste);
                              
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
            catch(Exception e)
            {
                MessageBox.Show("Erreur lors du chargement des pieces : " + e.Message);
            }
        }
        public void AjouterUtilisateur(Utilisateur utilisateur)
        {
            
            using (var db = new AppDbContext())
            {
                try
                {
                    
                    db.Utilisateurs.Add(utilisateur);  
                    db.SaveChanges();  

                    
                    Utilisateurs.Add(utilisateur);

                    MessageBox.Show("Utilisateur ajouté avec succès !");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'ajout de l'utilisateur: {ex.Message}");
                }
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

        public void ModifierVoiture(Voiture voitureModifiee)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var voitureExistante = db.Voitures.FirstOrDefault(v => v.vin == voitureModifiee.vin); 
                    if (voitureExistante != null)
                    {
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
                    }
                    else
                    {
                        MessageBox.Show("Voiture introuvable pour la mise à jour.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
                }
            }
            ChargerVoitures();
        }
        public void SupprimerVoiture(string vin)
        {
            using (var context = new AppDbContext())
            {
                var voiture = context.Voitures.FirstOrDefault(v => v.vin == vin);
                if (voiture != null)
                {
                    context.Voitures.Remove(voiture);
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Voiture introuvable avec le VIN : " + vin);
                }
            }

            ChargerVoitures();
        }
        public void SupprimerUtilisateur()
        {

        }
        public void SupprimerPiece()
        {
            if (PieceSelectionnee == null)
            {
                MessageBox.Show("Veuillez sélectionner une pièce à supprimer.");
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    var pieceASupprimer = db.Pieces.Find(PieceSelectionnee.id);
                    if (pieceASupprimer != null)
                    {
                        db.Pieces.Remove(pieceASupprimer);
                        db.SaveChanges();
                    }
                }

                Pieces.Remove(PieceSelectionnee);
                PieceSelectionnee = null;

                MessageBox.Show("Pièce supprimée avec succès.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression : {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
