using Humanizer;
using Luqma.Data.Entities;
using Luqma.Data.Response.Feedbacks;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Luqma.Service.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork unitOfWork;

        public FeedbackService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<(string, GetFeedbacksForItemResponse?)> GetFeedbacksForItemAsync(int id, int pageNumber, int pageSize)
        {
            var item = await unitOfWork.MenuItemRepository.GetByIdAsync(id);
            if (item is null)
                return ("ItemNotFound", null);

            var (result, customersFeedbacks) = await unitOfWork.MenuItemRepository.GetItemFeedbacksAsync(id, pageNumber, pageSize);
            if (result.Equals("NoFeedbacksFoundForItem"))
                return ("NoFeedbacksFoundForItem", null);

            var date = DateTime.UtcNow;
            var feedbacks = new GetFeedbacksForItemResponse()
            {
                Id = item.Id,
                TotalReviews = item.Feedbacks.Where(feedback => feedback.ItemId.Equals(item.Id)).Count(),
                AverageRating = item.TotalStars,
                ReviewsForThisMonth = item.Feedbacks.Where(
                    feedback => feedback.ItemId.Equals(item.Id)
                    && feedback.CreatedAt.Month.Equals(date.Month)
                    && feedback.CreatedAt.Year.Equals(date.Year)
                ).Count(),
                CustomersFeedback = customersFeedbacks
            };
            return ("FeedbacksFoundForItem", feedbacks);
        }

        public async Task<(string, double?)> DeleteExistingFeedbackAsync(int feedbackId)
        {
            var customerId = unitOfWork.CustomerRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(customerId))
                return ("CustomerNotFound", null);

            var customer = await unitOfWork.CustomerRepository.GetByIdAsync(Convert.ToInt32(customerId));
            if (customer is null)
                return ("CustomerNotFound", null);

            var feedback = await unitOfWork.FeedbackRepository.GetByIdAsync(feedbackId);
            if (feedback is null)
                return ("FeedbackNotFound", null);

            var (response, customerFeedbacks) = await unitOfWork.FeedbackRepository.GetCustomerFeedbacksAsync(customer.Id);
            if (response.Equals("FeedbacksForCustomerNotFound"))
                return ("FeedbacksForCustomerNotFound", null);

            var isFeedbackForCustomer = customerFeedbacks.Any(cf => cf.Equals(feedback.Id));
            if (!isFeedbackForCustomer)
                return ("ThisFeedbackDoNotBelongToThisCustomer", null);

            var result = await unitOfWork.FeedbackRepository.DeleteAsync(feedback);
            var (updateTotalStarsResult, totalStars) = await UpdateTotalStarsAsync(feedback.ItemId);
            if (updateTotalStarsResult.Equals("ItemNotFound")) return ("ItemNotFound", null);
            else if (updateTotalStarsResult.Equals("AnErrorOccurredWhileUpdatingTheTotalStars"))
                return ("AnErrorOccurredWhileUpdatingTheTotalStars", null);
            return result <= 0 ? ("AnErrorOccurredWhileDeletingFeedback", null) : ("TheFeedbackWasSuccessfullyDeleted", totalStars);
        }

        public async Task<(string, AddNewFeedbackResponse?, double?)> UpdateExistingFeedbackAsync(int feedbackId, double stars, string content)
        {
            var customerId = unitOfWork.CustomerRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(customerId))
                return ("CustomerNotFound", null, null);

            var customer = await unitOfWork.CustomerRepository.GetByIdAsync(Convert.ToInt32(customerId));
            if (customer is null)
                return ("CustomerNotFound", null, null);

            var feedback = await unitOfWork.FeedbackRepository.GetByIdAsync(feedbackId);
            if (feedback is null)
                return ("FeedbackNotFound", null, null);

            var (response, customerFeedbacks) = await unitOfWork.FeedbackRepository.GetCustomerFeedbacksAsync(customer.Id);
            if (response.Equals("FeedbacksForCustomerNotFound"))
                return ("FeedbacksForCustomerNotFound", null, null);

            var isFeedbackForCustomer = customerFeedbacks.Any(cf => cf.Equals(feedback.Id));
            if (!isFeedbackForCustomer)
                return ("ThisFeedbackDoNotBelongToThisCustomer", null, null);

            feedback.Stars = stars;
            feedback.Content = content;
            feedback.UpdatedAt = DateTime.UtcNow;

            var result = await unitOfWork.FeedbackRepository.UpdateAsync(feedback);
            var (updateTotalStarsResult, totalStars) = await UpdateTotalStarsAsync(feedback.ItemId);
            if (updateTotalStarsResult.Equals("ItemNotFound")) return ("ItemNotFound", null, null);
            else if (updateTotalStarsResult.Equals("AnErrorOccurredWhileUpdatingTheTotalStars"))
                return ("AnErrorOccurredWhileUpdatingTheTotalStars", null, null);

            return result <= 0 ? ("AnErrorOccurredWhileUpdatingFeedback.", null, null) : ("TheFeedbackWasSuccessfullyUpdated", new AddNewFeedbackResponse
            {
                Id = feedback.Id,
                Customer = new CustomerFeedback()
                {
                    Id = feedback.Customer.Id,
                    FirstName = feedback.Customer.FirstName,
                    LastName = feedback.Customer.LastName
                },
                Stars = stars,
                Content = content,
                Since = feedback.UpdatedAt.Humanize()
            }, totalStars);
        }

        public async Task<(string, AddNewFeedbackResponse?, double?)> AddNewFeedbackAsync(int id, double stars, string content)
        {
            var customerId = unitOfWork.CustomerRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(customerId))
                return ("CustomerNotFound", null, null);

            var customer = await unitOfWork.CustomerRepository.GetByIdAsync(Convert.ToInt32(customerId));
            if (customer is null)
                return ("CustomerNotFound", null, null);

            var item = await unitOfWork.MenuItemRepository.GetByIdAsync(id);
            if (item is null)
                return ("ItemNotFound", null, null);

            var orders = await unitOfWork.OrderRepository.GetTableNoTracking()
                         .Where(order => order.CustomerId.Equals(customer.Id)).ToListAsync();

            if (orders is null || !orders.Any())
                return ("YouHaveNoOrders", null, null);

            var hasItem = orders.Any(order => order.OrderItems.Any(item => item.MenuItem.Id.Equals(id)));
            if (!hasItem)
                return ("YouHaveNoOrdersWithThisItem", null, null);

            var existingFeedback = await unitOfWork.FeedbackRepository.GetTableNoTracking()
                                   .Where(feedback => feedback.CustomerId.Equals(customer.Id)
                                   && feedback.ItemId.Equals(id)
                                   ).ToListAsync();

            if (existingFeedback.Any())
                return ("YouAlreadyRatedThisItem", null, null);

            var feedback = new Feedback()
            {
                CustomerId = customer.Id,
                ItemId = id,
                Stars = stars,
                Content = content,
            };

            var result = await unitOfWork.FeedbackRepository.AddAsync(feedback);
            if (result is null)
                return ("AnErrorOccurredWhileAddingTheFeedback", null, null);

            var (updateTotalStarsResult, totalStars) = await UpdateTotalStarsAsync(id);
            if (updateTotalStarsResult.Equals("ItemNotFound")) return ("ItemNotFound", null, null);
            else if (updateTotalStarsResult.Equals("AnErrorOccurredWhileUpdatingTheTotalStars"))
                return ("AnErrorOccurredWhileUpdatingTheTotalStars", null, null);

            return ("FeedbackAddedSuccessfully", new AddNewFeedbackResponse()
            {
                Id = result.Id,
                Customer = new CustomerFeedback()
                {
                    Id = result.Customer.Id,
                    FirstName = result.Customer.FirstName,
                    LastName = result.Customer.LastName,
                },
                Stars = stars,
                Content = content,
                Since = result.UpdatedAt.Humanize()
            }, totalStars);
        }

        private async Task<(string, double?)> UpdateTotalStarsAsync(int itemId)
        {
            var item = await unitOfWork.MenuItemRepository.GetByIdAsync(itemId);
            if (item is null)
                return ("ItemNotFound", null);

            var feedbacks = await unitOfWork.FeedbackRepository.GetTableNoTracking()
                            .Where(feedback => feedback.ItemId.Equals(itemId)).ToListAsync();

            if (feedbacks.Any())
            {
                item.TotalStars = feedbacks.Average(feedback => feedback.Stars);
                var result = await unitOfWork.MenuItemRepository.UpdateAsync(item);
                return result <= 0 ? ("AnErrorOccurredWhileUpdatingTheTotalStars", null)
                                   : ("TotalStarsHaveBeenUpdatedSuccessfully", item.TotalStars);
            }
            item.TotalStars = 0;
            int response = await unitOfWork.MenuItemRepository.UpdateAsync(item);
            return response <= 0 ? ("AnErrorOccurredWhileUpdatingTheTotalStars", null)
                                   : ("TotalStarsHaveBeenUpdatedSuccessfully", item.TotalStars);
        }
    }
}