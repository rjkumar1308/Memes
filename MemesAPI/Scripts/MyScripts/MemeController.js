app.controller('memecontroller', function ($scope, memeservice) {
    $scope.Memes = [];

    $scope.Message = "";
    $scope.userName = sessionStorage.getItem('userName');

    LoadMemes();

    function LoadMemes() {

        var promise = memeservice.get();
        promise.then(function (resp) {
            $scope.Memes = resp.data;
            //$scope.Message = "Call Completed Successfully";
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
    $scope.logout = function () {
        sessionStorage.removeItem('userName');
        sessionStorage.removeItem('accessToken');
        window.location.href = '/Home/Index';
    };
});