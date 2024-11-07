using Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcoPower_Logistics.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SuperStoreContext _context;
        public GenericRepository(SuperStoreContext context)
        {
            _context = context;
        }
        //Add an entity to table
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        //Add a range of entities to table
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }
        //Find the Entity return true or false
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        //Return all the entities found
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        //Return the entity of specified ID
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        //Remove an entity
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        //Remove range of entities
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        //Save changes
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        //Save Changes using .NET
        public void Save()
        {
            _context.SaveChangesAsync();
        }
        //Update the entity
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        //Return all entities of type T
        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
        //Return list of entities specified
        public IEnumerable<T> Includes(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query.ToList(); // Convert IQueryable<T> to IEnumerable<T>
        }
    }
}
