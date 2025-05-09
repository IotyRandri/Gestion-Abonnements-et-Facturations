using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Models
{
    [NotMapped]
    public class AbonnementUsersDTO
    {
        public String? NomAbonnement { get; set; }
        public Abonnement? Abonnement { get; set; }
        public double TotalPrix { get; set; }
        public int Year { get; set; }
        public int Mois { get; set; }
        public double MoyenneDepenseMois { get; set; } 
    }
}
