using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_FinalProject2.Models
{
    public partial class AppointmentModel : DbContext
    {
        public AppointmentModel()
            : base("name=AppointmentModel2")
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
