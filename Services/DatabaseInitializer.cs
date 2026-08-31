using Microsoft.EntityFrameworkCore;
using TravailPratique2.Models;

namespace TravailPratique2.Services;

internal static class DatabaseInitializer
{
    public static void Initialize()
    {
        using var db = new AppDbContext();
        db.Database.Migrate();

        SeedVoitures(db);
        SeedPieces(db);
        db.SaveChanges();
    }

    private static void SeedVoitures(AppDbContext db)
    {
        var vinsExistants = db.Voitures
            .Select(voiture => voiture.vin)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var voiture in Stock.ImporterVoiture())
        {
            if (vinsExistants.Add(voiture.vin))
            {
                db.Voitures.Add(voiture);
            }
        }
    }

    private static void SeedPieces(AppDbContext db)
    {
        var nomsExistants = db.Pieces
            .Select(piece => piece.nom_de_piece)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var piece in Stock.ImporterPiece())
        {
            if (nomsExistants.Add(piece.nom_de_piece))
            {
                db.Pieces.Add(piece);
            }
        }
    }
}
