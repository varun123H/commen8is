# 🤖 LearnAnomalyDetect - AI-Powered Student Performance Analytics Platform

## Overview

LearnAnomalyDetect is a production-ready, full-stack application that leverages AI/ML and real-time technology to detect unusual student learning patterns. This project is perfect for portfolios, interviews, and resume building.

**Tech Stack:**
- **Frontend:** AngularJS 1.8.2, Bootstrap 5, Chart.js
- **Backend:** C# .NET 6+, ASP.NET Core REST API
- **Database:** MongoDB (NoSQL)
- **Real-time:** SignalR WebSockets
- **AI/ML:** Anomaly Detection Algorithm (Isolation Forest concepts)

---

## 🌟 Key Features

### 1. **Real-time Anomaly Detection**
- Detects unusual student behaviors using statistical analysis
- Three types of anomalies:
  - **Performance Drops:** Z-score based detection
  - **Irregular Study Patterns:** Unusual session durations
  - **Activity Anomalies:** Suspicious access patterns

### 2. **Live Dashboard**
- Real-time metrics and KPIs
- Interactive charts (Plotly/Chart.js)
- SignalR-powered live updates
- Anomaly statistics and trends

### 3. **Student Management**
- CRUD operations for students
- Performance tracking
- Learning session logs
- Individual student dashboards

### 4. **Anomaly Alerts**
- Real-time notifications
- Severity scoring (1-10)
- Detailed descriptions
- Resolvable alerts

---

## 📁 Project Structure

```
LearnAnomalyDetect/
├── Backend/
│   └── LearnAnomalyAPI/
│       ├── Models/
│       │   ├── Student.cs
│       │   ├── LearningSession.cs
│       │   └── AnomalyAlert.cs
│       ├── Services/
│       │   ├── StudentService.cs
│       │   ├── LearningSessionService.cs
│       │   └── AnomalyDetectionService.cs (AI/ML)
│       ├── Controllers/
│       │   ├── StudentsController.cs
│       │   ├── AnomalyController.cs
│       │   └── AnomalyDetectionController.cs
│       ├── Hubs/
│       │   └── AnomalyHub.cs (SignalR)
│       ├── Program.cs
│       ├── appsettings.json
│       └── LearnAnomalyAPI.csproj
├── Frontend/
│   └── public/
│       ├── index.html
│       ├── css/
│       │   ├── bootstrap.min.css
│       │   └── style.css
│       ├── js/
│       │   ├── app.js
│       │   ├── controllers/
│       │   │   └── main.controller.js
│       │   └── services/
│       │       └── api.service.js
│       └── views/
│           ├── dashboard.html
│           ├── students.html
│           ├── student-detail.html
│           └── anomalies.html
```

---

## 🚀 Getting Started

### Prerequisites
- .NET 6+ SDK
- Node.js (for local server)
- MongoDB (local or Atlas)
- Visual Studio or VS Code

### Backend Setup

1. **Navigate to backend:**
   ```bash
   cd Backend/LearnAnomalyAPI
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Update MongoDB connection string** in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "MongoDB": "mongodb://localhost:27017"
     }
   }
   ```

4. **Run the API:**
   ```bash
   dotnet run
   ```
   API will start at `https://localhost:5000`

### Frontend Setup

1. **Navigate to frontend:**
   ```bash
   cd Frontend
   ```

2. **Install simple HTTP server:**
   ```bash
   npm install -g http-server
   ```

3. **Start the server:**
   ```bash
   http-server -p 8080
   ```
   Frontend will be available at `http://localhost:8080`

### Database Setup

1. **Local MongoDB:**
   ```bash
   # Install MongoDB Community Edition
   # Start MongoDB service
   mongod
   ```

2. **Or use MongoDB Atlas (Cloud):**
   - Create cluster at https://www.mongodb.com/cloud/atlas
   - Update connection string in `appsettings.json`

---

## 🧠 AI/ML Implementation

### Anomaly Detection Algorithm

The `AnomalyDetectionService` implements multiple statistical techniques:

#### 1. **Z-Score Based Performance Analysis**
```
Z-Score = (Score - Mean) / StandardDeviation
Alert if |Z-Score| > 2.5
```

#### 2. **Behavior Pattern Analysis**
- Detects unusually short sessions (< 2 minutes)
- Identifies erratic study duration variance
- Flags inconsistent access patterns

#### 3. **Activity Anomaly Detection**
- High retry attempt counts
- Unusual access frequency (every single day)
- Abnormal session characteristics

