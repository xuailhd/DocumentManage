"use strict";
define(["module-services-apiUtil", "jquery-validate"], function (apiUtil) {

    var app = angular.module("myApp", [
        "pascalprecht.translate",
        'ui.router',
        "ui.bootstrap",
        "ngAnimate"]);

    app.controller('RecordDetailController', [
        '$scope',
        '$http',
        "$q",
        '$location',
        '$state',
        '$translate',
        function ($scope, $http, $q, $location, $state, $translate) {

            /*************************以下是和服务端交互的数据*****************/
            $scope.OrgID = $state.params.OrgID;

            $scope.Record = {};

            var countrys = [{
                Name: '中国', PingYin: 'ZG', Continent: '亚洲',
                Provinces: [
                    { Name: '北京', PingYin: 'BJ' },
                    { Name: '上海', PingYin: 'SH' },
                ]
            },
            {
                Name: '美国', PingYin: 'MG', Continent: '美洲',
                Provinces: [
                    { Name: '洛杉矶', PingYin: 'LSJ' },
                ]
            }];

            
            $scope.onTagChange = function () {
                switch ($scope.Record.Tag) {
                    case '0':
                        $('#Tag').attr('style', 'background-color:red');
                        break;
                    case '1':
                        $('#Tag').attr('style', 'background-color:yellow');
                        break;
                    case '2':
                        $('#Tag').attr('style', 'background-color:blue');
                        break;
                    case '3':
                        $('#Tag').attr('style', 'background-color:green');
                        break;
                    case '4':
                        $('#Tag').attr('style', 'background-color:white');
                        break;
                }
            }

            
            $scope.onLoad = function () {
                var data = { OrgID: $scope.OrgID };
                //请求
                apiUtil.requestWebApi("Org/GetDetail", "Post", data, function (response) {
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
