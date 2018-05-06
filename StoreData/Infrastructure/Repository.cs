using StoreData.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreData.Infrastructure
{
    public abstract class Repository<T>: IRepository<T> where T:class
    {        
        private readonly StoreContext db;

        protected Repository(StoreContext context)
        {
            db = context;
        }

        public virtual void Create(T entity)
        {
            db.Set<T>().Add(entity);

        }

        public virtual void Delete(T entity)
        {
            db.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {


            //Присоединяет заданную сущность к контексту, поддерживающему данный набор. Это означает, 
            //что сущность помещается в контекст в неизмененном состоянии, как если бы она была считана из базы данных.
            db.Set<T>().Attach(entity);
            db.Entry(entity).State = EntityState.Modified;

        }

        public virtual T Get(int id)
        {
            return db.Set<T>().Find(id);
        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            return db.Set<T>().Where(where).FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return db.Set<T>().Where(where).ToList();
        }


    }
}