### Example Anomaly Scenarios:
- **Student A:** Score drops from 85 to 45 → Performance Drop Alert
- **Student B:** 2-minute sessions daily → Automated Access Alert
- **Student C:** 10+ attempts per test → High Attempt Rate Alert

---

## 🔌 API Endpoints

### Students
- `GET /api/students` - Get all students
- `GET /api/students/{id}` - Get student by ID
- `POST /api/students` - Create new student
- `PUT /api/students/{id}` - Update student

### Learning Sessions
- `GET /api/learningsessions` - Get all sessions
- `GET /api/learningsessions/student/{studentId}` - Get student sessions
- `POST /api/learningsessions` - Create session

### Anomaly Detection
- `GET /api/anomalydetection/analyze` - Run analysis
- `GET /api/anomalydetection/statistics` - Get statistics

### SignalR Hub
- **Endpoint:** `ws://localhost:5000/anomalyHub`
- **Events:**
  - `AnomalyDetected` - New anomalies detected
  - `NewAnomalyAlert` - Real-time alert notification

---

## 📊 Dashboard Views

### 1. **Dashboard (Home)**
- Key metrics cards
- Anomaly distribution pie chart
- Performance trend line chart
- Real-time anomaly alerts

### 2. **Students**
- Student list with filtering
- Add new student form
- Quick status indicators
- Links to detailed profiles

### 3. **Student Detail**
- Individual student profile
- Score trend chart
- Learning session history
- Performance metrics

### 4. **Anomalies**
- Comprehensive anomaly list
- Filter by type
- Severity scoring visualization
- Mark as resolved
- Direct links to student profiles

---

## 💡 Resume Highlights

This project demonstrates:

✅ **Full-Stack Development**
- Backend: C# .NET API with RESTful design
- Frontend: AngularJS with modern UI patterns
- Database: MongoDB integration and aggregations

✅ **AI/Machine Learning**
- Statistical anomaly detection
- Real-world pattern recognition
- Data analysis and insights

✅ **Real-time Technology**
- WebSocket integration (SignalR)
- Live dashboard updates
- Event-driven architecture

✅ **Best Practices**
- SOLID principles
- Dependency injection
- Service-oriented architecture
- Proper error handling
- API documentation

✅ **Production-Ready**
- CORS enabled
- Database seeding
- Configuration management
- Logging and monitoring ready

---

## 🔧 Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://localhost:27017"
  },
  "AnomalyDetection": {
    "ContaminationFactor": 0.05,
    "MinSamplesRequired": 10
  }
}
```

---

## 📈 Sample Data

On startup, the application seeds 5 sample students:
- **STU001-004:** Normal students with good performance
- **STU005:** Anomalous student with low average score

---

## 🎯 Use Cases & Improvements

### Current Implementation:
- Student performance anomaly detection
- Real-time dashboard monitoring
- Learning behavior analysis

### Future Enhancements:
1. Advanced ML models (Neural Networks)
2. Predictive analytics
3. Teacher dashboard and notifications
4. Mobile app (React Native)
5. Advanced reporting and analytics
6. User authentication and authorization
7. Email/SMS alerts
8. Data export (PDF/Excel)

---

## 📝 Technology Highlights for Resume

**Backend Skills:**
- C# .NET 6+ development
- RESTful API design
- MongoDB (NoSQL) database
- SignalR real-time communication
- Dependency injection and middleware
- Async/await patterns

**Frontend Skills:**
- AngularJS framework
- SPA (Single Page Application)
- Responsive Bootstrap design
- Chart.js data visualization
- HTTP client integration
- WebSocket client

**AI/ML Skills:**
- Statistical analysis
- Anomaly detection algorithms
- Data pattern recognition
- Performance metrics analysis

---

## 🚨 Notes

- Ensure MongoDB is running before starting the API
- API runs on port 5000, Frontend on 8080
- CORS is configured to allow both servers
- Sample data is seeded automatically on first run

---

## 📞 Support

For questions or improvements, refer to:
- Microsoft .NET Docs: https://docs.microsoft.com/dotnet/
- AngularJS Documentation: https://angularjs.org/
- MongoDB Docs: https://docs.mongodb.com/
- SignalR Docs: https://docs.microsoft.com/aspnet/signalr/

---

**Built with ❤️ for your resume and portfolio**

*LearnAnomalyDetect - Making student learning smarter with AI*

