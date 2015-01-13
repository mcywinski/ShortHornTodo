shorthornApp.controller('usersController', function ($scope, $http, $location) {
    $scope.loginData = {};
    $scope.registerData = {};
    $scope.changeData = {};
    $scope.statusUserOperation = 0;
    $scope.messageStatusUserOperation = '';

    /*
    Status codes:
        1 - Success (login)
        2 - Success (register)
        3 - Success (save properties)
        ================================
        4 - Error (login)
        5 - Error (register)
        6 - Error (save properties)

    */

    $scope.executeLogin = function () {
        $http.post('/api/users/login', {
            login: $scope.loginData.login,
            password: $scope.loginData.password
        }).success(function (data, status) {
            SetLoginToken(data.token);
            $scope.loginData = {};
            $location.path('/');
            $scope.statusUserOperation = 1; // Success (login)
            $scope.messageStatusUserOperation = 'Login succeed!';
        }).error(function (data, status) {
            $scope.statusUserOperation = 4; // Error (login)
            $scope.messageStatusUserOperation = 'Error while singing in. Refresh the page and try again.';
        });
    };

    $scope.executeRegister = function () {
        //TODO Walidacja
        $http.post('/api/users/register', {
            login: $scope.registerData.login,
            password: $scope.registerData.password,
            passwordConfirmed: $scope.registerData.passwordConfirmed,
            email: $scope.registerData.email,
            emailConfirmed: $scope.registerData.emailConfirmed
        }).success(function (data, status) {
            $scope.registerData = {};
            $scope.statusUserOperation = 2; // Success (register)
            $scope.messageStatusUserOperation = 'Register succeed! Please check your email.';
        }).error(function () {
            $scope.statusUserOperation = 5; // Error (register)
            $scope.messageStatusUserOperation = 'Error! Register failed. Refresh the page and try again.';
        });

        $scope.$on('$viewContentLoaded', function () {
            if ($location.absUrl().indexOf('logout') > -1) {
                DeleteLoginToken();
                $location.path('/');
            }
        });
    };

        $scope.executeUserPropertiesSave = function () {
            $http.put('/api/users', {
                token: GetLoginToken(),
                login: $scope.changeData.login,
                email: $scope.changeData.email,
                city: $scope.changeData.city,
                country: $scope.changeData.country
            }).success(function (data, status) {
                $scope.statusUserOperation = 3; //Success (save properties)
                $scope.messageStatusUserOperation = 'Properties successfully saved!';
            }).error(function (data, status) {
                $scope.statusUserOperation = 6; //Error (save properties)
                $scope.messageStatusUserOperation = 'Error while saving your properties. Refresh the page and try again.';
            });
        };
    });