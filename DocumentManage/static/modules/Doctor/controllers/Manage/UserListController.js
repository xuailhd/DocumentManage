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
                $scope.UserAccount = {};

                var flag = false;
                for (var i = 0; i < golbal_Modules.length; i++) {
                    if (golbal_Modules[i].AuthUrl == "#/Index" + "/Manage/UserList") {
                        flag = true;
                    }
                }

                if (!flag) {
                    $state.go('Index.PersonalInfo');
                }

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

                $scope.addAccount = function () {
                    $("#modal-AddAccount").modal("show");
                };

                $scope.onSaveAccount = function () {
                    if ($("#addAccountForm").valid()) {
                        apiUtil.requestWebApi("User/AddAccount", "Post", $scope.UserAccount, function (response) {
                            layer.msg(response.Msg);
                            $scope.onSearch();
                            $("#modal-AddAccount").modal("hide");
                        }, function (response) {
                            layer.msg(response.Msg);
                            $("#modal-AddAccount").modal("hide");
                        });
                    }
                };

                $scope.onResetPassword = function (id) {
                    $scope.UserPassword.ID = id;
                    $("#modal-ResetPassword").modal("show");
                };

                $scope.onSavePassword = function () {
                    if ($("#passwordForm").valid()) {
                        apiUtil.requestWebApi("User/ResetPassword", "Post", $scope.UserPassword, function (response) {
                            layer.msg(response.Msg);
                            $scope.onSearch();
                            $("#modal-ResetPassword").modal("hide");
                        }, function (response) {
                            layer.msg(response.Msg);
                            $("#modal-ResetPassword").modal("hide");
                        });
                    }
                };

                $scope.onGetUserRoles = function (id) {
                    apiUtil.requestWebApi("User/GetRoleList", "Post", { ID: id }, function (response) {
                        $scope.EditUserRole.RoleLists = response.Data;
                        $scope.$apply();
                    }, function (response) {
                        layer.msg(response.Msg);
                    });
                };

                $scope.onEditUserRole = function (id) {
                    $scope.EditUserRole.ID = id;
                    $scope.EditUserRole.RoleLists = [];
                    $scope.onGetUserRoles(id);
                    
                    $("#modal-EditUserRole").modal("show");
                };

                $scope.onUserRoleSave = function () {
                    apiUtil.requestWebApi("User/EditUserRoles", "Post", $scope.EditUserRole, function (response) {
                        layer.msg(response.Msg);
                        $scope.onSearch();
                        $("#modal-EditUserRole").modal("hide");
                    }, function (response) {
                        layer.msg(response.Msg);
                        $("#modal-EditUserRole").modal("hide");
                    });
                };

                $scope.onDelete = function (id) {
                    //询问框
                    layer.confirm($translate.instant('msgConfirmDelete'), {
                        btn: ['是', '否'] //按钮
                    }, function () {
                        var data = { ID: id }
                        apiUtil.requestWebApi('User/DeleteAccount', 'Post', data, function (obj) {
                            layer.msg($translate.instant('msgDeleteSuccess'));
                            //刷新数据
                            $scope.onSearch();
                        },
                       function (obj) {
                           layer.msg(obj.Msg);
                       });
                    });
                };
            }
            ]);
        });