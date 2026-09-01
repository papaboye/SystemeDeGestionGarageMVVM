using System.Windows;
using TravailPratique2.ViewModels;

namespace TravailPratique2.View;

public partial class ClientView : Window
{
    public ClientView()
    {
        InitializeComponent();
        DataContext = new ClientVM();
    }

    private void Acheter_Click(object sender, RoutedEventArgs e) =>
        new AcheterVoiture { Owner = this }.ShowDialog();

    private void DemanderReparation_Click(object sender, RoutedEventArgs e)
    {
        new ReparationView { Owner = this }.ShowDialog();
        if (DataContext is ClientVM viewModel)
        {
            viewModel.ChargerDonnees();
        }
    }
}
