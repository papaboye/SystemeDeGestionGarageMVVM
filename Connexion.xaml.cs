using System.Windows;
using TravailPratique2.ViewModels;

namespace TravailPratique2;

public partial class Connexion : Window
{
    public Connexion()
    {
        InitializeComponent();
        DataContext = new ConnexionVM();
    }
}
