namespace LearnAnomalyAPI.Models;

public class AnomalyAlert
{
    public string StudentId { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public DateTime DetectedAt { get; set; } = DateTime.UtcNow;
    public string AnomalyType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double AnomalyScore { get; set; } = 0;
    public bool Resolved { get; set; } = false;
}
