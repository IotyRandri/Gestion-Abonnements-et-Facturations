/* PostgreSQL */

-- Utilisateurs
/* Stocker les informations des utilisateurs */
CREATE TABLE users (
	id SERIAL PRIMARY KEY,
	nom VARCHAR(255),
	prenom VARCHAR(255),
	date_naissance date,
	motdepasse VARCHAR(10)
);

-- Type Abonnement
/* Stocker les types d'abonnements possible, par exemple (journalier, hebdomadaire, mensuelle, ...) */
CREATE TABLE type_abonnement (
	id SERIAL PRIMARY KEY, 
	titre VARCHAR(255)
);

-- Abonnement
/* Stocker les abonnements possible, par exemple (Canalsat, mensuelle, 20 000Ar) */
CREATE TABLE abonnement(
	id SERIAL PRIMARY KEY, 
	nom VARCHAR(255), 
	id_type_abonnement INT REFERENCES type_abonnement(id) NOT NULL, 
	prix double precision NOT NULL DEFAULT 0.0
);

-- Commission Annulation
/* Stocker les informations a propos des commissions recus lors de l'annulation d'un/des abonnements d'un utilisateur */
CREATE TABLE commission_annulation(
	id SERIAL PRIMARY KEY, 
	prix_min double precision NOT NULL DEFAULT 0.0, 
	prix_max double precision NOT NULL DEFAULT 0.0, 
	pourcentage double precision NOT NULL DEFAULT 0.0
);

-- Paiement Abonnement
/* Stocker les informations des paiements/annulations des abonnements d'un utilisateur

	 * Statut ou Statut Paiement: 
		1- Payer
		2- Annuler
*/

CREATE TABLE abonnement_users(
	id SERIAL PRIMARY KEY, 
	id_users INT REFERENCES users(id) NOT NULL, 
	id_abonnement INT REFERENCES abonnement(id) NOT NULL, 
	nom_facture VARCHAR(255), 
	statut INT DEFAULT 0, 
	date_heure_paiement TIMESTAMP default now()
);

