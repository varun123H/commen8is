using Microsoft.AspNetCore.SignalR;

namespace LearnAnomalyAPI.Hubs;

/// <summary>
/// Real-time anomaly notification hub
/// Sends live updates to connected clients when anomalies are detected
/// </summary>
public class AnomalyHub : Hub
{
    private readonly ILogger<AnomalyHub> _logger;

    public AnomalyHub(ILogger<AnomalyHub> logger)
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation($"Client {Context.ConnectionId} connected to AnomalyHub");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation($"Client {Context.ConnectionId} disconnected from AnomalyHub");
        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// Receive anomaly detection trigger from client
    /// </summary>
    public async Task RequestAnomalyAnalysis()
    {
        _logger.LogInformation("Anomaly analysis requested");
        // Client will handle the actual API call
        await Clients.All.SendAsync("AnalysisRequested", DateTime.UtcNow);
    }

    /// <summary>
    /// Notify all clients of a detected anomaly
    /// </summary>
    public async Task BroadcastAnomalyAlert(string studentId, string studentName, string anomalyType, string description)
    {
        var alert = new
        {
            studentId,
            studentName,
            anomalyType,
            description,
            timestamp = DateTime.UtcNow
        };

        await Clients.All.SendAsync("NewAnomalyAlert", alert);
    }
}
