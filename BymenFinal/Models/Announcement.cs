namespace BymenFinal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Announcement")]
    public partial class Announcement
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Head { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
