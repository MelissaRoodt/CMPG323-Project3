using System.Linq.Expressions;

namespace EcoPower_Logistics.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        void SaveChanges();
        void Save();
        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> Includes(params Expression<Func<T, object>>[] includes);


    }

}
