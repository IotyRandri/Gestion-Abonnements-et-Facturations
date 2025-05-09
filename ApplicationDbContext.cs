using Microsoft.EntityFrameworkCore;
using Projet_.NET_Core_M2_Devops_Mr_Naina.Models;

namespace Projet_.NET_Core_M2_Devops_Mr_Naina
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TypeAbonnement> TypeAbonnements { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Projet_.NET_Core_M2_Devops_Mr_Naina.Models.Abonnement> Abonnement { get; set; } = default!;
        public DbSet<Projet_.NET_Core_M2_Devops_Mr_Naina.Models.Users> Users { get; set; } = default!;
        public DbSet<Projet_.NET_Core_M2_Devops_Mr_Naina.Models.AbonnementUsers> AbonnementUsers { get; set; } = default!;
        public DbSet<Projet_.NET_Core_M2_Devops_Mr_Naina.Models.CommissionAnnulation> CommissionAnnulation { get; set; } = default!;
        public DbSet<Projet_.NET_Core_M2_Devops_Mr_Naina.Models.StatutAbonnement> StatutAbonnement { get; set; } = default!;
        public DbSet<Projet_.NET_Core_M2_Devops_Mr_Naina.Models.Facture> Facture { get; set; } = default!;
    }
}
