using ApiShort.DAL.EFDbModel;
using System.Data.Entity;

namespace ApiShort.DAL.Repository
{
    public class ProjectEFRepository : EFRepository<ShortDB, Project>
    {
        public ProjectEFRepository(ShortDB db) : base(db) { }

        public override DbSet<Project> GetEntities(ShortDB db) => db.Projects;
    }
}