app.service('addnewmemeservice', function ($http) {
    this.add = function ($url,$tags) {

        var accesstoken = sessionStorage.getItem('accessToken');

        var authHeaders = {};
        if (accesstoken) {
            authHeaders.Authorization = 'Bearer ' + accesstoken;
        }
        $data = JSON.stringify({ Url: $url, Tags: $tags });
        var response = $http({
            url: "/api/addnewimage",
            method: "POST",
            headers: authHeaders,
            data: $data
        });
        return response;
    };
    
});

app.controller('addnewmemecontroller', function ($scope, addnewmemeservice, optionservice) {
    $scope.tagsarray = [];
    $scope.tagsstring = "";
    $scope.url = "";
    $scope.responseData = "";
    $scope.responseError = "";

    var prom = optionservice.verify();
    prom.then(function () { },function (err) {
        if (err.status == 401) {
            window.alert("Error! Unauthorized");
        }
        else {
            window.alert("Error!" + err.status);
        }
        window.location.href = '/Home/Index';
    });

    $scope.addnewmeme = function () {
        $scope.tagsarray = $scope.tagsstring.split(',');
        if ($scope.url == "") {
            $scope.responseError = "Please Enter the URL.";
            return;
        }
        var promise = addnewmemeservice.add($scope.url, $scope.tagsarray);
        promise.then(function (resp) {
            if (resp.data.message == "Success") {
                $scope.responseError = "";
                $scope.responseData = "Successfully Inserted The Meme.";
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