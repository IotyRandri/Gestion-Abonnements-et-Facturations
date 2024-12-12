# Gestion-Abonnements-et-Facturations
Une application qui permettra de gerer les abonnements et facturation des abonnements d'un ou plusieurs utilisateur(s).
Gestion des abonnements et facturations des abonnements (par exemple service de streaming, abonnement à un ISP, abonnement TV, service appel et Internet mobile, ...) 

## Login
 On a 2 type de profils 
   - Admin (BackOffice)
   - Client/Utilisateur (FrontOffice)

## BackOffice

1. Gestion des types d'abonnements
     - CRUD         - hebdomadaire, mensuelle, annuelle, ...
   
2. Gestion des Abonnements
     - CRUD
        - ex: Canalsat, Mensuelle, 20 000Ar
        <!-- - Nom, type abonnement, prix -->
   
3. Gestion des utilisateurs
     - CRUD 
        <!-- Nom, prenom, Date Naissance, Adresse, motdepasse -->

4. Gestion Annulation et Remboursement
    - Prix de remboursement = (prix d'abonnement) - commission
    - Commission par rapport au prix d'abonnement
        - ex: 
            - Entre 10 000Ar et 50 000Ar : 2%
            - Entre 50 000Ar et 100 000Ar : 5%
            - Entre 100 000Ar et 200 000Ar: 10%
            - Plus de 200 000Ar: 15%

        <!-- Creer une table commission_annulation -->

## FrontOffice

1. Ajout Abonnement pour l'utilisateur
    - Choisir une date et heure de debut et un abonnement pour l'utilisateur

2. Suivi des abonnements à venir / à payer (ou Tableau de Bord) pour l'utilisateur
    - Filtre par type abonnement (hebdomadaire, mensuelle, annuelle, ...)
    
3. Paiement ou Annulation Abonnement
 
    Par type d'abonnement

    - Paiement
        - Création Facture et export en PDF   
    - Annulation
        - Remboursement
            - Création Facture de remboursement et export en PDF

    <!-- Stocker les paiements ou annulations dans une table -->
    <!-- Nom, type de facture, prix_total, date_facture, heure_facture -->

4. Historique des Factures/transactions
    - Recherche par type (facture/remboursement)
    - Filtre par type abonnement (mois, année, ...)

5. Statistiques des abonnements

    Tableau de bord des abonnements, ou graphique

    - Top 10 ou top 5 (plus cher)
    - Top 10 ou top 5 (moins cher)
    - Moyenne de dépenses par mois, année, ...
