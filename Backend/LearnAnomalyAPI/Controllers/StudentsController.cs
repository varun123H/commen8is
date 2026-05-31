using Microsoft.AspNetCore.Mvc;
using LearnAnomalyAPI.Models;
using LearnAnomalyAPI.Services;

namespace LearnAnomalyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly StudentService _studentService;

    public StudentsController(StudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Student>>> GetAllStudents()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }

    [HttpGet("{studentId}")]
    public async Task<ActionResult<Student>> GetStudent(string studentId)
    {
        var student = await _studentService.GetStudentByIdAsync(studentId);
        if (student == null)
            return NotFound();
        return Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent(Student student)
    {
        var createdStudent = await _studentService.CreateStudentAsync(student);
        return CreatedAtAction(nameof(GetStudent), new { studentId = createdStudent.StudentId }, createdStudent);
    }

    [HttpPut("{studentId}")]
    public async Task<IActionResult> UpdateStudent(string studentId, Student student)
    {
        var existing = await _studentService.GetStudentByIdAsync(studentId);
        if (existing == null)
            return NotFound();

        student.StudentId = studentId;
        await _studentService.UpdateStudentAsync(student);
        return NoContent();
    }
}
