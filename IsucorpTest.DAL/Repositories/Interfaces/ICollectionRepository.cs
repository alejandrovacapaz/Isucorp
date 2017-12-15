using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IsucorpTest.Model.DBModel;

namespace IsucorpTest.DAL.Repositories.Interfaces
{
    public interface ICollectionRepository<T> where T : BaseEntity
    {
        IEnumerable<T> List(Expression<Func<T, bool>> filter = null, bool? track = null);        
        T FindById(int Id, bool? track = null);       
        bool ExecuteStoredProcedure(string stroredProcedure, params object[] parameters);
    }
}
