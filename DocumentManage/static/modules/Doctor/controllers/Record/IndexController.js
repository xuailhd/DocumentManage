"use strict";
define(["module-services-apiUtil", "module-directive-bundling-all"], function (apiUtil) {

            var app = angular.module("myApp", [
             "pascalprecht.translate",
             'ui.router',
             "ui.bootstrap",
             "ngAnimate"]);

            app.controller('RecordController', ['$scope', "$state", '$translate',
                function ($scope, $state, $translate) {
                    $scope.ListItems = [];
                    $scope.pageSize = 10;
                    $scope.CurrentPage = 1;
                    $scope.totalCount = 0;
                    $scope.Record = {};

                    var flag = false;
                    for (var i = 0; i < golbal_Modules.length; i++) {
                        if (golbal_Modules[i].AuthUrl == "#/Index" + "/Record") {
                            flag = true;
                        }
                    }

                    if (!flag) {
                        $state.go('Index.PersonalInfo');
                    }

                    //查询列表
                    $scope.onSearch = function (page) {
                        $scope.Record.PageSize = $scope.pageSize;
                        $scope.Record.PageIndex = $scope.CurrentPage;
                        var loading = layer.load(0, { shade: [0.1, '#000'] });
                        //请求
                        apiUtil.requestWebApi("Record/GetList", "Post", $scope.Record, function (response) {
                            layer.close(loading);
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
                            layer.close(loading);
                        });
                    }

                    //患者详细页面
                    $scope.showEdit = function () {
                        $state.go("Index.RecordEdit");
                    }

                }]);
        });