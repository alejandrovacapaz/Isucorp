using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using IsucorpTest.DAL.Repositories.Interfaces;
using IsucorpTest.Model.DBModel;

namespace IsucorpTest.DAL.Repositories
{
    public class CollectionRepository<T> : ICollectionRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> dbset;     

        public CollectionRepository(IsucorpTestContext context)
        {            
            _context = context;            
            dbset = _context.Set<T>();
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> filter = null, bool? track = null)
        {
            IQueryable<T> ret;
            if (track == true)
                ret = dbset;
            else
                ret = dbset.AsNoTracking();
            return filter == null ? ret : ret.Where(filter);
        }       

        public T FindById(int Id, bool? track = null)
        {
            return track == true ? dbset.FirstOrDefault(t => t.Id == Id) : dbset.AsNoTracking().FirstOrDefault(t => t.Id == Id);
        }       

        public bool ExecuteStoredProcedure(string stroredProcedure, params object[] parameters)
        {
            try
            {
                _context.Database.ExecuteSqlCommand(stroredProcedure, parameters);              
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
