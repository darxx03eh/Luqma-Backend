using Luqma.Data.Entities;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Luqma.Infrastructure.Repositories
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        private readonly LuqmaDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public FeedbackRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<(string, IList<int>?)> GetCustomerFeedbacksAsync(int customerId)
        {
            var customerFeedbacks = await GetTableNoTracking().Where(feedback => feedback.CustomerId.Equals(customerId))
                                    .Select(feedback => feedback.Id).ToListAsync();

            if (customerFeedbacks.Any())
                return ("FeedbacksForCustomerFound", customerFeedbacks);

            return ("FeedbacksForCustomerNotFound", null);
        }
    }
}
