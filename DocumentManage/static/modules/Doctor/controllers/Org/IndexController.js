"use strict";
define(["module-services-apiUtil", "module-directive-bundling-all"], function (apiUtil) {

            var app = angular.module("myApp", [
             "pascalprecht.translate",
             'ui.router',
             "ui.bootstrap",
             "ngAnimate"]);

            app.controller('OrgController', ['$scope', "$state", '$translate', "$q",
                function ($scope, $state, $translate, $q) {
                    $scope.ListItems = [];
                    $scope.pageSize = 10;
                    $scope.CurrentPage = 1;
                    $scope.totalCount = 0;

                    $scope.Record = { Tag : ''};

                    var countrys = [{
                        Name: '中国', PingYin: 'ZG', Continent: '亚洲',
                        Provinces: [
                            { Name: '北京', PingYin: 'BJ' },
                            { Name: '上海', PingYin: 'SH' },
                        ]
                    },
                    {
                        Name: '美国', PingYin: 'MG', Continent: '美洲',
                        Provinces: [
                            { Name: '洛杉矶', PingYin: 'LSJ' },
                        ]
                    }];

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
                        $scope.Record.Country = $('#Country').val();
                        //请求
                        apiUtil.requestWebApi("Org/GetList", "Post", $scope.Record, function (response) {
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
                        $state.go("Index.OrgEdit");
                    }


                    //患者详细页面
                    $scope.showDetail = function (item) {
                        $state.go("Index.OrgDetail", { OrgID: item.OrgID });
                    }

                    $scope.onCountrySelect = function ($model, $label) {
                        $scope.Record.Country = $model;
                        $scope.Record.Province = '';
                    }

                    $scope.onContinentChange = function ($model, $label) {
                        $scope.Record.Country = '';
                        $scope.Record.Province = '';
                    }

                    //获取字符串长度
                    $scope.GetStringLength = function (str) {
                        var realLength = 0;
                        var charCode = '';

                        for (var i = 0; i < str.length; i++) {
                            charCode = str.charCodeAt(i);
                            if (charCode >= 0 && charCode <= 128)
                                realLength += 1;
                            else
                                realLength += 2;
                        }
                        return realLength;
                    }

                    //获取国家
                    $scope.getCountrys = function (val) {
                        if ($scope.GetStringLength($.trim(val)) > 0) {
                            var deferred = $q.defer();//声明承诺
                            
                            var data = Array();
                            var j = 0;

                            for (var i = 0; i < countrys.length; i++) {
                                if ($scope.Record.Continent != countrys[i].Continent) {
                                    continue;
                                }

                                if (countrys[i].Name.indexOf(val.toUpperCase()) >= 0 ||
                                    countrys[i].PingYin.indexOf(val.toUpperCase()) >= 0) {
                                    data[j] = countrys[i].Name;
                                    j++;
                                }
                            }

                            deferred.resolve(data);//请求成功
                            return deferred.promise;
                        }
                    };
                }]);
        });