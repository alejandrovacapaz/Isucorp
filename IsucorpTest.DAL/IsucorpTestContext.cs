using System;
using IsucorpTest.Model.DBModel;
using System.Data.Entity;
using System.Linq;

namespace IsucorpTest.DAL
{
    public class IsucorpTestContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public IsucorpTestContext() : base("name=IsucorpTest.DAL.IsucorpTestContext")
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }
          
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var userName = "";
            var userId = "";

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
