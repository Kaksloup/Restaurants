# **Mini Projet : Gestion de Restaurant avec Clean Architecture**

## **Objectif du projet**
Ce projet met en œuvre une application de gestion de restaurant basée sur le principe de **Clean Architecture**. L'objectif est de fournir une structure modulaire, facilement testable, maintenable et extensible.

### **Fonctionnalités de l'application**
1. Gestion des menus (CRUD).
2. Gestion des commandes clients.
3. Authentification et autorisation des utilisateurs (administrateurs et employés).
4. Téléchargement et gestion des images des plats avec Azure Blob Storage.
5. Enregistrement des logs avec Serilog.

---

## **Technologies Utilisées**

### **Backend**
- **.NET 8** : Framework principal pour le développement.
- **Entity Framework Core** : ORM pour interagir avec la base de données.
- **SQL Server** : Base de données relationnelle.

### **Architecture et Design Patterns**
- **Clean Architecture** : Séparation des préoccupations avec des couches distinctes.
- **Mediator** : Gestion des flux de données entre couches via **MediatR**.
- **AutoMapper** : Mapping entre DTOs et entités.

### **Sécurité et Gestion des Utilisateurs**
- **Identity** : Authentification et gestion des rôles des utilisateurs.

### **Stockage**
- **Azure Blob Storage** : Stockage des images des plats.

### **Qualité du Code et Tests**
- **XUnit** : Framework de tests unitaires.
- **Moq** : Création de mocks pour les dépendances.
- **FluentAssertions** : Assertions lisibles pour les tests.
- **FluentValidation** : Validation des données d'entrée.

### **Outils et Utilitaires**
- **Swagger** : Documentation interactive de l'API.
- **Serilog** : Gestion avancée des logs.

---

## **Structure du Projet (Clean Architecture)**

Le projet suit la Clean Architecture en divisant le code en plusieurs couches :

### **1. Domain**
- Contient les entités métier, les interfaces et les exceptions.
- Pas de dépendances sur les autres couches.

### **2. Application**
- Contient les cas d'utilisation (use cases) et les règles métier.
- Utilise **Mediator** pour communiquer avec les handlers.
- Contient les contrats (DTOs), les validations avec **FluentValidation** et les interfaces pour les services.

### **3. Infrastructure**
- Implémente les interfaces définies dans l’Application.
- Contient les configurations d'Entity Framework Core, Azure Blob Storage, Identity et Serilog.

### **4. API**
- Couche d’entrée de l’application.
- Définit les contrôleurs et expose les endpoints via Swagger.
- Configure l’injection des dépendances et les middlewares.

---

## **Instructions pour Lancer le Projet**

### **1. Prérequis**
- **SDK .NET 8**
- **SQL Server** installé localement ou sur un serveur.
- Un compte **Azure Storage** configuré.

### **2. Configuration**

#### **Configurer l’API**
1. **Fichier `appsettings.json`** :
   - Ajouter la chaîne de connexion SQL Server :
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=RestaurantDb;User Id=sa;Password=YourPassword;"
     }
     ```
   - Ajouter les informations Azure Blob Storage :
     ```json
     "AzureBlobStorage": {
       "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=YourAccountName;AccountKey=YourAccountKey;EndpointSuffix=core.windows.net",
       "ContainerName": "restaurant-images"
     }
     ```
2. **Migration de la base de données** :
   - Exécuter la commande suivante dans le terminal :
     ```bash
     dotnet ef database update
     ```

### **3. Lancer le Projet**
1. Exécutez la commande :
   ```bash
   dotnet run --project src/API
   ```
2. Accédez à Swagger : [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## **Exemple d’EndPoints Swagger**

### **Menus**
- **GET /api/menus** : Récupère tous les menus.
- **POST /api/menus** : Crée un nouveau menu.

### **Commandes**
- **GET /api/orders** : Récupère toutes les commandes.
- **POST /api/orders** : Crée une nouvelle commande.

---

## **Tests Unitaires**

### **Structure des Tests**
- **Application.Tests** :
  - Tests des cas d’utilisation avec **XUnit** et **Moq**.
  - Assertions avec **FluentAssertions**.
- **Infrastructure.Tests** :
  - Tests des interactions avec la base de données.

### **Exécution des Tests**
1. Lancer les tests :
   ```bash
   dotnet test
   ```
2. Vérifier les rapports de couverture.

---

## **Prochaines Étapes**
1. Ajouter un système de notification (email ou SMS) pour les commandes.
2. Intégrer un système de paiement.
3. Déployer l’application sur Azure App Services.


