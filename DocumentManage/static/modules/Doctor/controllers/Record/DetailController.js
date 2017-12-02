"use strict";
define(["module-services-apiUtil", "jquery-validate"], function (apiUtil) {

    var app = angular.module("myApp", [
        "pascalprecht.translate",
        'ui.router',
        "ui.bootstrap",
        "ngAnimate"]);

    app.controller('RecordDetailController', [
        '$scope',
        '$http',
        "$q",
        '$location',
        '$state',
        '$translate',
        function ($scope, $http, $q, $location, $state, $translate) {

            /*************************以下是和服务端交互的数据*****************/
            $scope.VisitID = $state.params.VisitID;


            $scope.onLoad = function () {
                var data = { VisitID: $scope.VisitID };
                //请求
                apiUtil.requestWebApi("Record/GetDetail", "Post", data, function (response) {
                    if (response.Status == 0) {
                        $scope.Record = response.Data;

                        if ($scope.Record.MianPersons && $scope.Record.MianPersons.length > 0) {
                            $scope.Record.MianPersonStr = '';
                            for (var i = 0; i < $scope.Record.MianPersons.length; i++) {
                                $scope.Record.MianPersonStr += $scope.Record.MianPersons[i].NameCN + ',';
                            }
                        }

                        if ($scope.Record.OurPersons && $scope.Record.OurPersons.length > 0) {
                            $scope.Record.OurPersonStr = '';
                            for (var i = 0; i < $scope.Record.OurPersons.length; i++) {
                                $scope.Record.OurPersonStr += $scope.Record.OurPersons[i].NameCN + ',';
                            }
                        }

                        if ($scope.Record.OurOtherPersons && $scope.Record.OurOtherPersons.length > 0) {
                            $scope.Record.OurOtherPersonStr = '';
                            for (var i = 0; i < $scope.Record.OurOtherPersons.length; i++) {
                                $scope.Record.OurOtherPersonStr += $scope.Record.OurOtherPersons[i].NameCN + ',';
                            }
                        }

                        if ($scope.Record.TheyPersons && $scope.Record.TheyPersons.length > 0) {
                            $scope.Record.TheyPersonStr = '';
                            for (var i = 0; i < $scope.Record.TheyPersons.length; i++) {
                                $scope.Record.TheyPersonStr += $scope.Record.TheyPersons[i].NameCN + ',';
                            }
                        }

                        if ($scope.Record.TheyOtherPersons && $scope.Record.TheyOtherPersons.length > 0) {
                            $scope.Record.TheyOtherPersonStr = '';
                            for (var i = 0; i < $scope.Record.TheyOtherPersons.length; i++) {
                                $scope.Record.TheyOtherPersonStr += $scope.Record.TheyOtherPersons[i].NameCN + ',';
                            }
                        }

                        if ($scope.Record.OurOrgs && $scope.Record.OurOrgs.length > 0) {
                            $scope.Record.OurOrgStr = '';
                            for (var i = 0; i < $scope.Record.OurOrgs.length; i++) {
                                $scope.Record.OurOrgStr += $scope.Record.OurOrgs[i].OrgName + ',';
                            }
                        }

                        if ($scope.Record.TheyOrgs && $scope.Record.TheyOrgs.length > 0) {
                            $scope.Record.TheyOrgStr = '';
                            for (var i = 0; i < $scope.Record.TheyOrgs.length; i++) {
                                $scope.Record.TheyOrgStr += $scope.Record.TheyOrgs[i].OrgName + ',';
                            }
                        }

                        if ($scope.Record.BeViOrgs && $scope.Record.BeViOrgs.length > 0) {
                            $scope.Record.BeViOrgStr = '';
                            for (var i = 0; i < $scope.Record.BeViOrgs.length; i++) {
                                $scope.Record.BeViOrgStr += $scope.Record.BeViOrgs[i].OrgName + ',';
                            }
                        }

                        if ($scope.Record.VisitDetails && $scope.Record.VisitDetails.length > 0) {
                            for (var i = 0; i < $scope.Record.VisitDetails.length; i++) {
                                $scope.Record.VisitDetails[i].FromDate = $scope.Record.VisitDetails[i].FromDate.toDate().format('yyyy-MM-dd');
                                $scope.Record.VisitDetails[i].EndDate = $scope.Record.VisitDetails[i].EndDate.toDate().format('yyyy-MM-dd');
                            }
                            $scope.DetailNo = $scope.Record.VisitDetails.length + 1;
                        }
                        else {
                            $scope.DetailNo = 1;
                        }

                        $scope.Record.VisitTags = $scope.Record.VisitTags.toString();
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

            $scope.onLoad();


            $scope.GoBack = function () {
                history.back();
            };
        }
    ]);

});
