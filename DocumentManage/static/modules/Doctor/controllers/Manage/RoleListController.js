﻿"use strict";
define(["module-services-apiUtil", "module-directive-bundling-all"], function (apiUtil) {

            var app = angular.module("myApp", [
             "pascalprecht.translate",
             'ui.router',
             "ui.bootstrap",
             "ngAnimate"]);
            app.controller('RoleListController', ['$scope', '$state', '$translate', function ($scope, $state, $translate) {
                $scope.CurrentPage = 1;
                $scope.PageSize = 10;
                $scope.TotalCount = 1;
                $scope.ListItems = [];
                $scope.Record = {};
           
                $scope.onSearch = function ()
                {
                    $scope.Record.PageSize = $scope.PageSize;
                    $scope.Record.PageIndex = $scope.CurrentPage;
                    //请求
                    apiUtil.requestWebApi("User/GetRoleList", "Post", $scope.Record, function (response) {
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

                $scope.onEditRole = function (role) {
                    $scope.Role = role;
                    $scope.Role.IsNew = false;
                    $("#modal-EditRole").modal("show");
                };

                $scope.onAddRole = function () {
                    $scope.Role = {};
                    $scope.Role.IsNew = true;
                    $("#modal-EditRole").modal("show");
                };

                $scope.onSaveRole = function () {
                    if ($("#roleForm").valid()) {
                        apiUtil.requestWebApi("User/EditRole", "Post", $scope.Role, function (response) {
                            layer.msg(response.Msg);
                            $("#modal-EditRole").modal("hide");
                        }, function (response) {
                            layer.msg(response.Msg);
                            $("#modal-EditRole").modal("hide");
                        });
                    }
                };

                $scope.onGetRoleAuth = function (roleID) {
                    apiUtil.requestWebApi("User/GetAuthList", "Post", { RoleID: roleID }, function (response) {
                        $scope.EditRoleAuth.AuthLists = response.Data;
                        $scope.$apply();
                    }, function (response) {
                        layer.msg(response.Msg);
                    });
                };

                $scope.onEditAuth = function (roleID) {
                    $scope.EditRoleAuth = {};
                    $scope.EditRoleAuth.RoleID = roleID;
                    $scope.EditRoleAuth.AuthLists = [];
                    $scope.onGetRoleAuth(roleID);

                    $("#modal-EditRoleAuth").modal("show");
                };

                $scope.onRoleAuthSave = function () {
                    apiUtil.requestWebApi("User/EditRoleAuths", "Post", $scope.EditRoleAuth, function (response) {
                        layer.msg(response.Msg);
                        $("#modal-EditRoleAuth").modal("hide");
                    }, function (response) {
                        layer.msg(response.Msg);
                        $("#modal-EditRoleAuth").modal("hide");
                    });
                };

                $scope.onSearch();
            }
            ]);
        });