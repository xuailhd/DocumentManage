"use strict";
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
                            $scope.onSearch();
                            $("#modal-EditRole").modal("hide");
                        }, function (response) {
                            layer.msg(response.Msg);
                            $("#modal-EditRole").modal("hide");
                        });
                    }
                };

                $scope.onGetRoleAuth = function (id) {
                    apiUtil.requestWebApi("User/GetAuthList", "Post", { ID: id, Type: 0 }, function (response) {
                        $scope.EditRoleAuth.AuthLists = response.Data;
                        $scope.$apply();
                    }, function (response) {
                        layer.msg(response.Msg);
                    });
                };

                $scope.onEditAuth = function (id) {
                    $scope.EditRoleAuth = {};
                    $scope.EditRoleAuth.ID = id;
                    $scope.EditRoleAuth.AuthLists = [];
                    $scope.EditRoleAuth.Type = 0;
                    $scope.onGetRoleAuth(id);

                    $("#modal-EditRoleAuth").modal("show");
                };

                $scope.onRoleAuthSave = function () {
                    apiUtil.requestWebApi("User/EditRoleAuths", "Post", $scope.EditRoleAuth, function (response) {
                        layer.msg(response.Msg);
                        $scope.onSearch();
                        $("#modal-EditRoleAuth").modal("hide");
                    }, function (response) {
                        layer.msg(response.Msg);
                        $("#modal-EditRoleAuth").modal("hide");
                    });
                };

                $scope.onGetRoleFN = function (id) {
                    apiUtil.requestWebApi("User/GetAuthList", "Post", { ID: id, Type: 1 }, function (response) {
                        $scope.EditRoleFN.AuthLists = response.Data;
                        $scope.$apply();
                    }, function (response) {
                        layer.msg(response.Msg);
                    });
                };

                $scope.onEditFN = function (id) {
                    $scope.EditRoleFN = {};
                    $scope.EditRoleFN.ID = id;
                    $scope.EditRoleFN.AuthLists = [];
                    $scope.EditRoleFN.Type = 1;
                    $scope.onGetRoleFN(id);

                    $("#modal-EditRoleFN").modal("show");
                };

                $scope.onRoleFNSave = function () {
                    apiUtil.requestWebApi("User/EditRoleAuths", "Post", $scope.EditRoleFN, function (response) {
                        layer.msg(response.Msg);
                        $scope.onSearch();
                        $("#modal-EditRoleFN").modal("hide");
                    }, function (response) {
                        layer.msg(response.Msg);
                        $("#modal-EditRoleFN").modal("hide");
                    });
                };

                $scope.onGetRoleLE = function (id) {
                    apiUtil.requestWebApi("User/GetAuthList", "Post", { ID: id, Type: 2 }, function (response) {
                        $scope.EditRoleLE.AuthLists = response.Data;
                        $scope.$apply();
                    }, function (response) {
                        layer.msg(response.Msg);
                    });
                };

                $scope.onEditLE= function (id) {
                    $scope.EditRoleLE = {};
                    $scope.EditRoleLE.ID = id;
                    $scope.EditRoleLE.AuthLists = [];
                    $scope.EditRoleLE.Type = 2;
                    $scope.onGetRoleLE(id);

                    $("#modal-EditRoleLE").modal("show");
                };

                $scope.onRoleLESave = function () {
                    apiUtil.requestWebApi("User/EditRoleAuths", "Post", $scope.EditRoleLE, function (response) {
                        layer.msg(response.Msg);
                        $scope.onSearch();
                        $("#modal-EditRoleLE").modal("hide");
                    }, function (response) {
                        layer.msg(response.Msg);
                        $("#modal-EditRoleLE").modal("hide");
                    });
                };

                $scope.onDelete = function (id) {
                    //询问框
                    layer.confirm($translate.instant('msgConfirmDelete'), {
                        btn: ['是', '否'] //按钮
                    }, function () {
                        var data = { ID: id }
                        apiUtil.requestWebApi('User/DeleteRole', 'Post', data, function (obj) {
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