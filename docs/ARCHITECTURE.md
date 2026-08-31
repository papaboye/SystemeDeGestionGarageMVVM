# Architecture technique

## Vue d'ensemble

Le projet suit une organisation MVVM pragmatique :

- les fichiers XAML dans **View/** décrivent l'interface;
- les classes dans **ViewModels/** exposent les collections et la logique de présentation;
- les entités dans **Models/** représentent le domaine du garage;
- **AppDbContext** centralise l'accès à SQL Server LocalDB;
- **DatabaseInitializer** applique les migrations et importe les données initiales.

## Cycle de démarrage

1. **App** lance l'initialisation de la base.
2. Entity Framework applique les migrations en attente.
3. Les fichiers CSV sont lus depuis le dossier de l'application.
4. Les véhicules sont comparés par VIN et les pièces par nom avant insertion.
5. La fenêtre de connexion charge les comptes de démonstration.

## Données

La base contient les ensembles suivants :

- utilisateurs;
- véhicules;
- pièces;
- réparations;
- devis;
- factures.

La chaîne de connexion cible **TP2DB** sur SQL Server Express LocalDB. Elle est adaptée à une démonstration locale Windows. Dans une version déployée, elle devrait provenir d'une configuration externe sécurisée.

## Sécurité

Le mot de passe est saisi dans un PasswordBox, n'est plus affiché dans la console et le rôle n'est plus choisi manuellement. L'API DummyJSON reste toutefois une source de comptes fictifs : elle ne constitue pas une solution d'authentification de production.

## Évolutions recommandées

- injecter le contexte et les services avec un conteneur d'injection de dépendances;
- ajouter des repositories ou services applicatifs;
- déplacer les dernières actions métier des fichiers code-behind vers des commandes;
- ajouter des tests unitaires pour les ViewModels et les règles de calcul;
- utiliser un véritable fournisseur d'identité.
