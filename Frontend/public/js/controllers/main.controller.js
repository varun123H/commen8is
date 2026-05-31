// Dashboard Controller - Real-time analytics dashboard
learnAnomalyApp.controller('DashboardController', ['$scope', 'ApiService', '$interval',
    function($scope, ApiService, $interval) {
        $scope.loading = true;
        $scope.anomalyAlerts = [];
        $scope.statistics = {};

        // Initialize dashboard
        $scope.init = function() {
            // Load initial data
            loadStatistics();
            loadAnomalies();

            // Auto-refresh every 30 seconds
            $interval(loadStatistics, 30000);
        };

        function loadStatistics() {
            ApiService.getAnomalyStatistics().then(function(stats) {
                $scope.statistics = stats;
                $scope.anomalyPercentage = $scope.statistics.totalStudents > 0 
                    ? ($scope.statistics.anomalousStudents / $scope.statistics.totalStudents * 100).toFixed(2)
                    : 0;

                $scope.loading = false;
            }).catch(function(error) {
                console.error('Failed to load statistics:', error);
                $scope.loading = false;
            });
        }

        function loadAnomalies() {
            ApiService.analyzeAnomalies().then(function(data) {
                $scope.anomalyAlerts = data.alerts || [];
            }).catch(function(error) {
                console.error('Failed to load anomalies:', error);
            });
        }

        $scope.refreshAnomalies = function() {
            $scope.loading = true;
            ApiService.analyzeAnomalies().then(function(data) {
                $scope.anomalyAlerts = data.alerts || [];
                loadStatistics();
            }).catch(function(error) {
                console.error('Failed to refresh:', error);
                $scope.loading = false;
            });
        };

        $scope.dismissAlert = function(index) {
            $scope.anomalyAlerts.splice(index, 1);
        };

        // Initialize on load
        $scope.init();
    }
]);

// Students Controller - List and manage students
learnAnomalyApp.controller('StudentsController', ['$scope', 'ApiService',
    function($scope, ApiService) {
        $scope.loading = true;
        $scope.students = [];
        $scope.showForm = false;
        $scope.newStudent = {};

        $scope.loadStudents = function() {
            $scope.loading = true;
            ApiService.getAllStudents().then(function(students) {
                $scope.students = students;
                $scope.loading = false;
            }).catch(function(error) {
                console.error('Failed to load students:', error);
                $scope.loading = false;
            });
        };

        $scope.toggleForm = function() {
            $scope.showForm = !$scope.showForm;
            if (!$scope.showForm) {
                $scope.newStudent = {};
            }
        };

        $scope.addStudent = function() {
            if (!$scope.newStudent.studentId || !$scope.newStudent.name) {
                alert('Please fill required fields');
                return;
            }

            ApiService.createStudent($scope.newStudent).then(function(student) {
                $scope.students.push(student);
                $scope.newStudent = {};
                $scope.showForm = false;
                alert('Student added successfully!');
            }).catch(function(error) {
                console.error('Failed to add student:', error);
                alert('Failed to add student');
            });
        };

        $scope.deleteStudent = function(index) {
            if (confirm('Are you sure?')) {
                $scope.students.splice(index, 1);
            }
        };

        $scope.loadStudents();
    }
]);

// Student Detail Controller
learnAnomalyApp.controller('StudentDetailController', ['$scope', '$routeParams', 'ApiService',
    function($scope, $routeParams, ApiService) {
        $scope.loading = true;
        $scope.student = {};
        $scope.sessions = [];

        const studentId = $routeParams.studentId;

        $scope.loadStudent = function() {
            ApiService.getStudent(studentId).then(function(student) {
                $scope.student = student;
            });

            ApiService.getStudentSessions(studentId).then(function(sessions) {
                $scope.sessions = sessions;
                $scope.loading = false;
            }).catch(function(error) {
                console.error('Failed to load student data:', error);
                $scope.loading = false;
            });
        };

        $scope.loadStudent();
    }
]);

// Anomalies Controller
learnAnomalyApp.controller('AnomaliesController', ['$scope', 'ApiService',
    function($scope, ApiService) {
        $scope.loading = true;
        $scope.anomalies = [];
        $scope.filterType = 'All';
        $scope.anomalyTypes = [];

        $scope.loadAnomalies = function() {
            $scope.loading = true;
            ApiService.analyzeAnomalies().then(function(data) {
                $scope.anomalies = data.alerts || [];
                
                // Extract unique anomaly types
                $scope.anomalyTypes = ['All', ...new Set($scope.anomalies.map(a => a.anomalyType))];
                
                $scope.loading = false;
            }).catch(function(error) {
                console.error('Failed to load anomalies:', error);
                $scope.loading = false;
            });
        };

        $scope.filteredAnomalies = function() {
            if ($scope.filterType === 'All') {
                return $scope.anomalies;
            }
            return $scope.anomalies.filter(a => a.anomalyType === $scope.filterType);
        };

        $scope.resolveAnomaly = function(anomaly) {
            anomaly.resolved = true;
        };

        $scope.loadAnomalies();
    }
]);
