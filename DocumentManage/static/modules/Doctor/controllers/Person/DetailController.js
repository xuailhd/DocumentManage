"use strict";
define(["module-services-apiUtil", "jquery-validate"], function (apiUtil) {

    var app = angular.module("myApp", [
        "pascalprecht.translate",
        'ui.router',
        "ui.bootstrap",
        "ngAnimate"]);

    app.controller('PersonDetailController', [
        '$scope',
        '$http',
        "$q",
        '$location',
        '$state',
        '$translate',
        function ($scope, $http, $q, $location, $state, $translate) {

            /*************************以下是和服务端交互的数据*****************/
            $scope.PersonID = $state.params.PersonID;

            var flag = false;
            for (var i = 0; i < golbal_Modules.length; i++) {
                if (golbal_Modules[i].AuthUrl == "#/Index" + "/Person") {
                    flag = true;
                }
            }

            if (!flag) {
                $state.go('Index.PersonalInfo');
            }

            $scope.Record = {};

            $scope.onTagChange = function () {
                switch ($scope.Record.Tag) {
                    case '1':
                        $('#Tag').attr('style', 'background-color:red');
                        break;
                    case '2':
                        $('#Tag').attr('style', 'background-color:yellow');
                        break;
                    case '3':
                        $('#Tag').attr('style', 'background-color:blue');
                        break;
                    case '4':
                        $('#Tag').attr('style', 'background-color:green');
                        break;
                    case '5':
                        $('#Tag').attr('style', 'background-color:white');
                        break;
                    default:
                        $('#Tag').attr('style', '');
                        break;
                }
            }

            
            $scope.onLoad = function () {
                var data = { PersonID: $scope.PersonID };
                //请求
                apiUtil.requestWebApi("Person/GetDetail", "Post", data, function (response) {
                    if (response.Status == 0) {
                        $scope.Record = response.Data;
                        $scope.onTagChange();
                        return;
                    }
                    else {
                        layer.msg(response.Msg);
                    }
                }, function (response) {
                    layer.msg(response.Msg);
                });
            }

            $scope.onLoad();


            $scope.GoBack = function () {
                history.back();
            };
        }
    ]);

});
