using Luqma.Data.Response.Feedbacks;

namespace Luqma.Service.Interfaces
{
    public interface IFeedbackService
    {
        public Task<(string, AddNewFeedbackResponse?, double?)> AddNewFeedbackAsync(int id, double stars, string content);
    }
}
