shorthornApp.controller('todoListsController', function ($scope, $rootScope, $http, $location) {
    $scope.todoLists = [];
    $scope.todoItems = [];
    $scope.selectedTodoList = {};
    $scope.selectedTodoItem = {};
    $scope.newListName = '';
    $scope.newItemName = '';
    $scope.isItemDetailsPaneEnabled = false;
    var moreEdits = false;
    $scope.isUpdatingTodoItem = false;
    $scope.isFavouriteListVisible = false;
    $scope.statusTodoOperation = 0;
    $scope.messageStatusTodoOperation = '';

    /*
    Status codes:
        1 - Success (create todo list)
        2 - Success (create todo item)
        3 - Success (delete todo item)
        ================================
        4 - Error (toggle favourite item list)
        5 - Error (create todo list)
        6 - Error (create todo item)
        7 - Error (delete todo item)
        8 - Error (fetch todo list)
        9 - Error (update todo item)
        10 - Error (fetching weather)
        11 - Error (fetching todo lists) 
    */

    $.backstretch('/images/list_background.jpg');

    $scope.toggleTodoList = function (id) {
        $scope.isFavouriteListVisible = false;
        $scope.fetchTodoItems(id);
    };

    $scope.executeToggleFavouriteItemsList = function () {
        $scope.isFavouriteListVisible = true;
        $http.post('/api/items/GetFavourites', {
            token: GetLoginToken()
        }).success(function (data, status) {
            $scope.selectedTodoList = null;
            $scope.todoItems = data;
        }).error(function (data, status) {
            $scope.statusTodoOperation = 4; //4 - Error (toggle favourite item list), 
            $scope.messageStatusTodoOperation = 'Error while toggling favourite item. Refresh the page and try again.';
        });
    };

    $scope.executeCreateTodoList = function () {
        $http.post('/api/lists', {
            token: GetLoginToken(),
            name: $scope.newListName
        }).success(function (data, status) {
            $http.get('/api/lists?token=' + GetLoginToken()).success(function (data, status) {
                $scope.todoLists = data;

            }).error(function (data, status) {
                $scope.messageStatusTodoOperation = 'Error while fetching todo lists. Refresh the page and try again.';
            });
            $scope.statusTodoOperation = 1; //1 - Success (create todo list), 
            $scope.messageStatusTodoOperation = 'Todo list created!';
        }).error(function (data, status) {
            $scope.statusTodoOperation = 5; //5 - Error (create todo list),
            $scope.messageStatusTodoOperation = 'Error while creating todo list. Refresh the page and try again.';
        });
        $scope.newListName = '';
    };

    $scope.executeCreateTodoListKeyPress = function (keyEvent) {
        if (keyEvent.which == 13){
            $scope.executeCreateTodoList();
        }
    };

    $scope.executeCreateTodoItem = function (keyEvent) {
        if (keyEvent.which == 13) {
            $http.post('/api/items', {
                token: GetLoginToken(),
                name: $scope.newItemName,
                parentListId: $scope.selectedTodoList.id
            }).success(function (data, status) {
                $scope.fetchTodoItems($scope.selectedTodoList.id);
                $scope.newItemName = '';
                $scope.statusTodoOperation = 2; //2 - Success (create todo item), 
                $scope.messageStatusTodoOperation = 'Task created!';
            }).error(function (data, status) {
                $scope.statusTodoOperation = 6; //6 - Error (create todo item), 
                $scope.messageStatusTodoOperation = 'Error while creating task. Refresh the page and try again.';
            });
        }
    };

    $scope.executeToggleItemDetails = function (itemId) {
        for (var i = 0; i < $scope.todoItems.length; i++) {
            if ($scope.todoItems[i].id == itemId) {
                $scope.selectedTodoItem = $scope.todoItems[i];
                $scope.selectedTodoItem.dateFinish = moment($scope.selectedTodoItem.dateFinish).format('MM/DD/YYYY');
                $scope.isItemDetailsPaneEnabled = true;

                $scope.fetchWeatherDetails();
                break;
            }
        }
    };

    $scope.executeItemEdit = function () {
        $scope.isUpdatingTodoItem = true;
        moreEdits = true;
        setTimeout($scope.executeItemEditsUpdate, 1000);
        moreEdits = false;
    };

    $scope.executeItemEditsUpdate = function () {
            if (!moreEdits) {
                $scope.updateTodoItem($scope.selectedTodoItem);
                $scope.fetchWeatherDetails();
            $scope.isUpdatingTodoItem = false;
        }
        
    };

    $scope.executeHideItemDetails = function () {
        $scope.isItemDetailsPaneEnabled = false;
    };

    $scope.executeToggleItemComplete = function () {
        $scope.selectedTodoItem.isFinished = !$scope.selectedTodoItem.isFinished;
        $scope.updateTodoItem($scope.selectedTodoItem);
    };

    $scope.executeDeleteItem = function (id) {
        $http.delete('/api/items/' + id + '?token=' + GetLoginToken()).success(function (data, status) {
            for (var i = 0; i < $scope.todoItems.length; i++) {
                if ($scope.todoItems[i].id == id) {
                    $scope.todoItems.splice(i, 1);
                    $scope.executeHideItemDetails();
                    $scope.selectedTodoItem = null;
                    break;
                }
            }
            $scope.statusTodoOperation = 3; //3 - Success (delete todo item), 
            $scope.messageStatusTodoOperation = 'Task deleted!';
        }).error(function (data, status) {
            $scope.statusTodoOperation = 7; //7 - Error (delete todo item), 
            $scope.messageStatusTodoOperation = 'Error while deleting task. Refresh the page and try again.';
        });
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
                    $scope.statusTodoOperation = 8; //8 - Error (fetch todo list), 
                    $scope.messageStatusTodoOperation = 'Error while fetching todo list. Refresh the page and try again.';
                });
                break;
            }
        }
    };

    $scope.updateTodoItem = function (item) {
        item.token = GetLoginToken();
        $http.put('/api/items', item).success(function (data, status) {

        }).error(function (data, status) {
            $scope.statusTodoOperation = 9; //9 - Error (update todo item), 
        });
    };
    
    $scope.fetchWeatherDetails = function () {

        $http.get('http://api.openweathermap.org/data/2.5/forecast/daily?q=Warsaw,pl&cnt=14&mode=json').success(function (postback) {
            $scope.dateNow = moment(moment()).format('MM/DD/YYYY');
            $scope.daysBetweenDates = moment($scope.selectedTodoItem.dateFinish).diff($scope.dateNow, 'days');

            if ($scope.daysBetweenDates >= 0 && $scope.daysBetweenDates <= 13) {
                $scope.weather = postback.list[$scope.daysBetweenDates];
                $scope.weather.temp.day = ($scope.weather.temp.day - 273.15).toFixed(1);
            }
            else {
                $scope.weather = null;
            }
        }).error(function () {
            $scope.statusTodoOperation = 10; //10 - Error (fetching weather),
            $scope.messageStatusTodoOperation = 'Error while fetching weather. Refresh the page and try again.';
        });
    }

    $http.get('/api/lists?token=' + GetLoginToken()).success(function (data, status) {
        $scope.todoLists = data;
    }).error(function (data, status) {
        $scope.statusTodoOperation = 11; //11 - Error (fetching todo lists), 
        $scope.messageStatusTodoOperation = 'Error while fetching todo lists. Refresh the page and try again.';
    });

    $scope.executeToggleFavouriteItemsList();
});