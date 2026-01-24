using Luqma.Data.Response.Feedbacks;

namespace Luqma.Service.Interfaces
{
    public interface IFeedbackService
    {
        public Task<(string, AddNewFeedbackResponse?, double?)> AddNewFeedbackAsync(int id, double stars, string content);

        public Task<(string, AddNewFeedbackResponse?, double?)> UpdateExistingFeedbackAsync(int feedbackId, double stars, string content);

        public Task<(string, GetFeedbacksForItemResponse?)> GetFeedbacksForItemAsync(int id, int pageNumber, int pageSize);

        public Task<(string, double?)> DeleteExistingFeedbackAsync(int feedbackId);
    }
}