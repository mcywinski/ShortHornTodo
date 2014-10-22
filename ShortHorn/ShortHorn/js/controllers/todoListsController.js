shorthornApp.controller('todoListsController', function ($scope, $http, $location) {
    $scope.todoLists = [];
    $scope.selectedTodoList = {};
    $scope.newListName = '';
    $scope.newItemName = '';

    $scope.toggleTodoList = function (id) {
        for (var i = 0; i < $scope.todoLists.length; i++) {
            if ($scope.todoLists[i].id == id) {
                $scope.selectedTodoList = $scope.todoLists[i];
                break;
            }
        }
    };

    $scope.executeCreateTodoList = function () {
        $http.post('/api/lists', {
            token: GetLoginToken(),
            name: $scope.newListName
        }).success(function (data, status) {
            $http.get('/api/lists?token=' + GetLoginToken()).success(function (data, status) {
                $scope.todoLists = data;
            }).error(function (data, status) {
                alert('Error while fetching todo lists. Refresh the page and try again.');
            });
        }).error(function (data, status) {
        });
        $scope.newListName = '';
    };

    $scope.executeCreateTodoListKeyPress = function (keyEvent) {
        if (keyEvent.which == 13){
            $scope.executeCreateTodoList();
        }
    };

    $scope.executeCreateTodoItem = function () {
        $http.post('/api/items', {
            token: GetLoginToken(),
            name: $scope.newItemName,
            parentListId: $scope.selectedTodoList.id
        }).success(function (data, status) {
            alert('success');
            //TODO handle success
        }).error(function (data, status) {
            alert('error');
            //TODO handle error
        });
    };

    $http.get('/api/lists?token=' + GetLoginToken()).success(function (data, status) {
        $scope.todoLists = data;
    }).error(function (data, status) {
        alert('Error while fetching todo lists. Refresh the page and try again.');
    });
});