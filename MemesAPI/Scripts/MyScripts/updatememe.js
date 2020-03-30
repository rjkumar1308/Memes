app.service('updatememeservice', function ($http) {
    this.add = function ($url, $ImageId) {

        var accesstoken = sessionStorage.getItem('accessToken');        
        var authHeaders = {};
        if (accesstoken) {
            authHeaders.Authorization = 'Bearer ' + accesstoken;
        }
        $data = JSON.stringify({ Url: $url, ImageId: $ImageId });
        var response = $http({
            url: "/api/update",
            method: "POST",
            headers: authHeaders,
            data: $data
        });
        return response;
    };

});

app.controller('updatememecontroller', function ($scope, updatememeservice, optionservice) {
    //$scope.tagsarray = [];
    //$scope.tagsstring = "";
    $scope.url = "";
    $scope.responseData = "";
    $scope.responseError = "";
    var id = new URL(location.href).searchParams.get('id');
    $scope.id = parseInt(id);

    var prom = optionservice.verify();
    prom.then(function () { }, function (err) {
        if (err.status == 401) {
            window.alert("Error! Unauthorized");
        }
        else {
            window.alert("Error!" + err.status);
        }
        window.location.href = '/Home/Index';
    });

    $scope.update = function () {
        
        if ($scope.url == "") {
            $scope.responseError = "Please Enter the URL.";
            return;
        }

        var promise = updatememeservice.add($scope.url, $scope.id);
        promise.then(function (resp) {
            if (resp.data.message == "Success") {
                $scope.responseError = "";
                $scope.responseData = "Successfully Updated the url.";
            }
            else if (resp.data.message.search('0x80131904') >= 0) {
                $scope.responseData = "";
                $scope.responseError = "Error! Meme Already Exists.";
            }
            else {
                $scope.responseData = "";
                $scope.responseError = resp.data.message;
            }
        }, function (err) {
            if (err.status == 401) {
                window.alert("Error! Unauthorized");
            }
            else {
                window.alert("Error!" + err.status);
            }
            window.location.href = '/Home/Index';
        });
    };
    $scope.back = function () {
        window.location.href = '/CRUD/Options';
    };
});