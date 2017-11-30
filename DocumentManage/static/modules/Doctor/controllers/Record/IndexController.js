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

                    //查询列表
                    $scope.onSearch = function (page) {
                        debugger;
                        $scope.Record.PageSize = $scope.pageSize;
                        $scope.Record.PageIndex = $scope.CurrentPage;
                        //请求
                        apiUtil.requestWebApi("Record/GetList", "Post", $scope.Record, function (response) {
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
                        $state.go("Index.RecordEdit");
                    }


                    //患者详细页面
                    $scope.showDetail = function (item) {
                        $state.go("Index.RecordDetail", { id: item.DoctorMemberID });
                    }
                }]);
        });