using Microsoft.AspNetCore.Mvc;
using LearnAnomalyAPI.Services;
using Microsoft.AspNetCore.SignalR;
using LearnAnomalyAPI.Hubs;

namespace LearnAnomalyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnomalyDetectionController : ControllerBase
{
    private readonly AnomalyDetectionService _anomalyService;
    private readonly IHubContext<AnomalyHub> _hubContext;

    public AnomalyDetectionController(AnomalyDetectionService anomalyService, IHubContext<AnomalyHub> hubContext)
    {
        _anomalyService = anomalyService;
        _hubContext = hubContext;
    }

    [HttpGet("analyze")]
    public async Task<IActionResult> AnalyzeAnomalies()
    {
        var alerts = await _anomalyService.DetectAnomaliesAsync();
        
        // Broadcast to all connected clients in real-time
        await _hubContext.Clients.All.SendAsync("AnomalyDetected", alerts);
        
        return Ok(new { alerts, detectedAt = DateTime.UtcNow });
    }

    [HttpGet("statistics")]
    public async Task<IActionResult> GetStatistics()
    {
        var stats = await _anomalyService.GetAnomalyStatisticsAsync();
        return Ok(stats);
    }
}
