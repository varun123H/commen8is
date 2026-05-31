// API Service for REST calls
learnAnomalyApp.factory('ApiService', ['$http', '$q', function($http, $q) {
    const API_BASE = 'http://localhost:5000/api';

    return {
        // Student endpoints
        getAllStudents: function() {
            return $http.get(API_BASE + '/students').then(response => response.data);
        },
        
        getStudent: function(studentId) {
            return $http.get(API_BASE + '/students/' + studentId).then(response => response.data);
        },
        
        createStudent: function(student) {
            return $http.post(API_BASE + '/students', student).then(response => response.data);
        },
        
        updateStudent: function(student) {
            return $http.put(API_BASE + '/students/' + student.studentId, student).then(response => response.data);
        },

        // Learning session endpoints
        getStudentSessions: function(studentId) {
            return $http.get(API_BASE + '/learningsessions/student/' + studentId).then(response => response.data);
        },
        
        getAllSessions: function() {
            return $http.get(API_BASE + '/learningsessions').then(response => response.data);
        },
        
        createSession: function(session) {
            return $http.post(API_BASE + '/learningsessions', session).then(response => response.data);
        },

        // Anomaly detection endpoints
        analyzeAnomalies: function() {
            return $http.get(API_BASE + '/anomalydetection/analyze').then(response => response.data);
        },
        
        getAnomalyStatistics: function() {
            return $http.get(API_BASE + '/anomalydetection/statistics').then(response => response.data);
        }
    };
}]);
