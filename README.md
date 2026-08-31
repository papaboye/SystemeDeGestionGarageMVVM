# Garage Management — WPF et MVVM

[![Build](https://github.com/papaboye/SystemeDeGestionGarageMVVM/actions/workflows/build.yml/badge.svg)](https://github.com/papaboye/SystemeDeGestionGarageMVVM/actions/workflows/build.yml)

Application de bureau Windows pour gérer les activités principales d'un garage automobile : inventaire des véhicules et des pièces, utilisateurs, demandes de réparation, devis et factures.

Ce projet met en pratique **C#**, **WPF**, le patron **MVVM**, **Entity Framework Core**, **SQL Server LocalDB**, l'importation CSV et la consommation d'une API REST.

## Fonctionnalités

| Espace | Fonctions principales |
| --- | --- |
| Propriétaire | Consulter et administrer les véhicules, les pièces et les utilisateurs |
| Fournisseur | Approvisionner le stock de véhicules et de pièces |
| Client | Consulter le catalogue, demander une réparation et valider un devis |

L'application calcule le montant d'un devis à partir des pièces et de la main-d'œuvre, puis génère une facture lorsque le devis est accepté.

## Architecture

~~~mermaid
flowchart TD
    View["Vues WPF / XAML"] --> VM["ViewModels"]
    VM --> EF["Entity Framework Core"]
    EF --> DB["SQL Server LocalDB"]
    CSV["Données CSV"] --> Seed["Initialisation"]
    Seed --> DB
    API["DummyJSON REST API"] --> Auth["Authentification de démonstration"]
    Auth --> VM
~~~

La base **TP2DB** est créée et migrée automatiquement au démarrage. Les fichiers CSV fournis contiennent uniquement des données fictives et initialisent les véhicules et les pièces sans créer de doublons.

Une description plus détaillée est disponible dans [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md).

## Technologies

- .NET 8 et C#;
- WPF et XAML;
- Entity Framework Core 9;
- SQL Server Express LocalDB;
- CsvHelper;
- Newtonsoft.Json;
- API REST DummyJSON;
- GitHub Actions.

## Prérequis

- Windows 10 ou 11;
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0);
- SQL Server Express LocalDB, fourni notamment avec Visual Studio.

## Installation

~~~powershell
git clone https://github.com/papaboye/SystemeDeGestionGarageMVVM.git
cd SystemeDeGestionGarageMVVM
dotnet restore
dotnet run --project TravailPratique2.csproj
~~~

Les fichiers **vehicules_db.csv** et **reparations_db.csv** sont automatiquement copiés dans le dossier de sortie lors de la compilation.

## Authentification de démonstration

L'écran de connexion charge les comptes publics de démonstration fournis par [DummyJSON](https://dummyjson.com/docs/users). Le rôle du compte détermine automatiquement l'espace ouvert :

- admin → propriétaire;
- moderator → fournisseur;
- user → client.

Cette authentification sert uniquement à la démonstration du projet. Une application de production utiliserait un fournisseur d'identité et ne conserverait jamais de mots de passe en clair.

## Structure du projet

~~~text
├── Models/               Entités et contexte Entity Framework
├── View/                 Fenêtres et composants WPF
├── ViewModels/           État, commandes et logique de présentation
├── Services/             Initialisation de la base
├── Migrations/           Schéma Entity Framework Core
├── docs/                 Documentation technique
├── vehicules_db.csv      Jeu de données initial des véhicules
└── reparations_db.csv    Jeu de données initial des pièces
~~~

## Qualité

Le workflow GitHub Actions restaure les dépendances et compile automatiquement le projet en mode **Release** pour chaque Pull Request vers **master**.

## Limites et améliorations prévues

- l'application WPF fonctionne uniquement sous Windows;
- l'authentification actuelle est une démonstration basée sur un service externe;
- le rôle vendeur et les tests automatisés de l'interface restent à ajouter;
- l'interface peut encore être enrichie avec davantage de commandes MVVM.

## Auteur

**Papa Alioune Boye** — étudiant à la maîtrise en informatique à l'UQAR.
