using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Models
{
    [NotMapped]
    public class PaginatedListViewModel<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
