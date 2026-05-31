using LearnAnomalyAPI.Models;
using MongoDB.Driver;

namespace LearnAnomalyAPI.Services;

/// <summary>
/// AI-Powered Anomaly Detection Service
/// Uses statistical analysis and pattern recognition to detect unusual student behaviors
/// </summary>
public class AnomalyDetectionService
{
    private readonly IMongoCollection<Student> _students;
    private readonly IMongoCollection<LearningSession> _sessions;
    private readonly ILogger<AnomalyDetectionService> _logger;

    public AnomalyDetectionService(IMongoDatabase database, ILogger<AnomalyDetectionService> logger)
    {
        _students = database.GetCollection<Student>("Students");
        _sessions = database.GetCollection<LearningSession>("LearningSessions");
        _logger = logger;
    }

    /// <summary>
    /// Analyzes student and session data to detect anomalies
    /// Returns anomaly alerts for suspicious patterns
    /// </summary>
    public async Task<List<AnomalyAlert>> DetectAnomaliesAsync()
    {
        var alerts = new List<AnomalyAlert>();
        var students = await _students.Find(s => true).ToListAsync();

        foreach (var student in students)
        {
            var sessions = await _sessions
                .Find(s => s.StudentId == student.StudentId)
                .SortByDescending(s => s.SessionDate)
                .Limit(20)
                .ToListAsync();

            if (sessions.Count < 5) continue;

            // Check for performance anomalies
            var performanceAlert = DetectPerformanceAnomaly(student, sessions);
            if (performanceAlert != null)
                alerts.Add(performanceAlert);

            // Check for behavior anomalies
            var behaviorAlert = DetectBehaviorAnomaly(student, sessions);
            if (behaviorAlert != null)
                alerts.Add(behaviorAlert);

            // Check for suspicious activity patterns
            var activityAlert = DetectActivityAnomaly(student, sessions);
            if (activityAlert != null)
                alerts.Add(activityAlert);
        }

        return alerts;
    }

    /// <summary>
    /// Performance Anomaly: Sudden drops in scores using Z-score method
    /// </summary>
    private AnomalyAlert? DetectPerformanceAnomaly(Student student, List<LearningSession> sessions)
    {
        var scores = sessions.Select(s => s.Score).ToList();
        
        if (scores.Count < 5) return null;

        var mean = scores.Average();
        var stdDev = Math.Sqrt(scores.Average(x => Math.Pow(x - mean, 2)));

        // Z-score threshold
        var recentScores = scores.Take(5).ToList();
        var zScores = recentScores.Select(s => Math.Abs((s - mean) / (stdDev + 0.0001))).ToList();

        if (zScores.Average() > 2.5) // Significant deviation
        {
            var scoreDrop = mean - recentScores.Average();
            return new AnomalyAlert
            {
                StudentId = student.StudentId,
                StudentName = student.Name,
                AnomalyType = "Performance Drop",
                Description = $"Significant performance decline detected. Average drop: {scoreDrop:F2} points",
                AnomalyScore = Math.Min(zScores.Average(), 10.0),
                DetectedAt = DateTime.UtcNow
            };
        }

        return null;
    }

    /// <summary>
    /// Behavior Anomaly: Unusual study patterns (duration, frequency)
    /// </summary>
    private AnomalyAlert? DetectBehaviorAnomaly(Student student, List<LearningSession> sessions)
    {
        var durations = sessions.Select(s => s.DurationMinutes).ToList();
        var mean = durations.Average();
        var stdDev = Math.Sqrt(durations.Average(x => Math.Pow(x - mean, 2)));

        var recentDurations = durations.Take(3).ToList();
        var anomalySeverity = recentDurations.Count(d => Math.Abs(d - mean) > 2 * stdDev);

        // Check for extremely short sessions (possible login/logout without work)
        if (recentDurations.Any(d => d < 2))
        {
            return new AnomalyAlert
            {
                StudentId = student.StudentId,
                StudentName = student.Name,
                AnomalyType = "Irregular Study Pattern",
                Description = "Unusually short study sessions detected - possible account sharing or unusual behavior",
                AnomalyScore = 7.5,
                DetectedAt = DateTime.UtcNow
            };
        }

        // Check for erratic study durations
        if (anomalySeverity >= 2 && stdDev > 0)
        {
            return new AnomalyAlert
            {
                StudentId = student.StudentId,
                StudentName = student.Name,
                AnomalyType = "Study Duration Volatility",
                Description = $"Inconsistent study duration detected. Expected ~{mean:F0} mins, got high variance",
                AnomalyScore = 6.0,
                DetectedAt = DateTime.UtcNow
            };
        }

        return null;
    }

    /// <summary>
    /// Activity Anomaly: Unusual access patterns (time, frequency, attempt counts)
    /// </summary>
    private AnomalyAlert? DetectActivityAnomaly(Student student, List<LearningSession> sessions)
    {
        var recentSessions = sessions.Take(10).ToList();
        var attemptCounts = recentSessions.Select(s => s.AttemptCount).ToList();

        // Detect unusually high attempt counts (possible cheating/struggling)
        var avgAttempts = attemptCounts.Average();
        var highAttempts = attemptCounts.Count(a => a > avgAttempts * 2);

        if (highAttempts >= 3)
        {
            return new AnomalyAlert
            {
                StudentId = student.StudentId,
                StudentName = student.Name,
                AnomalyType = "High Attempt Rate",
                Description = $"Excessive retry attempts detected ({avgAttempts:F1} avg). May indicate struggling or unusual behavior",
                AnomalyScore = 6.5,
                DetectedAt = DateTime.UtcNow
            };
        }

        // Check for unusual access frequency
        var dates = recentSessions.Select(s => s.SessionDate.Date).Distinct().Count();
        var daySpan = (recentSessions.First().SessionDate.Date - recentSessions.Last().SessionDate.Date).Days + 1;

        if (daySpan > 0 && dates == daySpan) // Every single day
        {
            return new AnomalyAlert
            {
                StudentId = student.StudentId,
                StudentName = student.Name,
                AnomalyType = "Unusual Access Frequency",
                Description = "Student logged in every single day with very short sessions - possible automated access",
                AnomalyScore = 8.0,
                DetectedAt = DateTime.UtcNow
            };
        }

        return null;
    }

    /// <summary>
    /// Get anomaly statistics for dashboard
    /// </summary>
    public async Task<Dictionary<string, object>> GetAnomalyStatisticsAsync()
    {
        var alerts = await DetectAnomaliesAsync();
        var students = await _students.Find(s => true).ToListAsync();

        var stats = new Dictionary<string, object>
        {
            { "totalStudents", students.Count },
            { "anomalousStudents", students.Count(s => s.IsAnomaly) },
            { "totalAlerts", alerts.Count },
            { "alertsByType", alerts.GroupBy(a => a.AnomalyType).Select(g => new { type = g.Key, count = g.Count() }).ToList() },
            { "averageAnomalyScore", alerts.Any() ? alerts.Average(a => a.AnomalyScore) : 0 },
            { "lastDetectionTime", DateTime.UtcNow }
        };

        return stats;
    }
}
