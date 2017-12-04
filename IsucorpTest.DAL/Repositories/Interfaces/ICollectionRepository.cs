using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IsucorpTest.Model.DBModel;
using System.Data.Entity;

namespace IsucorpTest.DAL.Repositories.Interfaces
{
    public interface ICollectionRepository<T> where T : BaseEntity
    {
        IEnumerable<T> List(Expression<Func<T, bool>> filter = null, bool? track = null);
        int Add(T entity);
        IEnumerable<T> AddRange(ICollection<T> entities);
        void Delete(int id, bool soft = true);
        void DeleteRange(IEnumerable<T> entities);
        T FindById(int Id, bool? track = null);
        void Update(T entity);
        void UpdateEntity(T entity, T modifiedEntity);
        void SaveChanges();
        List<T> GetAllEntities();
        List<T> Include(string entity);
        T FirstOrDefault(int idEntity);
        bool ExecuteStoredProcedure(string stroredProcedure, params object[] parameters);
    }
}
