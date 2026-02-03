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
    class ConnexionVM : INotifyPropertyChanged
    {
        private UserAPI userapi;
        List<Utilisateur> user = new List<Utilisateur> ();
        public event PropertyChangedEventHandler PropertyChanged;
        public string username;
        public string password;
        public string role { get; set; }
        public Models.Utilisateur Utilisateur { get; set; }
        public ObservableCollection<string> Roles { get; set; }
        public ICommand ConnexionCommand { get; }
        public string Username
        {
            get { return username; }
            set { if(username != value)
                {
                    username = value;
                     OnPropertyChanged();
                } }
        }
        public string Password
        {
            get { return password; }
            set { 
                if (password!= value)
                {
                    password = value;
                    OnPropertyChanged();
                } 
            }
        }
        public string Role
        {
            get { return role; }
            set { if (role != value)
                {
                    role = value;
                    OnPropertyChanged();
                } }
        }
        
        public ConnexionVM()
        {
            Roles = new ObservableCollection<string>() { "Proprietaire", "Fournisseur", "Client", "Vendeur" };
            userapi = new UserAPI();
            user = new List<Utilisateur>();
            ConnexionCommand = new RelayCommand(
                
                o => CanSeConnecter(),
                o => Seconnecter()
            );
            LoadUtilisateurs();
        }
        private async void LoadUtilisateurs()
        {
            user = await userapi.GetUtilisateursAsync();
            foreach (var u in user)
            {
                Console.WriteLine($"Username: {u.username}, Password: {u.password}");
            }
        }
        private bool CanSeConnecter()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }
        
        public bool verifier;
        public void Verifier()
        {
            if (user == null || user.Count == 0)
                return;

            Utilisateur = user.Find(u => u.username == Username && u.password == Password);

            verifier = Utilisateur != null;
        }
        public void Seconnecter()
        {
            Verifier(); // Vérifie d'abord si l'utilisateur existe

            if (!verifier)
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (Role)
            {
                case "Proprietaire":
                    new ProprietaireView().Show();
                    
                    break;
                case "Fournisseur":
                    new FournisseurView().Show();
                   
                    break;
                case "Client":
                    new ClientView().Show();
                    break;
                case "Vendeur":
                    //new VendeurView().Show();
                    break;
                default:
                    MessageBox.Show("Rôle non reconnu.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
