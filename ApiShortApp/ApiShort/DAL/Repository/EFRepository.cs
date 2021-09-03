using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace ApiShort.DAL.Repository
{
    public abstract class EFRepository<TDbContext, T> : IRepository<T>
        where T : class
        where TDbContext : DbContext
    {
        protected readonly TDbContext db;

        public EFRepository(TDbContext db)
        {
            this.db = db;
        }

        public abstract DbSet<T> GetEntities(TDbContext db);

        public virtual void Create(T item)
        {
            GetEntities(db).Add(item);
        }

        public virtual void Delete(int id)
        {
            T project = Get(id);
            GetEntities(db).Remove(project);
        }

        public virtual T Get(int id)
        {
            return GetEntities(db).Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return GetEntities(db).ToList();
        }

        public IEnumerable<T> GetAllInclude<TProperty>(Expression<Func<T, TProperty>> getProperty)
        {
            return GetEntities(db)
                .Include(getProperty)
                .ToList();
        }

        public virtual void Save()
        {
            db.SaveChanges();
        }

        public virtual void Update(T item)
        {
            db.Set<T>().AddOrUpdate(item);
            //db.Entry(item).State = EntityState.Modified;
        }

        public virtual void Dispose()
        {
            db?.Dispose();
        }
    }
}