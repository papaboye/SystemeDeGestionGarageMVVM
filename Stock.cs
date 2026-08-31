using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using TravailPratique2.Models;

namespace TravailPratique2;

internal static class Stock
{
    public static List<Voiture> ImporterVoiture() =>
        LireCsv<Voiture>("vehicules_db.csv");

    public static List<Piece> ImporterPiece() =>
        LireCsv<Piece>("reparations_db.csv");

    private static List<T> LireCsv<T>(string nomFichier)
    {
        var chemin = Path.Combine(AppContext.BaseDirectory, nomFichier);
        if (!File.Exists(chemin))
        {
            throw new FileNotFoundException(
                $"Le fichier de données '{nomFichier}' est introuvable.", chemin);
        }

        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            Encoding = Encoding.UTF8,
            HasHeaderRecord = true,
            HeaderValidated = null,
            MissingFieldFound = null
        };

        using var lecteur = new StreamReader(chemin, Encoding.UTF8);
        using var csv = new CsvReader(lecteur, configuration);
        return csv.GetRecords<T>().ToList();
    }
}
