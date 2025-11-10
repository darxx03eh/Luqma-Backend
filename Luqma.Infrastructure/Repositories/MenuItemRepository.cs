using Humanizer;
using Luqma.Data.Entities;
using Luqma.Data.Response.Feedbacks;
using Luqma.Data.Wrappers;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Luqma.Infrastructure.Repositories
{
    public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
    {

        private readonly LuqmaDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MenuItemRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<(string, PaginatedResult<GetCustomerFeedback>?)> GetItemFeedbacksAsync(int id, int pageNumber, int pageSize)
        {
            var feedbacksQueryable = _context.Feedbacks.Where(feedback => feedback.ItemId.Equals(id))
                            .AsNoTracking().AsQueryable();

            if (feedbacksQueryable is null || !feedbacksQueryable.Any())
                return ("NoFeedbacksFoundForItem", null);

            var customersFeedbacksPaginated = await feedbacksQueryable.ToPaginatedListAsync(pageNumber, pageSize);

            var customerFeedbacks = customersFeedbacksPaginated.Data.Select(feedback => new GetCustomerFeedback()
            {
                Id = feedback.CustomerId,
                FirstName = feedback.Customer.FirstName,
                LastName = feedback.Customer.LastName,
                Stars = feedback.Stars,
                Content = feedback.Content,
                Since = feedback.UpdatedAt.Humanize()
            }).ToList();

            var result = PaginatedResult<GetCustomerFeedback>.Success(
                customerFeedbacks,
                customersFeedbacksPaginated.TotalCount,
                customersFeedbacksPaginated.TotalPages,
                customersFeedbacksPaginated.PageSize
            );
            result.CurrentPage = customersFeedbacksPaginated.CurrentPage;
            if (result.Data.Count.Equals(0))
                return ("NoFeedbacksFoundForItem", null);

            return ("FeedbacksFoundForItem", result);
        }

        public async Task<bool> IsIdExistAsync(int id)
        {
            var menuitem = _context.MenuItems.FirstOrDefault(mi => mi.Id == id);
            if (menuitem is null) return false;
            return true;

        }
        public async Task<List<MenuItem>> GetAllMenuItemsAsync()
        {
            return  await _context.MenuItems.Include(m => m.CategoryItems).ThenInclude(ci => ci.Category).AsNoTracking().AsQueryable().ToListAsync();

        }
       
    }
}
