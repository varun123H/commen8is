# LearnAnomalyDetect - Development Guide

## Architecture Overview

This project follows a **Clean Architecture** pattern with clear separation of concerns:

```
Presentation Layer (AngularJS Frontend)
         ↓↓↓
API Layer (ASP.NET Core REST API)
         ↓↓↓
Business Logic (Services & AI/ML)
         ↓↓↓
Data Access Layer (MongoDB)
```

---

## Backend Architecture

### Models Layer
- **Student.cs** - Student entity with performance metrics
- **LearningSession.cs** - Individual session data
- **AnomalyAlert.cs** - Anomaly detection results

### Services Layer (Business Logic)
- **StudentService** - CRUD operations, data management
- **LearningSessionService** - Session tracking, performance calculations
- **AnomalyDetectionService** - AI/ML anomaly detection algorithms

### Controllers Layer (REST API)
- **StudentsController** - Manage student data
- **AnomalyController** - Learning session endpoints  
- **AnomalyDetectionController** - Real-time anomaly analysis

### SignalR Hub
- **AnomalyHub** - Real-time WebSocket communication

### Configuration
- **Program.cs** - Dependency injection, middleware setup
- **appsettings.json** - Configuration settings

---

## Frontend Architecture

### App Structure
- **app.js** - AngularJS app initialization, routing
- **services/api.service.js** - HTTP client and API communication
- **controllers/main.controller.js** - View logic and state management
- **views/** - HTML templates for each page

### Key Controllers
1. **DashboardController** - Real-time metrics and alerts
2. **StudentsController** - Student list and management
3. **StudentDetailController** - Individual student profiles
4. **AnomaliesController** - Anomaly list and filtering

---

## AI/ML Algorithm Details

### Anomaly Detection Methods

#### Method 1: Z-Score Analysis (Performance)
```csharp
// Detects sudden performance drops
double zScore = (currentScore - mean) / stdDev;
if (Math.Abs(zScore) > 2.5) // Alert threshold
{
    // Performance anomaly detected
}
```

#### Method 2: Behavior Pattern Detection
```csharp
// Detects unusual study patterns
- Session duration < 2 minutes → Suspicious
- High variance in session durations → Irregular
- Every day access → Possible automation
```

#### Method 3: Activity Analysis
```csharp
// Detects suspicious attempts
- Attempt count > 2x average → High attempt rate
- Unusual access frequency → Pattern anomaly
- Multiple short sessions → Batch anomaly
```

---

## Real-time Features

### SignalR Integration
```javascript
// Client-side: Listen for real-time updates
connection.on('NewAnomalyAlert', function(alert) {
    // Update UI in real-time
    $scope.anomalyAlerts.unshift(alert);
});

// Server-side: Broadcast anomalies
await _hubContext.Clients.All.SendAsync("AnomalyDetected", alerts);
```

---

## Database Schema

### Students Collection
```json
{
  "_id": ObjectId,
  "studentId": "STU001",
  "name": "Alice Johnson",
  "email": "alice@university.edu",
  "enrollmentDate": ISODate,
  "averageScore": 85.5,
  "isAnomaly": false,
  "lastUpdated": ISODate
}
```

### LearningSession Collection
```json
{
  "_id": ObjectId,
  "studentId": "STU001",
  "courseId": "COURSE101",
  "sessionDate": ISODate,
  "durationMinutes": 45,
  "score": 88,
  "attemptCount": 2,
  "topicsRevised": ["Arrays", "Functions"],
  "isAnomaly": false,
  "anomalyReason": ""
}
```

---

## Extending the Project

### Adding a New Anomaly Detection Type

1. **Create a detection method in AnomalyDetectionService:**
```csharp
private AnomalyAlert? DetectNewAnomaly(Student student, List<LearningSession> sessions)
{
    // Your logic here
    if (conditionMet)
    {
        return new AnomalyAlert
        {
            AnomalyType = "Your Type",
            Description = "Description",
            AnomalyScore = score
        };
    }
    return null;
}
```

2. **Call it from DetectAnomaliesAsync():**
```csharp
var newAlert = DetectNewAnomaly(student, sessions);
if (newAlert != null)
    alerts.Add(newAlert);
```

3. **Update the frontend filter** to show the new type.

### Adding a New API Endpoint

1. **Create a controller method:**
```csharp
[HttpGet("new-endpoint")]
public async Task<IActionResult> NewEndpoint()
{
    // Your logic
    return Ok(result);
}
```

2. **Add service method if needed:**
```csharp
public async Task<Data> GetNewDataAsync()
{
    // Business logic
}
```

3. **Update frontend ApiService:**
```javascript
newEndpoint: function() {
    return $http.get(API_BASE + '/controller/new-endpoint')
        .then(response => response.data);
}
```

### Adding a New Page/View

1. **Create HTML template in `views/`:**
```html
<div class="container">
    <!-- Your content -->
</div>
```

2. **Add route in `app.js`:**
```javascript
.when('/new-page', {
    templateUrl: 'views/new-page.html',
    controller: 'NewPageController'
})
```

3. **Create controller in `main.controller.js`:**
```javascript
learnAnomalyApp.controller('NewPageController', 
    ['$scope', 'ApiService', function($scope, ApiService) {
        // Controller logic
    }
]);
```

---

## Testing Guide

### Backend Unit Tests (Recommended)
```bash
# Create test project
dotnet new xunit -n LearnAnomalyAPI.Tests

# Add test for AnomalyDetectionService
# Test each detection method independently
```

### Frontend Testing
```bash
# Use Jasmine for AngularJS testing
npm install --save-dev jasmine-core karma karma-jasmine
```

### Integration Testing
- Test API endpoints with Postman
- Test real-time communication with browser DevTools

---

## Performance Optimization

1. **Database Indexing:**
```javascript
db.LearningSessions.createIndex({ "studentId": 1, "sessionDate": -1 })
db.Students.createIndex({ "studentId": 1 })
```

2. **Caching:**
- Cache student lists for 1 minute
- Cache anomaly statistics for 30 seconds

3. **Pagination:**
- Limit sessions returned (use skip/take)
- Implement cursor-based pagination

---

## Security Considerations

1. Add Authentication (JWT tokens)
2. Implement Authorization (role-based access)
3. Validate all input data
4. Use HTTPS in production
5. Implement rate limiting
6. Add CSRF protection

---

## Deployment

### Azure App Service
```bash
# Publish backend
dotnet publish -c Release

# Deploy to Azure App Service
# Update MongoDB connection string
```

### Docker Deployment
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY bin/Release/net6.0/publish/ /app/
ENTRYPOINT ["dotnet", "LearnAnomalyAPI.dll"]
```

---

## Troubleshooting

### MongoDB Connection Error
- Ensure MongoDB is running: `mongod`
- Check connection string in `appsettings.json`
- Verify firewall isn't blocking port 27017

### CORS Error
- Check CORS policy in `Program.cs`
- Verify frontend and API are on allowed origins

### SignalR Connection Issues
- Check WebSocket support in network
- Verify firewall allows WebSocket connections
- Check browser console for specific errors

---

## Resources

- **Microsoft .NET Documentation:** https://docs.microsoft.com/en-us/dotnet/
- **AngularJS Guide:** https://angularjs.org/
- **MongoDB Manual:** https://docs.mongodb.com/manual/
- **SignalR Documentation:** https://docs.microsoft.com/en-us/aspnet/signalr/
- **Bootstrap Documentation:** https://getbootstrap.com/
- **Chart.js:** https://www.chartjs.org/
