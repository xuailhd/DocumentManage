"use strict";
define(["module-services-apiUtil","jquery-validate"], function (apiUtil) {

            var app = angular.module("myApp", [
          "pascalprecht.translate",
          'ui.router',
          "ui.bootstrap",
          "ngAnimate"]);

            app.controller('RecordEditController', [
                '$scope',
                '$http',
                "$q",
                '$location',
                '$state',
                '$translate',
                function ($scope, $http, $q, $location, $state, $translate) {

                    /*************************以下是和服务端交互的数据*****************/

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

                    //获取省份
                    $scope.getProvinces = function (val) {
                        if ($scope.GetStringLength($.trim(val)) > 0) {
                            var deferred = $q.defer();//声明承诺

                            var data = Array();
                            var j = 0;

                            for (var i = 0; i < countrys.length; i++){
                                if ($scope.Record.Country != countrys[i].Name) {
                                    continue;
                                }
                                else {
                                    var provinces = countrys[i].Provinces;
                                    for (var k = 0; k < provinces.length; k++) {
                                        if (provinces[k].Name.indexOf(val.toUpperCase()) >= 0 ||
                                            provinces[k].PingYin.indexOf(val.toUpperCase()) >= 0) {
                                            data[j] = provinces[k].Name;
                                            j++;
                                        }
                                    }
                                }
                            }

                            deferred.resolve(data);//请求成功
                            return deferred.promise;
                        }
                    };

                    $scope.onProvinceSelect = function ($model, $label) {
                        $scope.Record.Province = $model;
                    }

                    $scope.onCountrySelect = function ($model, $label) {
                        $scope.Record.Country = $model;
                        $scope.Record.Province = '';
                    }

                    $scope.onContinentChange = function ($model, $label) {
                        $scope.Record.Country = '';
                        $scope.Record.Province = '';
                    }

                    $scope.onTagChange = function () {
                        switch ($scope.Record.Tag)
                        {
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

                    //获取国家
                    $scope.getCountrys = function (val) {
                        debugger;
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

                    $scope.getWaterNo = function () {
                        var type = 1;
                        if ($scope.Record.FromType == "外方") {
                            type = 2;
                        }

                        //请求
                        apiUtil.requestWebApi("User/GetWaterNo?type=" + type, "Get", null, function (response) {
                            if (response.Status == 0) {
                                $scope.Record.OrgID = response.Data;
                                return;
                            }
                            else {
                                layer.msg(response.Msg);
                            }
                        }, function (response) {
                            layer.msg(response.Msg);
                        });
                    }

                    $scope.onSubmit = function () {
                        //请求
                        apiUtil.requestWebApi("Org/Edit", "Post", $scope.Record, function (response) {
                            if (response.Status == 0) {
                                layer.msg(response.Msg);
                                $state.go("Index.Org");
                                return;
                            }
                            else {
                                layer.msg(response.Msg);
                            }
                        }, function (response) {
                            layer.msg(response.Msg);
                        });
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

                    $scope.GoBack = function () {
                        history.back();
                    };

                    $scope.onLoad = function () {
                        var data = { OrgID: $scope.OrgID };
                        //请求
                        apiUtil.requestWebApi("Org/GetDetail", "Post", data, function (response) {
                            if (response.Status == 0) {
                                $scope.Record = response.Data;
                                $scope.onTagChange();
                                $scope.$apply();
                                return;
                            }
                            else {
                                layer.msg(response.Msg);
                            }
                        }, function (response) {
                            layer.msg(response.Msg);
                        });
                    }

                    if ($state.params.OrgID) {
                        $scope.OrgID = $state.params.OrgID;
                        $scope.onLoad();
                    }
                    else {
                        $scope.Record = { FromType: '外方', Continent: '亚洲', Tag: '' };
                        $scope.onTagChange();
                        $scope.getWaterNo();
                    }
                }
            ]);

        });
