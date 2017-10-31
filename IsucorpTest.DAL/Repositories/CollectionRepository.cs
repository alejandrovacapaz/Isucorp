﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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

        public int Add(T entity)
        {
            var e = dbset.Add(entity);
            SaveChanges();
            return e.Id;
        }

        public IEnumerable<T> AddRange(ICollection<T> entities)
        {
            var e = dbset.AddRange(entities);
            SaveChanges();
            return e;
        }

        private void Delete(T entity)
        {
            // If entity is detached, fetch atached entity from dbset.
            var e = _context.Entry(entity).State == EntityState.Detached ? FindById(entity.Id, true) : entity;
            dbset.Remove(e);
            SaveChanges();
        }

        public void Delete(int id, bool soft = true)
        {
            var entity = FindById(id, true);
            Delete(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            // Fetch attached entities which id is contained into entities to delete ids.
            dbset.RemoveRange(List(null, true).Where(e => entities.Select(c => c.Id).Contains(e.Id)));
            SaveChanges();
        }

        public T 
            FindById(int Id, bool? track = null)
        {
            return track == true ? dbset.FirstOrDefault(t => t.Id == Id) : dbset.AsNoTracking().FirstOrDefault(t => t.Id == Id);
        }

        public void Update(T entity)
        {

            var oldEntity = FindById(entity.Id);
            if (oldEntity != null)
            {
                entity.CreatedDate = oldEntity.CreatedDate;
                entity.CreatedByName = oldEntity.CreatedByName;
                entity.CreatedById = oldEntity.CreatedById;
            }
            dbset.AddOrUpdate(entity);
            SaveChanges();
        }

        public void UpdateEntity(T entity, T modifiedEntity)
        {
            _context.Entry(entity).CurrentValues.SetValues(modifiedEntity);
            _context.Entry(entity).State = EntityState.Modified;

            Update(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        
        public List<T> GetAllEntities()
        {
            return dbset.Where(x => x.Id > 0).AsNoTracking().ToList();
        }

        public List<T> Include (string entity)
        {            
             return dbset.Where(x => x.Id > 0).Include(entity).ToList();
        }

        public T FirstOrDefault(int idEntity)
        {
            return dbset.FirstOrDefault(x => x.Id == idEntity);
        }
    }
}