# LearnAnomalyDetect - Quick Start Guide

Get **LearnAnomalyDetect** running in 5 minutes!

---

## ⚡ Prerequisites

Before starting, ensure you have:

- ✅ **.NET 6+ SDK** - [Download](https://dotnet.microsoft.com/en-us/download/dotnet)
- ✅ **Node.js** - [Download](https://nodejs.org/)
- ✅ **MongoDB** - [Download Community Edition](https://www.mongodb.com/try/download/community) OR use [MongoDB Atlas](https://www.mongodb.com/cloud/atlas) (Cloud)
- ✅ **Git** - [Download](https://git-scm.com/)

---

## 🚀 Quick Start (Windows)

### Step 1: Clone & Navigate
```bash
cd path/to/your/projects
git clone <repository-url>
cd commen8is
```

### Step 2: Start MongoDB
```bash
# Option 1: Local MongoDB
mongod

# Option 2: MongoDB Atlas (Cloud)
# Update connection string in Backend/LearnAnomalyAPI/appsettings.json
# "MongoDB": "mongodb+srv://username:password@cluster.mongodb.net"
```

### Step 3: Run Backend
```bash
cd Backend/LearnAnomalyAPI
dotnet restore
dotnet run
```
✅ API starts at `https://localhost:5000`

### Step 4: Run Frontend (New Terminal)
```bash
cd Frontend

# Install http-server if needed
npm install -g http-server

# Start server
http-server -p 8080
```
✅ Frontend available at `http://localhost:8080`

### Step 5: Access Application
Open browser and go to:
```
http://localhost:8080
```

---

## 🚀 Quick Start (Mac/Linux)

```bash
# 1. Navigate to project
cd path/to/your/projects/commen8is

# 2. Start MongoDB
mongod

# 3. Terminal 1: Backend
cd Backend/LearnAnomalyAPI
dotnet restore
dotnet run

# 4. Terminal 2: Frontend
cd Frontend
npm install -g http-server
http-server -p 8080

# 5. Open browser
# Visit http://localhost:8080
```

---

## 🎯 First Steps in App

### 1. **View Dashboard**
- Metrics cards showing student overview
- Real-time anomaly detection
- Performance charts
- Live alerts

### 2. **Browse Students**
- Click "Students" tab
- View pre-seeded students (STU001-STU005)
- Add new students
- Click student name for detailed profile

### 3. **Check Anomalies**
- Click "Anomalies" tab
- View detected anomalies
- Filter by type
- Mark as resolved
- View student profile from alert

### 4. **Add Test Data**
Create a new learning session:
```bash
curl -X POST http://localhost:5000/api/learningsessions \
  -H "Content-Type: application/json" \
  -d '{
    "studentId": "STU001",
    "courseId": "COURSE101",
    "durationMinutes": 45,
    "score": 92,
    "attemptCount": 1,
    "topicsRevised": ["Arrays", "Loops"]
  }'
```

---

## 📊 Troubleshooting

### Issue: "Cannot connect to MongoDB"
```
Solution:
1. Ensure MongoDB is running: mongod
2. Check connection string in appsettings.json
3. Verify port 27017 is available
```

### Issue: "Cannot reach API from frontend"
```
Solution:
1. Verify API is running on port 5000
2. Check CORS settings in Program.cs
3. Check browser console for error details
```

### Issue: "http-server command not found"
```
Solution:
npm install -g http-server
```

### Issue: ".NET SDK not found"
```
Solution:
Install from https://dotnet.microsoft.com/en-us/download/dotnet
```

---

## 📁 Project Structure at a Glance

```
commen8is/
├── Backend/LearnAnomalyAPI/          ← C# .NET API
│   ├── Models/                        ← Data models
│   ├── Services/                      ← Business logic & AI
│   ├── Controllers/                   ← REST endpoints
│   ├── Hubs/                          ← SignalR real-time
│   └── appsettings.json               ← Configuration
│
├── Frontend/public/                   ← AngularJS app
│   ├── js/                            ← Application code
│   ├── views/                         ← HTML templates
│   └── css/                           ← Styling
│
├── README.md                          ← Full documentation
├── API_DOCUMENTATION.md               ← API reference
├── DEVELOPMENT.md                     ← Dev guide
└── setup.bat/setup.sh                 ← Setup scripts
```

---

## 🔌 Key API Endpoints

**Students:**
```
GET    /api/students               → List all students
POST   /api/students               → Create student
GET    /api/students/{id}          → Get student details
PUT    /api/students/{id}          → Update student
```

**Learning Sessions:**
```
GET    /api/learningsessions               → All sessions
GET    /api/learningsessions/student/{id}  → Student sessions
POST   /api/learningsessions               → Create session
```

**Anomaly Detection:**
```
GET    /api/anomalydetection/analyze       → Detect anomalies
GET    /api/anomalydetection/statistics    → Get statistics
```

---

## 🧪 Test with Sample Data

The app comes pre-seeded with 5 students:

| ID    | Name           | Score | Status    |
|-------|----------------|-------|-----------|
| STU001| Alice Johnson  | 85.5  | Normal    |
| STU002| Bob Smith      | 78.2  | Normal    |
| STU003| Carol White    | 92.1  | Normal    |
| STU004| Diana Brown    | 88.7  | Normal    |
| STU005| Eve Davis      | 45.3  | **Anomaly**|

**Try:** Click "Anomalies" tab to see STU005 flagged as anomalous!

---

## 💡 What to Explore

### 1. **Frontend**
- Navigate between Dashboard, Students, Anomalies
- Add new students
- View student details and performance charts
- See real-time anomaly alerts

### 2. **Backend API**
- Test endpoints with Postman or curl
- Create test learning sessions
- Trigger anomaly detection
- Subscribe to SignalR updates

### 3. **AI/ML Engine**
- Check `AnomalyDetectionService.cs` for algorithms
- Understand Z-score based detection
- Review behavior pattern analysis
- See activity anomaly detection

---

## 🎓 Learning Resources

Included in project:
- ✅ **README.md** - Full project overview
- ✅ **API_DOCUMENTATION.md** - Complete API reference
- ✅ **DEVELOPMENT.md** - Architecture & extension guide
- ✅ **Well-commented code** - Throughout project

---

## 📝 Next Steps

1. **Explore the code** - Read through services and controllers
2. **Add features** - Implement authentication, more anomaly types
3. **Deploy** - Try deploying to Azure or Docker
4. **Customize** - Adapt for your use case
5. **Share** - Add to your portfolio!

---

## 🆘 Need Help?

### Check logs:
```bash
# Backend console shows all activity
# Frontend browser console (F12) shows errors
```

### Common fixes:
1. Restart services
2. Clear browser cache
3. Check MongoDB is running
4. Verify connection strings
5. Review error messages in console

### Still stuck?
- Check API_DOCUMENTATION.md for endpoint details
- Review DEVELOPMENT.md for architecture
- Check sample requests in API docs

---

## ✅ Success Checklist

- [ ] MongoDB running
- [ ] Backend started (`https://localhost:5000`)
- [ ] Frontend started (`http://localhost:8080`)
- [ ] Can see Dashboard with student metrics
- [ ] Can view pre-seeded students
- [ ] Can see anomalies detected
- [ ] Can add new student
- [ ] Can create learning session

If all checked ✅ - You're ready to go! 🚀

---

## 🎉 You're All Set!

Enjoy exploring **LearnAnomalyDetect**!

**Questions?** Check the documentation files or review the code comments.

**Ready to deploy?** See DEVELOPMENT.md for deployment options.

---

**Happy coding! 🚀**
