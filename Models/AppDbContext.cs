using Microsoft.EntityFrameworkCore;

namespace TravailPratique2.Models;

internal sealed class AppDbContext : DbContext
{
    public DbSet<Utilisateur> Utilisateurs => Set<Utilisateur>();
    public DbSet<Reparation> Reparations => Set<Reparation>();
    public DbSet<Voiture> Voitures => Set<Voiture>();
    public DbSet<Piece> Pieces => Set<Piece>();
    public DbSet<Devis> Devis => Set<Devis>();
    public DbSet<Facture> Factures => Set<Facture>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=TP2DB;" +
                "Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
