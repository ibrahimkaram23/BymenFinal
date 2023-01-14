namespace BymenFinal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("team")]
    public partial class team
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Position { get; set; }

        
        public string image { get; set; }

        [StringLength(250)]
        [Required]
        public string qute { get; set; }
    }
}
