"use strict";
define(["plugins-echarts", "module-directive-bundling-all",
        "css!styles/layout.schedules.min.css"], function (echarts) {

            var app = angular.module("myApp", [
             "pascalprecht.translate",
             'ui.router',
             "ui.bootstrap",
             "ngAnimate"]);

            app.controller('QueryController', ['$scope', "$state", '$translate',
                function ($scope, $state, $translate) {
                    $scope.ListItems = [];
                    $scope.pageSize = 10;
                    $scope.CurrentPage = 1;
                    $scope.totalCount = 0;
                    //查询列表
                    $scope.onSearch = function (page) {
                    }


                    //患者详细页面
                    $scope.showDetail = function (item) {
                        $state.go("Doctor.MyPatientDetail", { id: item.DoctorMemberID });
                    }
                }]);
        });