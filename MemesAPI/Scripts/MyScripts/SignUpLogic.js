app.service('signupservice', function ($http) {

    this.signup = function (userSignUp) {
        $data = JSON.stringify(userSignUp);
        var resp = $http({
            url: "/api/signup",
            method: "POST",
            data: $data
        });
        return resp;
    };
});

app.controller('signupcontroller', function ($scope, signupservice) {

    $scope.responseData = "";
    $scope.responseMessage = "";

    $scope.userLoginUserName = "";
    $scope.userLoginPassword = "";
    $scope.userLoginPasswordCnf = "";
    $scope.userLoginEmail = "";

    $scope.signup = function () {

        if ($scope.userLoginUserName == "") {
            $scope.responseData = "Plaese Enter The UserName.";
            return;
        }
        if ($scope.userLoginPassword == "") {
            $scope.responseData = "Plaese Enter The Password.";
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
        if ($scope.userLoginEmail == "") {
            $scope.responseData = "Plaese Enter The Email.";
            return;
        }
        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        if (!$scope.userLoginEmail.match(mailformat)) {
            $scope.responseData = "Plaese Enter Valid Email.";
            return;
        }

        var userSignUp = {
            UserName: $scope.userLoginUserName,
            Password: $scope.userLoginPassword,
            Email: $scope.userLoginEmail,
            Role: 'admin'
        };

        var promisesignup = signupservice.signup(userSignUp);

        promisesignup.then(function (resp) {
            if (resp.data.message.search('0x80131904') >= 0) {
                $scope.responseData = "Error! UserName Is Already Taken.";
                return;
            }
            $scope.responseData = "";
            $scope.responseMessage = "Successfully Signed Up.";
        }, function (err) {
            window.alert("Error! " + err.status);
            window.location.href = '/Home/Index';
        });

    };

    $scope.back = function () {
        window.location.href = '/Home/Index';
    }

});