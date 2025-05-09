using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Models
{
    public class Facture
    {
        public int Id { get; set; }
        public required String Nom { get; set; }
        public DateTime Dateheure { get; set; }
        public required int UserId { get; set; }
        public Users? User { get; set; }
        public required int StatutAbonnementId { get; set; }
        public StatutAbonnement? StatutAbonnement { get; set; }
        public int? TypeAbonnementId { get; set; }
        public TypeAbonnement? TypeAbonnement { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}")]
        public double Prixtotal { get; set; }

    }
}
