using System.Windows;
using TravailPratique2.Models;

namespace TravailPratique2.View;

public partial class DevisView : Window
{
    public Devis Devis { get; }
    public bool estvalide { get; private set; }

    public DevisView(Devis devis)
    {
        InitializeComponent();
        Devis = devis;
        DataContext = devis;
    }

    private void Valider_Click(object sender, RoutedEventArgs e)
    {
        estvalide = true;
        Close();
    }

    private void Annuler_Click(object sender, RoutedEventArgs e) => Close();
}
