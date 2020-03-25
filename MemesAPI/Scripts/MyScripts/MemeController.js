app.controller('memecontroller', function ($scope, memeservice) {
    $scope.Memes = [];

    $scope.Message = "";
    $scope.userName = sessionStorage.getItem('userLoginUserName');


    LoadMemes();

    function LoadMemes() {


        var promise = memeservice.get();
        promise.then(function (resp) {
            $scope.Memes = resp.data;
            $scope.Message = "Call Completed Successfully";
        }, function (err) {
            $scope.Message = "Error!!! " + err.status
        });
    };
    $scope.logout = function () {

        sessionStorage.removeItem('accessToken');
        window.location.href = '/Login/SecurityInfo';
    };
});