namespace BymenFinal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("persons")]
    public partial class person
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string fullName { get; set; }

        [Required]
        [StringLength(200)]
        public string emailAddress { get; set; }

        [Required]
        [StringLength(200)]
        public string university { get; set; }

        [Required]
        [StringLength(200)]
        public string eduYear { get; set; }

        [Required]
        [StringLength(200)]
        public string eduMajor { get; set; }

        [Required]
        [StringLength(200)]
        public string country { get; set; }

        public string profilePic { get; set; }

        [Required]
        [StringLength(200)]
        public string conferaneModel { get; set; }

        [Required]
        [StringLength(200)]
        public string committee { get; set; }

        public string PaymentProof { get; set; }

        [StringLength(250)]
        public string partType { get; set; }

        public string age { get; set; }
        public string enrolledAs { get; set; }
        public string gender { get; set; }
        public string nationality { get; set; }
        public string countryCode { get; set; }
        public string phoneNb { get; set; }
        public string isVaccinated { get; set; }
        public string doses { get; set; }
        public string passportPict { get; set; }
        public string vaccinationCert { get; set; }
        public string reference { get; set; }
        public int? GropId { get; set; }
    }
}
