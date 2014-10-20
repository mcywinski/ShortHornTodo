shorthornApp.controller('todoListsController', function ($scope, $http, $location) {
    $scope.todoLists = [];
    $scope.selectedTodoList = {};

    $scope.toggleTodoList = function (id) {
        for (var i = 0; i < $scope.todoLists.length; i++) {
            if ($scope.todoLists[i].id == id) {
                $scope.selectedTodoList = $scope.todoLists[i];
                break;
            }
        }
    };

    $http.get('/api/lists?token=' + GetLoginToken()).success(function (data, status) {
        $scope.todoLists = data;
    }).error(function (data, status) {
        alert('Error while fetching todo lists. Refresh the page and try again.');
    });
});