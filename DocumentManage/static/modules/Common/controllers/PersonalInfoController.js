"use strict";
define(["jquery",
        "module-services-apiUtil",
        'plugins-layer',
        'bootstrap-typeahead',
        "jquery-form",
        "module-directive-bundling-all"], function ($, apiUtil, layer) {

            var app = angular.module("myApp", [
          "pascalprecht.translate",
          'ui.router',
          "ui.bootstrap",
          "ngAnimate"]);

            app.controller('PersonalInfoController', [
                '$scope',
                '$http',
                "$q",
                '$location',
                '$state',
                '$translate',
                '$filter',
                function ($scope, $http, $q, $location, $state, $translate, $filter) {
                    var params = $state.params;

                    $scope.Data = {};
                    $scope.load = function () {
                        apiUtil.requestWebApi("User/GetUserInfo", "Get",null,
                            function (response) {
                                if (response.Status == 0) {
                                    $scope.Data = response.Data;
                                }
                            });
                    };
                    $scope.load();
                    
                    $scope.save = function () {
                        apiUtil.requestWebApi("User/UpdateUserInfo", "Post",$scope.Data,
                        function (response) {
                            if (response.Status == 0) {
                                layer.msg("保存成功");
                                var loginInfo = apiUtil.getLoginInfo();
                                loginInfo.UserName = $scope.Data.UserName;
                                apiUtil.setLoginInfo(loginInfo);
 
                            } else {
                                layer.msg("保存失败", { icon: 2, shade: 0.5 });
                            }
                        },
                        function (response) {
                            layer.msg("保存失败", { icon: 2, shade: 0.5 });
                        });
                    };
                }
            ]);

        });