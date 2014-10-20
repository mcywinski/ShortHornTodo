shorthornApp.controller('todoListsController', function ($scope, $http, $location) {
    $scope.todoLists = [];

    $http.get('/api/lists?token=' + GetLoginToken()).success(function (data, status) {
        $scope.todoLists = data;
    }).error(function (data, status) {
        alert('Error while fetching todo lists. Refresh the page and try again.');
    });
});