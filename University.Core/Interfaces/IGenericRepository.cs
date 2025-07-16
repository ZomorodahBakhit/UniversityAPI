namespace University.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        List<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}
