using System.Windows;
using TravailPratique2.Services;

namespace TravailPratique2;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        try
        {
            DatabaseInitializer.Initialize();
        }
        catch (Exception exception)
        {
            MessageBox.Show(
                "La base de données n'a pas pu être initialisée. " +
                "Vérifiez que SQL Server Express LocalDB est installé.\n\n" +
                exception.Message,
                "Initialisation impossible",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Shutdown();
        }
    }
}
