using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_FinalProject2.Models
{
    public partial class map_Model : DbContext
    {
        public map_Model()
            : base("name=map_Model")
        {
        }

        public virtual DbSet<Location> Location { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.Latitude)
                .HasPrecision(10, 8);

            modelBuilder.Entity<Location>()
                .Property(e => e.Longitude)
                .HasPrecision(11, 8);
        }
    }
}
