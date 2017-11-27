using System;
using IsucorpTest.Model.DBModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;


namespace IsucorpTest.DAL
{
    public class IsucorpTestContext : IdentityDbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public IsucorpTestContext() : base("IsucorpTest.DAL.IsucorpTestContext")
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }
          
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var userName = HttpContext.Current?.User?.Identity.GetUserName();
            var userId = HttpContext.Current?.User?.Identity.GetUserId();

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedDate = DateTime.UtcNow;
                    ((BaseEntity)entity.Entity).CreatedByName = userName;
                    ((BaseEntity)entity.Entity).CreatedById = userId;
                }

                ((BaseEntity)entity.Entity).ModifiedDate = DateTime.UtcNow;
                ((BaseEntity)entity.Entity).ModifiedByName = userName;
                ((BaseEntity)entity.Entity).ModifiedById = userId;
            }
        }

    }
}
