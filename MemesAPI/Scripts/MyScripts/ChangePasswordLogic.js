app.service('changepasswordservice', function ($http) {

    this.change = function (userChangePass) {
        var accesstoken = sessionStorage.getItem('accessToken');
        var authHeaders = {};
        if (accesstoken) {
            authHeaders.Authorization = 'Bearer ' + accesstoken;
        }
        $data = JSON.stringify(userChangePass);
        var resp = $http({
            url: "/api/changepass",
            method: "POST",
            headers: authHeaders,
            data: $data
        });
        return resp;
    };
});

app.controller('changepasswordcontroller', function ($scope, changepasswordservice, optionservice) {

    $scope.responseData = "";
    $scope.responseMessage = "";

    $scope.useroldpassword = "";
    $scope.userLoginPassword = "";
    $scope.userLoginPasswordCnf = "";
    $scope.userName = sessionStorage.getItem('userName');


    var prom = optionservice.verify();
    prom.then(function () {}, function (err) {
            if (err.status == 401) {
                window.alert("Error! Unauthorized");
            }
            else {
                window.alert("Error!" + err.status);
            }
            window.location.href = '/Home/Index';
        });
    
    $scope.changepassword = function () {

        if ($scope.useroldpassword == "") {
            $scope.responseData = "Plaese Enter The Old Password.";
            return;
        }
        if ($scope.userLoginPassword == "") {
            $scope.responseData = "Plaese Enter The New Password.";
            return;
        }
        if ($scope.userLoginPasswordCnf == "") {
            $scope.responseData = "Plaese Enter The Confirmation Password.";
            return;
        }
        if ($scope.userLoginPassword != $scope.userLoginPasswordCnf) {
            $scope.responseData = "Error! Passwords Does Not Match.";
            return;
        }

        var userChangePass = {
            UserName: $scope.userName,
            OldPassword: $scope.useroldpassword,
            Password: $scope.userLoginPassword
        };

        var promise = changepasswordservice.change(userChangePass);

        promise.then(function (resp) {
            $scope.responseData = "";
            $scope.responseMessage = resp.data.message;
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
    }

});