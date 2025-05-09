/* PostreSQL */

-- View Abonnement Users
/* Créer une vue pour obtenir les informations sur tout les abonnements d'un utilisateur, avec prix et type_abonnement */
CREATE VIEW view_abonnement_users AS 
	SELECT 
			au.id as id_ab_users,au.id_users,au.id_abonnement,au.nom_facture,au.statut,au.date_heure_paiement, -- abonnement_users
			u.nom,u.prenom,u.date_naissance, -- users
			a.nom as nom_ab,a.id_type_abonnement, a.prix, -- abonnement
			ta.titre -- type_abonnement

	FROM abonnement_users au 
		JOIN users u on au.id_users = u.id 
		JOIN abonnement a on a.id = au.id_abonnement
		JOIN type_abonnement ta on ta.id = a.id_type_abonnement