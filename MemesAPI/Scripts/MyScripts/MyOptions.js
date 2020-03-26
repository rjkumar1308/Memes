app.service('optionservice', function ($http) {
    this.get = function () {

        var accesstoken = sessionStorage.getItem('accessToken');

        var authHeaders = {};
        if (accesstoken) {
            authHeaders.Authorization = 'Bearer ' + accesstoken;
        }

        var response = $http({
            url: "/api/getallimages",
            method: "GET",
            headers: authHeaders
        });
        return response;
    };
    this.verify = function () {

        var accesstoken = sessionStorage.getItem('accessToken');

        var authHeaders = {};
        if (accesstoken) {
            authHeaders.Authorization = 'Bearer ' + accesstoken;
        }

        var response = $http({
            url: "/api/getaccess",
            method: "GET",
            headers: authHeaders
        });
        return response;
    };
});

app.controller('optioncontroller', function ($scope, optionservice) {
    $scope.Memes = [];

    $scope.Message = "";
    $scope.userName = sessionStorage.getItem('userName');

    LoadMemes();

    function LoadMemes() {

        var prom = optionservice.verify();
        prom.then(function () {
            var promise = optionservice.get();
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
    $scope.addtag = function ($id) {
        window.location.href = '/CRUD/AddTags/?id='+$id;
    };
    $scope.deletetag = function ($id) {
        window.location.href = '/CRUD/DeleteTags/?id=' + $id;
    };
    $scope.updateurl = function ($id) {
        window.location.href = '/CRUD/UpdateURL/?id=' + $id;
    };
    $scope.deleteimage = function ($id) {
        window.location.href = '/CRUD/DeleteImage/?id=' + $id;
    };
    $scope.addnewmeme = function () {
        window.location.href = '/CRUD/AddNewMeme';
    };
});