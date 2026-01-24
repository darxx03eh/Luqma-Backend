using Luqma.Data.Wrappers;

namespace Luqma.Data.Response.Feedbacks
{
    public class GetFeedbacksForItemResponse
    {
        public int Id { get; set; }
        public int TotalReviews { get; set; }
        public double AverageRating { get; set; }
        public int ReviewsForThisMonth { get; set; }
        public PaginatedResult<GetCustomerFeedback> CustomersFeedback { get; set; }
    }

    public class GetCustomerFeedback
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int FeedbackId { get; set; }
        public double Stars { get; set; }
        public string Content { get; set; }
        public string Since { get; set; }
    }
}