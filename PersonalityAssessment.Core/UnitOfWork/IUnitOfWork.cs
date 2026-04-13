namespace PersonalityAssessment.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
