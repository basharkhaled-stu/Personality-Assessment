namespace PersonalityAssessment.Core.Repository
{
    public interface IRepository<T> where T : class
    {

        IQueryable<T> GetAll();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<T?> GetByIdAsync(int id);


    }
}
