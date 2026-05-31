using MongoDB.Driver;
using LearnAnomalyAPI.Models;

namespace LearnAnomalyAPI.Services;

public class StudentService
{
    private readonly IMongoCollection<Student> _students;

    public StudentService(IMongoDatabase database)
    {
        _students = database.GetCollection<Student>("Students");
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _students.Find(s => true).ToListAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(string studentId)
    {
        return await _students.Find(s => s.StudentId == studentId).FirstOrDefaultAsync();
    }

    public async Task<Student> CreateStudentAsync(Student student)
    {
        student.EnrollmentDate = DateTime.UtcNow;
        student.LastUpdated = DateTime.UtcNow;
        await _students.InsertOneAsync(student);
        return student;
    }

    public async Task UpdateStudentAsync(Student student)
    {
        student.LastUpdated = DateTime.UtcNow;
        await _students.ReplaceOneAsync(s => s.StudentId == student.StudentId, student);
    }

    public async Task SeedSampleData()
    {
        var count = await _students.CountDocumentsAsync(s => true);
        if (count == 0)
        {
            var students = new List<Student>
            {
                new Student { StudentId = "STU001", Name = "Alice Johnson", Email = "alice@university.edu", AverageScore = 85.5 },
                new Student { StudentId = "STU002", Name = "Bob Smith", Email = "bob@university.edu", AverageScore = 78.2 },
                new Student { StudentId = "STU003", Name = "Carol White", Email = "carol@university.edu", AverageScore = 92.1 },
                new Student { StudentId = "STU004", Name = "Diana Brown", Email = "diana@university.edu", AverageScore = 88.7 },
                new Student { StudentId = "STU005", Name = "Eve Davis", Email = "eve@university.edu", AverageScore = 45.3 } // Anomaly case
            };

            await _students.InsertManyAsync(students);
        }
    }
}
