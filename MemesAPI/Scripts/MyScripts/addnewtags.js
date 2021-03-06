﻿app.service('addnewtagsservice', function ($http) {
    this.add = function ($Id, $tags) {

        var accesstoken = sessionStorage.getItem('accessToken');        
        var authHeaders = {};
        if (accesstoken) {
            authHeaders.Authorization = 'Bearer ' + accesstoken;
        }
        $data = JSON.stringify({ ImageId: $Id, Tags: $tags });
        var response = $http({
            url: "/api/update_add_tag",
            method: "POST",
            headers: authHeaders,
            data: $data
        });
        return response;
    };

});

app.controller('addtagscontroller', function ($scope, addnewtagsservice, optionservice) {
    $scope.tagsarray = [];
    $scope.tagsstring = "";
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


    $scope.addtags = function () {
        $scope.tagsarray = $scope.tagsstring.split(',');
        if ($scope.tagsarray == "") {
            $scope.responseError = "Enter the Tags";
            return;
        }
        var promise = addnewtagsservice.add($scope.id, $scope.tagsarray);
        promise.then(function (resp) {
            if (resp.data.message == "Success") {
                $scope.responseError = "";
                $scope.responseData = "Successfully Added the Tags.";
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