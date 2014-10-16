var shorthornApp = angular.module('shorthornApp', ['ngRoute']);

/*** Route Configuration ***/
shorthornApp.config(function ($routeProvider) {
    $routeProvider.when('/', {
        templateUrl: '/Home/Home',
        controller: 'homeController'
    }).when('/contact', {
        templateUrl: '/Home/Contact',
        controller: 'contactController'
    }).when('/register', {
        templateUrl: '/Users/Register'
    }).when('/login', {
        templateUrl: '/Users/Login'
    });
});

/*** App Event handling ***/
shorthornApp.run(function ($rootScope) {
    $rootScope.isLoggedIn = false;
    if (GetLoginToken() != null) {
        $rootScope.isLoggedIn = true;
    }

    $rootScope.$on('$routeChangeSuccess', function (ev, data) {
        $rootScope.isNonApplicationController = false;

        if (data.$$route && (data.$$route.controller == 'homeController')) {
            $rootScope.showHomeHeader = true;
        }
        else {
            $rootScope.showHomeHeader = false;
        }
    })
});