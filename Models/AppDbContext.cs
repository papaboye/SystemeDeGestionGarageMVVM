using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TravailPratique2.Models
{
    class AppDbContext : DbContext
    {
        public DbSet<Models.Utilisateur> Utilisateurs { get; set; }
        public DbSet<Models.Reparation> Reparations { get; set; }
        public DbSet<Models.Voiture> Voitures { get; set; }
        public DbSet<Models.Piece> Pieces { get; set; }
        public DbSet<Models.Devis> Devis { get; set; }
        public DbSet<Models.Facture>Factures { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial " +
                "Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;" +
                "Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            string dbname = "TP2DB";
            options.UseSqlServer($"{connection};Database={dbname};");

        }
            
    }
}
