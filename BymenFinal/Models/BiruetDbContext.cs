using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BymenFinal.Models
{
    public partial class BiruetDbContext : DbContext
    {
        public BiruetDbContext()
            : base("name=BiruetDbContext3")
        {
        }

        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<Announcement> Announcements { get; set; }
        public virtual DbSet<commitee> commitees { get; set; }
        public virtual DbSet<Gallery> Galleries { get; set; }
        public virtual DbSet<person> persons { get; set; }
        public virtual DbSet<sponser> sponsers { get; set; }
        public virtual DbSet<team> teams { get; set; }
        public virtual DbSet<Volanteer> Volanteers { get; set; }
        public virtual DbSet<VolanteersPossition> VolanteersPossitions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<admin>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<admin>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Gallery>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<person>()
                .Property(e => e.emailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<person>()
                .Property(e => e.eduYear)
                .IsUnicode(false);

            modelBuilder.Entity<person>()
                .Property(e => e.eduMajor)
                .IsUnicode(false);

            modelBuilder.Entity<person>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<person>()
                .Property(e => e.profilePic)
                .IsUnicode(false);

            modelBuilder.Entity<person>()
                .Property(e => e.conferaneModel)
                .IsUnicode(false);

            modelBuilder.Entity<person>()
                .Property(e => e.committee)
                .IsUnicode(false);

            modelBuilder.Entity<person>()
                .Property(e => e.PaymentProof)
                .IsUnicode(false);

            modelBuilder.Entity<sponser>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<team>()
                .Property(e => e.qute)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.fullName)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.major)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.exGrad)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.gIntersted)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.gEnglishRate)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.gMassCommunication)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.sLandscape)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.sBackgroundSponser)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.sProspectiveSponsors)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.sPotentialVenues)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.sMarketingPlan)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.sExperienceGrapgic)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.sExperienceOrganizing)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.sExperienceProgramming)
                .IsUnicode(false);

            modelBuilder.Entity<Volanteer>()
                .Property(e => e.sMaintainSecurity)
                .IsUnicode(false);

            modelBuilder.Entity<VolanteersPossition>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<VolanteersPossition>()
                .HasMany(e => e.Volanteers)
                .WithOptional(e => e.VolanteersPossition)
                .HasForeignKey(e => e.position);
        }
    }
}
