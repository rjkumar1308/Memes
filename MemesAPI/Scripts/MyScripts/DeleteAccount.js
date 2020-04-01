app.service('deleteaccountservice', function ($http) {
  
    this.DeleteAccount = function ($UserName) {

        var accesstoken = sessionStorage.getItem('accessToken');
        var authHeaders = {};
        if (accesstoken) {
            authHeaders.Authorization = 'Bearer ' + accesstoken;
        }
        $data = JSON.stringify($UserName);
        var response = $http({
            url: "/api/deleteaccount",
            method: "POST",
            headers: authHeaders,
            data: $data
        });
        return response;
    };

});

app.controller('deleteaccountcontroller', function ($scope,deleteaccountservice,optionservice) {

    $scope.userName = sessionStorage.getItem('userName');
    $scope.Password = "";
    $scope.responseError = "";

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
    
    $scope.deleteaccount = function () {
        if ($scope.Password == "") {
            $scope.responseError = "Please Enter The Password.";
            return;
        }
        var user = {
            UserName: $scope.userName,
            Password: $scope.Password
        };
        var prom = deleteaccountservice.DeleteAccount(user);
        prom.then(function (resp) {
            if (resp.data.message == "Success") {
                window.alert("Successfully Delted The Account.");
                sessionStorage.removeItem('userName');
                sessionStorage.removeItem('accessToken');
                window.location.href = '/Home/Index';
            }
            else {
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

    }
    $scope.back = function () {
        window.location.href = '/CRUD/Options';
    };

});