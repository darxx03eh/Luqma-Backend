using Microsoft.EntityFrameworkCore.Storage;

namespace Luqma.Infrastructure.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task DeleteRangeAsync(ICollection<T> entities);
        public Task<T> GetByIdAsync(Int32 id);
        public Task SaveChangesAsync();
        public IDbContextTransaction BeginTransaction();
        public void Commit();
        public void RollBack();
        public IQueryable<T> GetTableNoTracking();
        public IQueryable<T> GetTableAsTracking();
        public Task<T> AddAsync(T entity);
        public Task AddRangeAsync(ICollection<T> entities);
        public Task<int> UpdateAsync(T entity);
        public Task UpdateRangeAsync(ICollection<T> entities);
        public Task<int> DeleteAsync(T entity);
        public Task<IDbContextTransaction> BeginTransactionAsync();
        public Task CommitAsync();
        public Task RollBackAsync();
        public String? ExtractUserIdFromToken();
        public Task<ICollection<T>> GetAllAsync();





    }
}
