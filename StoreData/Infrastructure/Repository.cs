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
            try
            {
                db.Set<T>().Add(entity);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public virtual void Delete(T entity)
        {
            try
            {
                db.Set<T>().Remove(entity);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                //Присоединяет заданную сущность к контексту, поддерживающему данный набор. Это означает, 
                //что сущность помещается в контекст в неизмененном состоянии, как если бы она была считана из базы данных.
                db.Set<T>().Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public virtual T Get(int id)
        {
            try
            {
                return db.Set<T>().Find(id);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            try
            {
                return db.Set<T>().Where(where).FirstOrDefault();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                return db.Set<T>().ToList();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            try
            {
                return db.Set<T>().Where(where).ToList();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }


    }
}
