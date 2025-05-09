using System.ComponentModel.DataAnnotations;

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Models
{
    public class Users
    {
        public int Id { get; set; }
        public required String Nom { get; set; }
        public required String Prenom { get; set; }
        public DateOnly DateNaisssance { get; set; }
        public String? Adresse { get; set; }
        public required String Username { get; set; }
        [DataType(DataType.Password)]
        public required String Motdepasse { get; set; }
    }
}
