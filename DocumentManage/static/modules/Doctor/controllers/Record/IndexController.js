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

                    $scope.exportexcel = function () {
                        var loading = layer.load(0, { shade: [0.1, '#000'] });
                        //请求
                        apiUtil.requestWebApi("Record/Export", "Post", $scope.Record, function (response) {
                            layer.close(loading);
                            if (response.Status == 0) {
                                $("#submitForm").remove();
                                var path = "api/store/download/" + response.Data;

                                var html = '<form method="get" id="submitForm" action="' + path + '" >';
                                html += '<input name="fileName" type="hidden"/>';
                                html += '</form>';

                                $("#exportdiv").append(html);
                                $("#submitForm").submit();
                            }
                            else {
                                layer.msg(response.Msg);
                            }
                        }, function (response) {
                            layer.msg(response.Msg);
                            layer.close(loading);
                        });
                    };

                    $scope.VisitTagOps = ['全国两会', '国宗', '统战部', '政府机构', '访问院校', '其他'];
                    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
                        // 下拉菜单多选
                        $('.selectpicker').selectpicker({
                            size: 'auto',
                            noneSelectedText: '请选择访问标注'
                        })
                    });

                    //患者详细页面
                    $scope.showEdit = function () {
                        $state.go("Index.RecordEdit");
                    }

                    $scope.onDelete = function (id) {
                        //询问框
                        layer.confirm($translate.instant('msgConfirmDelete'), {
                            btn: ['是', '否'] //按钮
                        }, function () {
                            var data = { VisitID: id }
                            apiUtil.requestWebApi('Record/Delete', 'Post', data, function (obj) {
                                layer.msg($translate.instant('msgDeleteSuccess'));
                                //刷新数据
                                $scope.onSearch();
                            },
                           function (obj) {
                               layer.msg(obj.Msg);
                           });
                        });
                    };

                }]);
        });