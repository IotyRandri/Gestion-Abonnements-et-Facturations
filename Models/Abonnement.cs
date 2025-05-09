using System.ComponentModel.DataAnnotations;

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Models
{
    public class Abonnement
    {
        public int Id { get; set; }

        public String? Nom { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}")]
        public double Prix { get; set; }

        public int TypeAbonnementId { get; set; }

        public TypeAbonnement? TypeAbonnement { get; set; }
    }
}
