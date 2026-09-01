using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TravailPratique2.Models;
using TravailPratique2.View;

namespace TravailPratique2.ViewModels;

internal sealed class ConnexionVM : INotifyPropertyChanged
{
    private readonly UserAPI _userApi = new();
    private IReadOnlyList<Utilisateur> _utilisateurs = [];
    private string _username = string.Empty;
    private string _statusMessage = "Chargement des comptes de démonstration…";
    private bool _isLoading = true;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ICommand ConnexionCommand { get; }

    public string Username
    {
        get => _username;
        set
        {
            if (_username == value)
            {
                return;
            }

            _username = value;
            OnPropertyChanged();
        }
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set
        {
            if (_statusMessage == value)
            {
                return;
            }

            _statusMessage = value;
            OnPropertyChanged();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        private set
        {
            if (_isLoading == value)
            {
                return;
            }

            _isLoading = value;
            OnPropertyChanged();
            CommandManager.InvalidateRequerySuggested();
        }
    }

    public ConnexionVM()
    {
        ConnexionCommand = new RelayCommand(CanSeConnecter, SeConnecter);
        _ = LoadUtilisateursAsync();
    }

    private async Task LoadUtilisateursAsync()
    {
        try
        {
            _utilisateurs = await _userApi.GetUtilisateursAsync();
            StatusMessage = $"{_utilisateurs.Count} comptes de démonstration disponibles.";
        }
        catch (Exception exception)
        {
            StatusMessage = "Le service d'authentification de démonstration est indisponible.";
            MessageBox.Show(
                exception.Message,
                "Connexion au service impossible",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
        finally
        {
            IsLoading = false;
        }
    }

    private bool CanSeConnecter(object? parameter) =>
        !IsLoading &&
        !string.IsNullOrWhiteSpace(Username) &&
        parameter is PasswordBox { Password.Length: > 0 };

    private void SeConnecter(object? parameter)
    {
        if (parameter is not PasswordBox passwordBox)
        {
            return;
        }

        var utilisateur = _utilisateurs.FirstOrDefault(user =>
            string.Equals(user.username, Username.Trim(), StringComparison.OrdinalIgnoreCase) &&
            string.Equals(user.password, passwordBox.Password, StringComparison.Ordinal));

        if (utilisateur is null)
        {
            MessageBox.Show(
                "Nom d'utilisateur ou mot de passe incorrect.",
                "Connexion refusée",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        var fenetre = CreerFenetrePourRole(utilisateur.role);
        if (fenetre is null)
        {
            MessageBox.Show(
                $"Le rôle « {utilisateur.role} » n'est pas pris en charge.",
                "Rôle non reconnu",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        fenetre.Show();
        Application.Current.MainWindow = fenetre;
        Window.GetWindow(passwordBox)?.Close();
    }

    private static Window? CreerFenetrePourRole(string? role) =>
        role?.Trim().ToLowerInvariant() switch
        {
            "admin" or "proprietaire" or "propriétaire" => new ProprietaireView(),
            "moderator" or "fournisseur" => new FournisseurView(),
            "user" or "client" => new ClientView(),
            _ => null
        };

    private void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
