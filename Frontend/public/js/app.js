// Main AngularJS Application
var learnAnomalyApp = angular.module('learnAnomalyApp', ['ngRoute', 'ngResource']);

// Configure routes
learnAnomalyApp.config(['$routeProvider', function($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'views/dashboard.html',
            controller: 'DashboardController'
        })
        .when('/students', {
            templateUrl: 'views/students.html',
            controller: 'StudentsController'
        })
        .when('/student/:studentId', {
            templateUrl: 'views/student-detail.html',
            controller: 'StudentDetailController'
        })
        .when('/anomalies', {
            templateUrl: 'views/anomalies.html',
            controller: 'AnomaliesController'
        })
        .otherwise({
            redirectTo: '/'
        });
}]);

// Global HTTP interceptor for API calls
learnAnomalyApp.factory('ApiInterceptor', ['$q', function($q) {
    return {
        request: function(config) {
            config.headers = config.headers || {};
            return config;
        },
        responseError: function(response) {
            console.error('API Error:', response);
            return $q.reject(response);
        }
    };
}]);

learnAnomalyApp.config(['$httpProvider', function($httpProvider) {
    $httpProvider.interceptors.push('ApiInterceptor');
}]);
