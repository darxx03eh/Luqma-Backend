using Luqma.Data.Entities;

namespace Luqma.Infrastructure.IRepositories
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        public Task<(string, IList<int>?)> GetCustomerFeedbacksAsync(int customerId);
    }
}
