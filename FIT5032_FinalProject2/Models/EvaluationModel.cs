using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_FinalProject2.Models
{
    public partial class EvaluationModel : DbContext
    {
        public EvaluationModel()
            : base("name=EvaluationModel")
        {
        }

        public virtual DbSet<Evaluation> Evaluations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public System.Data.Entity.DbSet<FIT5032_FinalProject2.Models.Location> Locations { get; set; }
    }
}
