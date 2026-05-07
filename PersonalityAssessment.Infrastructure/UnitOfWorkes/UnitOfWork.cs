using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PersonalityAssessment.Core.UnitOfWork;
using PersonalityAssessment.Infrastructure.Data;

namespace PersonalityAssessment.Infrastructure.UnitOfWorkes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;


        private IDbContextTransaction _transaction;
        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;


        }
        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }


        public void Rollback()
        {
            _transaction?.Rollback();
        }

        public async Task<int> SaveChangesAsync()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Deleted)
                {
                    var hasIsDeleted = entry.Properties
                                    .Any(p => p.Metadata.Name == "IsDeleted");

                    if (hasIsDeleted)
                    {
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                    }
                }
            }
            return await _context.SaveChangesAsync();

        }
    }
}
