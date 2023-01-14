namespace BymenFinal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Volanteer
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string fullName { get; set; }

        [StringLength(250)]
        public string phoneNumber { get; set; }

        [StringLength(250)]
        public string major { get; set; }

        [StringLength(250)]
        public string exGrad { get; set; }

        public bool? academicProbation { get; set; }

        public bool? attendAUB { get; set; }
        public bool? partBey { get; set; }

        public int? position { get; set; }

        [StringLength(500)]
        public string gIntersted { get; set; }

        [StringLength(150)]
        public string gEnglishRate { get; set; }

        [StringLength(250)]
        public string gMassCommunication { get; set; }

        [StringLength(250)]
        public string sLandscape { get; set; }

        public string sBackgroundSponser { get; set; }

        public string sProspectiveSponsors { get; set; }

        public string sPotentialVenues { get; set; }

        public string sMarketingPlan { get; set; }

        public string sExperienceGrapgic { get; set; }

        public string sExperienceOrganizing { get; set; }

        public bool? sProgrammingKnow { get; set; }

        [StringLength(250)]
        public string sExperienceProgramming { get; set; }

        public string sMaintainSecurity { get; set; }

        public virtual VolanteersPossition VolanteersPossition { get; set; }
    }
}
