using System.ComponentModel.DataAnnotations;

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Models
{
    public class CommissionAnnulation
    {
        public int Id { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:N}")]
        public required double Prixmin { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}")]
        public required double Prixmax { get; set; }

        [DisplayFormat(DataFormatString = "{0:P}")]
        public required double Pourcentage { get; set; }
    }
}
