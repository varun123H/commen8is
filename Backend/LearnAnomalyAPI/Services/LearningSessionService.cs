using MongoDB.Driver;
using LearnAnomalyAPI.Models;

namespace LearnAnomalyAPI.Services;

public class LearningSessionService
{
    private readonly IMongoCollection<LearningSession> _sessions;
    private readonly IMongoCollection<Student> _students;

    public LearningSessionService(IMongoDatabase database)
    {
        _sessions = database.GetCollection<LearningSession>("LearningSessions");
        _students = database.GetCollection<Student>("Students");
    }

    public async Task<List<LearningSession>> GetSessionsByStudentAsync(string studentId)
    {
        return await _sessions
            .Find(s => s.StudentId == studentId)
            .SortByDescending(s => s.SessionDate)
            .ToListAsync();
    }

    public async Task<LearningSession> CreateSessionAsync(LearningSession session)
    {
        session.SessionDate = DateTime.UtcNow;
        await _sessions.InsertOneAsync(session);
        
        // Update student average score
        await UpdateStudentAverageAsync(session.StudentId);
        
        return session;
    }

    public async Task<List<LearningSession>> GetAllSessionsAsync()
    {
        return await _sessions.Find(s => true).SortByDescending(s => s.SessionDate).ToListAsync();
    }

    private async Task UpdateStudentAverageAsync(string studentId)
    {
        var sessions = await GetSessionsByStudentAsync(studentId);
        if (sessions.Count > 0)
        {
            var avgScore = sessions.Average(s => s.Score);
            var student = await _students.Find(s => s.StudentId == studentId).FirstOrDefaultAsync();
            if (student != null)
            {
                student.AverageScore = avgScore;
                student.LastUpdated = DateTime.UtcNow;
                await _students.ReplaceOneAsync(s => s.StudentId == studentId, student);
            }
        }
    }
}
