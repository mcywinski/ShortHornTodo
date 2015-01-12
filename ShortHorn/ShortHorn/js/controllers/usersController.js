shorthornApp.controller('usersController', function ($scope, $http, $location) {
    $scope.loginData = {};
    $scope.registerData = {};
    $scope.changeData = {};
    $scope.currentUser = {};

    $scope.executeLogin = function () {
        $http.post('/api/users/login', {
            login: $scope.loginData.login,
            password: $scope.loginData.password
        }).success(function (data, status) {
            SetLoginToken(data.token);
            $scope.loginData = {};
            $location.path('/');
            //TODO alert z sukcesem logowania
        }).error(function (data, status) {
            alert('Problem w trakcie logowania!');
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
            $location.path('/');
            //TODO alert z sukcesem rejestracji
        }).error(function () {
            alert('Problem w trakcie rejestracji!');
        });
    };

    $scope.$on('$viewContentLoaded', function () {
        if ($location.absUrl().indexOf('logout') > -1) {
            DeleteLoginToken();
            $location.path('/');
        }
    });

    $scope.executeUserPropertiesSave = function (item) {
        $http.put('/api/users', {
            token: GetLoginToken(),
            login: $scope.changeData.login,
            email: $scope.changeData.email,
            city: $scope.changeData.city,
            country: $scope.changeData.country
        }).success(function (data, status) {
           
        }).error(function (data, status) {
            alert('Problem with updating the item. Try again');
        });
    };

});