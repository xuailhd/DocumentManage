"use strict";
define(["module-services-apiUtil", "module-directive-bundling-all"], function (apiUtil) {

            var app = angular.module("myApp", [
             "pascalprecht.translate",
             'ui.router',
             "ui.bootstrap",
             "ngAnimate"]);

            app.controller('PersonController', ['$scope', "$state", '$translate', "$q",
                function ($scope, $state, $translate, $q) {
                    $scope.ListItems = [];
                    $scope.pageSize = 10;
                    $scope.CurrentPage = 1;
                    $scope.totalCount = 0;

                    $scope.Record = { Tag : ''};

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


                    //查询列表
                    $scope.onSearch = function (page) {
                        $scope.Record.PageSize = $scope.pageSize;
                        $scope.Record.PageIndex = $scope.CurrentPage;
                        //请求
                        apiUtil.requestWebApi("Person/GetList", "Post", $scope.Record, function (response) {
                            if (response.Status == 0) {
                                $scope.ListItems = response.Data;
                                $scope.totalCount = response.Total;
                                $scope.$apply();
                            }
                            else {
                                layer.msg(response.Msg);
                            }
                        }, function (response) {
                            layer.msg(response.Msg);
                        });
                    }

                    //患者详细页面
                    $scope.showEdit = function () {
                        $state.go("Index.PersonEdit");
                    }
                }]);
        });