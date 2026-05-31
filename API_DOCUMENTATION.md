# LearnAnomalyDetect - API Documentation

## Base URL
```
https://localhost:5000/api
```

## Authentication
Currently, the API is open without authentication. In production, implement JWT Bearer token authentication.

---

## Students Endpoints

### Get All Students
```http
GET /students
```

**Response:**
```json
[
  {
    "id": "507f1f77bcf86cd799439011",
    "studentId": "STU001",
    "name": "Alice Johnson",
    "email": "alice@university.edu",
    "enrollmentDate": "2026-01-15T10:00:00Z",
    "averageScore": 85.5,
    "isAnomaly": false,
    "lastUpdated": "2026-05-31T14:30:00Z"
  }
]
```

**Status Codes:**
- `200 OK` - Successfully retrieved students
- `500 Internal Server Error` - Server error

---

### Get Student by ID
```http
GET /students/{studentId}
```

**Parameters:**
- `studentId` (string, required) - The student ID (e.g., "STU001")

**Response:**
```json
{
  "id": "507f1f77bcf86cd799439011",
  "studentId": "STU001",
  "name": "Alice Johnson",
  "email": "alice@university.edu",
  "enrollmentDate": "2026-01-15T10:00:00Z",
  "averageScore": 85.5,
  "isAnomaly": false,
  "lastUpdated": "2026-05-31T14:30:00Z"
}
```

**Status Codes:**
- `200 OK` - Student found
- `404 Not Found` - Student not found
- `500 Internal Server Error` - Server error

---

### Create Student
```http
POST /students
Content-Type: application/json
```

**Request Body:**
```json
{
  "studentId": "STU006",
  "name": "Frank Wilson",
  "email": "frank@university.edu",
  "averageScore": 75.0
}
```

**Response:**
```json
{
  "id": "507f1f77bcf86cd799439012",
  "studentId": "STU006",
  "name": "Frank Wilson",
  "email": "frank@university.edu",
  "enrollmentDate": "2026-05-31T15:00:00Z",
  "averageScore": 75.0,
  "isAnomaly": false,
  "lastUpdated": "2026-05-31T15:00:00Z"
}
```

**Status Codes:**
- `201 Created` - Student created successfully
- `400 Bad Request` - Invalid input
- `500 Internal Server Error` - Server error

---

### Update Student
```http
PUT /students/{studentId}
Content-Type: application/json
```

**Parameters:**
- `studentId` (string, required) - The student ID to update

**Request Body:**
```json
{
  "studentId": "STU001",
  "name": "Alice Johnson",
  "email": "alice.johnson@university.edu",
  "enrollmentDate": "2026-01-15T10:00:00Z",
  "averageScore": 87.5,
  "isAnomaly": false
}
```

**Status Codes:**
- `204 No Content` - Student updated successfully
- `404 Not Found` - Student not found
- `400 Bad Request` - Invalid input
- `500 Internal Server Error` - Server error

---

## Learning Sessions Endpoints

### Get All Sessions
```http
GET /learningsessions
```

**Query Parameters (Optional):**
- `limit` (integer) - Limit number of results
- `skip` (integer) - Skip number of results

**Response:**
```json
[
  {
    "id": "507f1f77bcf86cd799439021",
    "studentId": "STU001",
    "courseId": "COURSE101",
    "sessionDate": "2026-05-31T10:00:00Z",
    "durationMinutes": 45,
    "score": 88,
    "attemptCount": 2,
    "topicsRevised": ["Arrays", "Functions"],
    "isAnomaly": false,
    "anomalyReason": ""
  }
]
```

**Status Codes:**
- `200 OK` - Successfully retrieved sessions
- `500 Internal Server Error` - Server error

---

### Get Student Sessions
```http
GET /learningsessions/student/{studentId}
```

**Parameters:**
- `studentId` (string, required) - The student ID

**Response:**
```json
[
  {
    "id": "507f1f77bcf86cd799439021",
    "studentId": "STU001",
    "courseId": "COURSE101",
    "sessionDate": "2026-05-31T10:00:00Z",
    "durationMinutes": 45,
    "score": 88,
    "attemptCount": 2,
    "topicsRevised": ["Arrays", "Functions"],
    "isAnomaly": false,
    "anomalyReason": ""
  }
]
```

**Status Codes:**
- `200 OK` - Sessions found
- `404 Not Found` - Student not found
- `500 Internal Server Error` - Server error

---

### Create Learning Session
```http
POST /learningsessions
Content-Type: application/json
```

**Request Body:**
```json
{
  "studentId": "STU001",
  "courseId": "COURSE101",
  "durationMinutes": 50,
  "score": 92,
  "attemptCount": 1,
  "topicsRevised": ["Loops", "Recursion"]
}
```

**Response:**
```json
{
  "id": "507f1f77bcf86cd799439022",
  "studentId": "STU001",
  "courseId": "COURSE101",
  "sessionDate": "2026-05-31T15:30:00Z",
  "durationMinutes": 50,
  "score": 92,
  "attemptCount": 1,
  "topicsRevised": ["Loops", "Recursion"],
  "isAnomaly": false,
  "anomalyReason": ""
}
```

