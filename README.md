# Gestion-Abonnements-et-Facturations
Une application qui permettra de gerer les abonnements et facturation des abonnements d'un ou plusieurs utilisateur(s).
Gestion des abonnements et facturations des abonnements (par exemple service de streaming, abonnement à un ISP, abonnement TV, service appel et Internet mobile, ...) 

## Login
 On a 2 type de profil 
   - Admin (BackOffice)
   - Client/Utilisateur (FrontOffice)

## BackOffice

1. Gestion des Types d'abonnements
     CRUD (hebdomadaire, mensuelle, annuelle, ...)
   
3. Gestion des Abonnements
     CRUD (Nom, type abonnement, prix)
   
4. Gestion des utilisateurs
     CRUD (Nom, prenom, Date Naissance, Adresse, ...)
   
     - Gestion des factures des utilisateurs
         - CRUD (nom, abonnement utilisateur, date)

## FrontOffice

1. Suivi des abonnements à venir/payer ou Tableau de Bord
    - Filtre par type abonnement, mois, année
2. Historique des transactions
    - Recherche des abonnements
    - Filtre par type abonnement, mois, année
3. Création et export factures en PDF ou Excel (.csv)
