using Microsoft.EntityFrameworkCore;
using University.Core.Interfaces;
using University.Data.Contexts;

namespace University.Data.Repositories
{
    //This implementation of a generic repository pattern allows for CRUD operations on any entity type.
    //The interface IGenericRepository<T> is in the University.Core.Interfaces namespace.
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly UniversityDbContext _context;
        protected readonly DbSet<T> _set;

        public GenericRepository(UniversityDbContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }

        public T GetById(int id) => _set.Find(id);
        public List<T> GetAll() => _set.ToList();
        public void Add(T entity) { _set.Add(entity); _context.SaveChanges(); }
        public void Update(T entity) { _set.Update(entity); _context.SaveChanges(); }
        public void Delete(T entity) { _set.Remove(entity); _context.SaveChanges(); }
        public void SaveChanges() => _context.SaveChanges();
    }
}
