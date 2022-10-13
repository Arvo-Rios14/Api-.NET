using Microsoft.EntityFrameworkCore;
using Sithec.Models.Entity;

namespace Sithec.Models
{
    public class SithecContext : DbContext
    {
        public SithecContext(DbContextOptions<SithecContext> options)
            : base(options)
        {

        }

        public DbSet<Human> Humans { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Human>().ToTable("Human");
        //}
    }
}