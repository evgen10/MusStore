using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreData.Infrastructure.Interfaces
{
    public interface IRepository<T> where T: class
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

        T Get(int id);
        T Get(Expression<Func<T, bool>> where);


        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

    }
}
