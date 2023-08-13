namespace FIT5032_FinalProject2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Evaluation")]
    public partial class Evaluation
    {
        public int Id { get; set; }

        public int Recovery { get; set; }

        public int Waiting { get; set; }

        public int satisfaction { get; set; }

        [Required]
        public string UserID { get; set; }
    }
}
