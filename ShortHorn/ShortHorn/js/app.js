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
    }).when('/logout', {
        controller: 'usersController',
        templateUrl: '/Users/Login'
    }).when('/lists/', {
        templateUrl: '/ShortHorn/',
    }).when('/profile', {
        controller: 'usersController',
        templateUrl: '/Users/Profile'
    });
});

/*** App Event handling ***/
shorthornApp.run(function ($rootScope) {
    $rootScope.isLoggedIn = false;
    $rootScope.isTodoListsView = false;

    $rootScope.$on('$routeChangeSuccess', function (ev, data) {
        if (GetLoginToken() != null) {
            $rootScope.isLoggedIn = true;
        }

        $rootScope.isNonApplicationController = false;

        if (data.$$route && (data.$$route.controller == 'homeController')) {
            $rootScope.showHomeHeader = true;
        }
        else {
            $rootScope.showHomeHeader = false;
        }
    })
});