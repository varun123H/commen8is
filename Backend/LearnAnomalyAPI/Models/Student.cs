using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LearnAnomalyAPI.Models;

public class Student
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("studentId")]
    public string StudentId { get; set; } = string.Empty;

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("enrollmentDate")]
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

    [BsonElement("averageScore")]
    public double AverageScore { get; set; } = 0;

    [BsonElement("isAnomaly")]
    public bool IsAnomaly { get; set; } = false;

    [BsonElement("lastUpdated")]
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
