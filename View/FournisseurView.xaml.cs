using System.Windows;
using TravailPratique2.ViewModels;

namespace TravailPratique2.View;

public partial class FournisseurView : Window
{
    public FournisseurView()
    {
        InitializeComponent();
        DataContext = new FournisseurVM();
    }

    private void AjouterVoiture_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is FournisseurVM viewModel)
        {
            new ajoutvoituref(viewModel) { Owner = this }.ShowDialog();
        }
    }

    private void AjouterPiece_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is FournisseurVM viewModel)
        {
            new ajoutpiecef(viewModel) { Owner = this }.ShowDialog();
        }
    }
}
