app.service('loginservice', function ($http) {

    this.login = function (userlogin) {

        var resp = $http({
            url: "/token",
            method: "POST",
            data: $.param({ grant_type: 'password', username: userlogin.username, password: userlogin.password }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        });
        return resp;
    };
});

app.controller('logincontroller', function ($scope, loginservice) {

    //Scope Declaration
    $scope.responseData = "";

    $scope.userLoginUserName = "";
    $scope.userLoginPassword = "";

    $scope.accessToken = "";
    $scope.refreshToken = "";
    //Ends Here
    
    //Function to Login. This will generate Token 
    $scope.login = function () {
        //This is the information to pass for token based authentication
        var userLogin = {
            grant_type: 'password',
            username: $scope.userLoginUserName,
            password: $scope.userLoginPassword
        };

        var promiselogin = loginservice.login(userLogin);

        promiselogin.then(function (resp) {

            
            //Store the token information in the SessionStorage
            //So that it can be accessed for other views
            sessionStorage.setItem('userName', $scope.userLoginUserName);
            sessionStorage.setItem('accessToken', resp.data.access_token);
            //sessionStorage.setItem('refreshToken', resp.data.refresh_token);
            window.location.href = '/CRUD/Options';
        }, function (err) {
            if (err.status == 400) {
                $scope.responseData = "Error! Username or Password Incorrect.";
            }
            else
            $scope.responseData = "Error! " + err.status;
        });

    };
});