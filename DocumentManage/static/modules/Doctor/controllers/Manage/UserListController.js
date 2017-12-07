"use strict";
define(["module-services-apiUtil", "module-directive-bundling-all"], function (apiUtil) {

            var app = angular.module("myApp", [
             "pascalprecht.translate",
             'ui.router',
             "ui.bootstrap",
             "ngAnimate"]);
            app.controller('UserListController', ['$scope', '$state', '$translate', function ($scope, $state, $translate) {
                $scope.CurrentPage = 1;
                $scope.PageSize = 10;
                $scope.TotalCount = 1;
                $scope.ListItems = [];
                $scope.Record = {};
                $scope.EditUserRole = {};
                $scope.UserPassword = {};
           
                $scope.onSearch = function ()
                {
                    $scope.Record.PageSize = $scope.PageSize;
                    $scope.Record.PageIndex = $scope.CurrentPage;
                    //请求
                    apiUtil.requestWebApi("User/GetUserList", "Post", $scope.Record, function (response) {
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
                };

                $scope.onResetPassword = function (userID) {
                    $scope.UserPassword.UserID = userID;
                    $("#modal-ResetPassword").modal("show");
                };

                $scope.onSavePassword = function () {
                    apiUtil.requestWebApi("User/ResetPassoword", "Post", $scope.UserPassword, function (response) {
                        layer.msg(response.Msg);
                        $("#modal-ResetPassword").hide();
                    }, function (response) {
                        layer.msg(response.Msg);
                        $("#modal-ResetPassword").hide();
                    });
                };

                $scope.onEditUserRole = function (userID) {
                    $scope.roles = [];
                    $scope.onGetUserRoles(userID);
                    $scope.EditUserRole.UserID = userID;
                    $("#modal-EditUserRole").modal("show");
                };

                $scope.onGetUserRoles = function (userID) {
                    apiUtil.requestWebApi("User/GetRoleList", "Post", new { UserID: userID }, function (response) {
                        $scope.roles = response.Data;
                        $scope.$apply();
                    }, function (response) {
                        layer.msg(response.Msg);
                    });
                }

                $scope.onUserRoleSave = function () {
                    apiUtil.requestWebApi("User/EditUserRoles", "Post", $scope.EditUserRole, function (response) {
                        layer.msg(response.Msg);
                        $("#modal-EditUserRole").hide();
                    }, function (response) {
                        layer.msg(response.Msg);
                        $("#modal-EditUserRole").hide();
                    });
                }
            }
            ]);
        });