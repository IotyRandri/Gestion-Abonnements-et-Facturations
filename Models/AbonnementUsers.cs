using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Models
{
    public class AbonnementUsers
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Users? User { get; set; }
        public required int AbonnementId { get; set; }
        public Abonnement? Abonnement { get; set; }
        public String? NomFacture { get; set; }
        public int StatutAbonnementId { get; set; }
        public StatutAbonnement? StatutAbonnement { get; set; }
        private DateTime _DateHeureDebut;
        [DisplayFormat(DataFormatString = "{0:dddd, MMMM dd, yyyy h:mm tt}")]
        public required DateTime DateHeureDebut{ 
            get => _DateHeureDebut;
            set => _DateHeureDebut = value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime(); // Convert to UTC if it's not already
        }
        [DisplayFormat(DataFormatString = "{0:dddd, MMMM dd, yyyy h:mm tt}")]
        public DateTime DateHeureFin { get; set; }
        public int? FactureId { get; set; }
        public Facture? Facture { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}")]
        public double PrixEstimation { get; set; }

        [NotMapped]
        public int Difference { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}")]

        public double PrixCommission { get; set; }

        [NotMapped]
        public double PrixAnnulation { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}")]
        public double PrixTotal { get; set; }

    }
}
