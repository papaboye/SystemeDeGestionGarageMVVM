using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravailPratique2.Migrations
{
    /// <inheritdoc />
    public partial class ajouttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devis",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<double>(type: "float", nullable: false),
                    reparation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    typeIntervention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estvalidee = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devis", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Pieces",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom_de_piece = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prix_approx = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pieces", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Reparations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categorie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reparation_associee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cout = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Voitures",
                columns: table => new
                {
                    vin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    marque = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modele = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    annee = table.Column<int>(type: "int", nullable: false),
                    categorie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prixAproximatif = table.Column<int>(type: "int", nullable: false),
                    kilometrage = table.Column<double>(type: "float", nullable: false),
                    couleur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    typeCarburant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    transmission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    etatGeneral = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    proprietaireActuel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateAchat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    derniereRevision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    garantitRestant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    assurance = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voitures", x => x.vin);
                });

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    montantTotal = table.Column<double>(type: "float", nullable: false),
                    modePaiement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    totalpieces = table.Column<double>(type: "float", nullable: false),
                    coutMain = table.Column<double>(type: "float", nullable: false),
                    statut = table.Column<bool>(type: "bit", nullable: false),
                    reparationAssocieeid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.id);
                    table.ForeignKey(
                        name: "FK_Factures_Reparations_reparationAssocieeid",
                        column: x => x.reparationAssocieeid,
                        principalTable: "Reparations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Factures_reparationAssocieeid",
                table: "Factures",
                column: "reparationAssocieeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devis");

            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "Pieces");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Voitures");

            migrationBuilder.DropTable(
                name: "Reparations");
        }
    }
}
