namespace Luqma.Data.Response.Feedbacks
{
    public class AddNewFeedbackResponse
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public double Stars { get; set; }
        public CustomerFeedback Customer { get; set; }
        public string Since { get; set; }
    }
    public class CustomerFeedback
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
