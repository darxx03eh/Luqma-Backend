namespace Luqma.Data.Entities
{
    public class Prediction
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public DateTime PredictionDate { get; set; } = DateTime.UtcNow;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public double PredictedQuantity { get; set; }
        public double ConfidenceScore { get; set; }
        public virtual MenuItem? MenuItem { get; set; }

    }
}
