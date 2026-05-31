using Microsoft.AspNetCore.Mvc;
using LearnAnomalyAPI.Models;
using LearnAnomalyAPI.Services;

namespace LearnAnomalyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LearningSessionsController : ControllerBase
{
    private readonly LearningSessionService _sessionService;

    public LearningSessionsController(LearningSessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<LearningSession>>> GetAllSessions()
    {
        var sessions = await _sessionService.GetAllSessionsAsync();
        return Ok(sessions);
    }

    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<List<LearningSession>>> GetStudentSessions(string studentId)
    {
        var sessions = await _sessionService.GetSessionsByStudentAsync(studentId);
        return Ok(sessions);
    }

    [HttpPost]
    public async Task<ActionResult<LearningSession>> CreateSession(LearningSession session)
    {
        var createdSession = await _sessionService.CreateSessionAsync(session);
        return CreatedAtAction(nameof(GetStudentSessions), new { studentId = createdSession.StudentId }, createdSession);
    }
}
