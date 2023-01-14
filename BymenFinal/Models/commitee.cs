namespace BymenFinal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class commitee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string comName { get; set; }

        [Required]
        [StringLength(250)]
        public string comType { get; set; }
    }
}
