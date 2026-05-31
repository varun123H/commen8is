using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LearnAnomalyAPI.Models;

public class LearningSession
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("studentId")]
    public string StudentId { get; set; } = string.Empty;

    [BsonElement("courseId")]
    public string CourseId { get; set; } = string.Empty;

    [BsonElement("sessionDate")]
    public DateTime SessionDate { get; set; } = DateTime.UtcNow;

    [BsonElement("durationMinutes")]
    public int DurationMinutes { get; set; } = 0;

    [BsonElement("score")]
    public double Score { get; set; } = 0;

    [BsonElement("attemptCount")]
    public int AttemptCount { get; set; } = 1;

    [BsonElement("topicsRevised")]
    public List<string> TopicsRevised { get; set; } = new();

    [BsonElement("isAnomaly")]
    public bool IsAnomaly { get; set; } = false;

    [BsonElement("anomalyReason")]
    public string AnomalyReason { get; set; } = string.Empty;
}
