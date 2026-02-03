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

namespace TravailPratique2.ViewModels
{
    public class VoitureVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Voiture> _voitures;
        private Voiture _nouvelleVoiture;

        public ObservableCollection<Voiture> Voitures
        {
            get => _voitures;
            set
            {
                _voitures = value;
                OnPropertyChanged();
            }
        }

        public Voiture NouvelleVoiture
        {
            get => _nouvelleVoiture;
            set
            {
                if (_nouvelleVoiture != value)
                {
                    _nouvelleVoiture = value;
                    OnPropertyChanged();
                }
            }
        }

        

        

        public VoitureVM()
        {
            
           

            
            

         
            
        }

        private void ChargerVoitures()
        {
            var listeImportee = Stock.ImporterVoiture();

            if (listeImportee != null)
                Voitures = new ObservableCollection<Voiture>(listeImportee);
            else
                Voitures = new ObservableCollection<Voiture>();
        }

        

        private void OnPropertyChanged([CallerMemberName] string nomPropriete = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));
        }
     
        
        
        
        
       
       
    }
}
