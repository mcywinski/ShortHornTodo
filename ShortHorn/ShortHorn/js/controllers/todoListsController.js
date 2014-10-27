shorthornApp.controller('todoListsController', function ($scope, $http, $location) {
    $scope.todoLists = [];
    $scope.todoItems = [];
    $scope.selectedTodoList = {};
    $scope.selectedTodoItem = {};
    $scope.newListName = '';
    $scope.newItemName = '';
    $scope.isItemDetailsPaneEnabled = false;

    $scope.toggleTodoList = function (id) {
        $scope.fetchTodoItems(id);
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

    $scope.executeToggleItemDetails = function (itemId) {
        if (!$scope.isItemDetailsPaneEnabled) {
            for (var i = 0; i < $scope.todoItems.length; i++) {
                if ($scope.todoItems[i].id == itemId) {
                    $scope.selectedTodoItem = $scope.todoItems[i];
                    $scope.isItemDetailsPaneEnabled = true;
                    break;
                }
            }
        }
    };

    $scope.executeHideItemDetails = function () {
        $scope.isItemDetailsPaneEnabled = false;
    }

    $scope.executeToggleItemComplete = function () {
        $scope.selectedTodoItem.isFinished = !$scope.selectedTodoItem.isFinished;
        $scope.updateTodoItem($scope.selectedTodoItem);
    };

    $scope.executeToggleItemFavourite = function () {
        $scope.selectedTodoItem.isFavourite = !$scope.selectedTodoItem.isFavourite;
        $scope.updateTodoItem($scope.selectedTodoItem);
    };

    $scope.fetchTodoItems = function (listId) {
        for (var i = 0; i < $scope.todoLists.length; i++) {
            if ($scope.todoLists[i].id == listId) {
                $scope.selectedTodoList = $scope.todoLists[i];
                $http.post('/api/items/GetByList', {
                    token: GetLoginToken(),
                    id: $scope.selectedTodoList.id
                }).success(function (data, status) {
                    $scope.todoItems = data;
                }).error(function (data, status) {
                    alert('Error while fetching todo items: ' + status);
                });
                break;
            }
        }
    };

    $scope.updateTodoItem = function (item) {
        item.token = GetLoginToken();
        $http.put('/api/items', item).success(function (data, status) {

        }).error(function (data, status) {
            alert('Problem with updating the item. Try again');
        });
    };

    $http.get('/api/lists?token=' + GetLoginToken()).success(function (data, status) {
        $scope.todoLists = data;
    }).error(function (data, status) {
        alert('Error while fetching todo lists. Refresh the page and try again.');
    });
});