**Status Codes:**
- `201 Created` - Session created successfully
- `400 Bad Request` - Invalid input
- `500 Internal Server Error` - Server error

---

## Anomaly Detection Endpoints

### Analyze Anomalies
```http
GET /anomalydetection/analyze
```

**Description:** Runs real-time anomaly detection analysis on all students

**Response:**
```json
{
  "alerts": [
    {
      "studentId": "STU005",
      "studentName": "Eve Davis",
      "detectedAt": "2026-05-31T15:45:00Z",
      "anomalyType": "Performance Drop",
      "description": "Significant performance decline detected. Average drop: 45.30 points",
      "anomalyScore": 8.5,
      "resolved": false
    }
  ],
  "detectedAt": "2026-05-31T15:45:00Z"
}
```

**Status Codes:**
- `200 OK` - Analysis completed
- `500 Internal Server Error` - Server error

---

### Get Anomaly Statistics
```http
GET /anomalydetection/statistics
```

**Description:** Get aggregated anomaly statistics and insights

**Response:**
```json
{
  "totalStudents": 5,
  "anomalousStudents": 1,
  "totalAlerts": 3,
  "alertsByType": [
    {
      "type": "Performance Drop",
      "count": 1
    },
    {
      "type": "Irregular Study Pattern",
      "count": 1
    },
    {
      "type": "Activity Anomaly",
      "count": 1
    }
  ],
  "averageAnomalyScore": 7.33,
  "lastDetectionTime": "2026-05-31T15:45:00Z"
}
```

**Status Codes:**
- `200 OK` - Statistics retrieved
- `500 Internal Server Error` - Server error

---

## Real-time WebSocket API (SignalR)

### Connection
```javascript
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:5000/anomalyHub")
    .withAutomaticReconnect()
    .build();

connection.start();
```

### Events

#### Receive: AnomalyDetected
Fired when anomalies are detected

```javascript
connection.on("AnomalyDetected", (alerts) => {
    console.log("Anomalies detected:", alerts);
    // Update UI with new alerts
});
```

#### Receive: NewAnomalyAlert
Fired for each new anomaly alert

```javascript
connection.on("NewAnomalyAlert", (alert) => {
    console.log("New alert:", alert);
    // {
    //   "studentId": "STU005",
    //   "studentName": "Eve Davis",
    //   "anomalyType": "Performance Drop",
    //   "description": "...",
    //   "timestamp": "2026-05-31T15:45:00Z"
    // }
});
```

#### Send: RequestAnomalyAnalysis
Request server to run anomaly analysis

```javascript
connection.invoke("RequestAnomalyAnalysis")
    .catch(err => console.error(err));
```

#### Receive: AnalysisRequested
Confirmation that analysis was requested

```javascript
connection.on("AnalysisRequested", (timestamp) => {
    console.log("Analysis started at:", timestamp);
});
```

---

## Error Responses

All endpoints return error responses in the following format:

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "StudentId": ["The StudentId field is required."]
  }
}
```

### Common Error Codes:
- `400 Bad Request` - Invalid request format or validation error
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server-side error
- `503 Service Unavailable` - Service temporarily unavailable

---

## Rate Limiting

Currently not implemented. For production, consider implementing:
- Request rate limiting (e.g., 100 requests per minute per IP)
- Anomaly analysis throttling (e.g., once per 30 seconds)

---

## CORS

The API allows requests from:
- `http://localhost:3000`
- `http://localhost:8080`

Update CORS policy in `Program.cs` for other origins.

---

## Examples

### Using cURL

**Get all students:**
```bash
curl -X GET https://localhost:5000/api/students
```

**Create a new student:**
```bash
curl -X POST https://localhost:5000/api/students \
  -H "Content-Type: application/json" \
  -d '{
    "studentId": "STU010",
    "name": "Grace Lee",
    "email": "grace@university.edu",
    "averageScore": 80.0
  }'
```

**Analyze anomalies:**
```bash
curl -X GET https://localhost:5000/api/anomalydetection/analyze
```

### Using JavaScript (Fetch)

```javascript
// Get all students
const response = await fetch('https://localhost:5000/api/students');
const students = await response.json();
console.log(students);

// Create new session
const session = {
    studentId: "STU001",
    courseId: "COURSE101",
    durationMinutes: 45,
    score: 88,
    attemptCount: 2,
    topicsRevised: ["Arrays", "Functions"]
};

const response = await fetch('https://localhost:5000/api/learningsessions', {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json'
    },
    body: JSON.stringify(session)
});

const createdSession = await response.json();
console.log('Session created:', createdSession);
```

---

## API Changelog

### Version 1.0 (2026-05-31)
- Initial release
- Core student management endpoints
- Learning session tracking
- Anomaly detection with 3 detection methods
- SignalR real-time communication

---

## Contact & Support

For API issues or questions:
- Check DEVELOPMENT.md for architecture details
- Review example implementations in Frontend code
- Check application logs for detailed errors
