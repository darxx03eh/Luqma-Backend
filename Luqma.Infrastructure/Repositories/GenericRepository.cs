using Luqma.Data.Helpers;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.IdentityModel.Tokens.Jwt;

namespace Luqma.Infrastructure.Repositories
{
    public class GenericRepository<T>(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor) : IGenericRepository<T>
        where T : class
    {
        private readonly LuqmaDbContext context = context;
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
        public virtual async Task<ICollection<T>> GetAllAsync(bool withtrack=false)
        {
            if (withtrack)
            {
                return await context.Set<T>().ToListAsync();
            }
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }
      
        public virtual async Task<T> GetByIdAsync(Int32 id)
            => await context.Set<T>().FindAsync(id);
        public IQueryable<T> GetTableNoTracking()
            => context.Set<T>().AsNoTracking().AsQueryable();
        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            await context.Set<T>().AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }
        public virtual async Task<T> AddAsync(T entity)
        {
          
            await context.Set<T>().AddAsync(entity);

            await context.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<int> UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
            return await context.SaveChangesAsync();
        }
        public virtual async Task<int> DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            return await context.SaveChangesAsync();
        }
        public virtual async Task DeleteRangeAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
                context.Entry(entity).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
            => await context.SaveChangesAsync();
        public IDbContextTransaction BeginTransaction()
            => context.Database.BeginTransaction();
        public void Commit()
            => context.Database.CommitTransaction();
        public void RollBack() => context.Database.RollbackTransaction();
        public IQueryable<T> GetTableAsTracking()
            => context.Set<T>().AsQueryable();
        public virtual async Task UpdateRangeAsync(ICollection<T> entities)
        {
            context.Set<T>().UpdateRange(entities);
            await context.SaveChangesAsync();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
            => await context.Database.BeginTransactionAsync();

        public async Task CommitAsync()
            => await context.Database.CommitTransactionAsync();
        public async Task RollBackAsync()
            => await context.Database.RollbackTransactionAsync();
        public String? ExtractUserIdFromToken()
        {
            var authHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (String.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return null;
            var token = authHeader.Substring("Bearer ".Length);
            var handler = new JwtSecurityTokenHandler();
            try
            {

                var jwtToken = handler.ReadJwtToken(token);
                return jwtToken.Claims.FirstOrDefault(token => token.Type.Equals(nameof(UserClaimModel.Id)))?.Value;
            }
            catch
            {
                return null;
            }
        }
    
    }
}
