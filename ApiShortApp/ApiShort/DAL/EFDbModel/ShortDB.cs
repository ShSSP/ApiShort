using System.Data.Entity;

namespace ApiShort.DAL.EFDbModel
{
    public partial class ShortDB : DbContext
    {
        public ShortDB() : base("name=ShortDB") { }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Facility> Facilities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(t => t.Facilities)
                .WithRequired(t => t.Project);

            modelBuilder.Entity<Facility>()
                .HasMany(t => t.ChildFacilities)
                .WithMany(t => t.ParentFacilities)
                .Map(t => t
                    .MapRightKey("ChildId")
                    .MapLeftKey("ParentId")
                    .ToTable("ChildParentFacilities"));
        }
    }
}
