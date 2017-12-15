"use strict";
define(["module-services-apiUtil", "module-Services-uploader", "plugins-extend-date", "bootstrap-select", "jquery-validate", 'module-directive-bundling-all'], function (apiUtil, uploader) {

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

                    $scope.onPersonSelect = function (viewModels, entiyModels) {
                        if (viewModels == undefined || viewModels.length <= 0) {
                            return;
                        }
                        for (var i = 0; i < viewModels.length; i++) {
                            if (viewModels[i].Selected) {
                                var existFlag = false;
                                if (entiyModels != undefined) {
                                    for (var j = 0; j < entiyModels.length; j++) {
                                        if (entiyModels[j].PersonID == viewModels[i].PersonID) {
                                            existFlag = true;
                                            break;
                                        }
                                    }
                                }
                                if (!existFlag) {
                                    entiyModels.push(viewModels[i]);
                                }
                            }
                        }
                    };

                    $scope.MainPersonpageSize = 2;
                    $scope.MainPersonCurrentPage = 1;
                    $scope.MainPersontotalCount = 0;

                    $scope.onGetMainPerson = function () {
                        apiUtil.requestWebApi("Person/GetList", "Post",
                            {
                                UserName: $scope.Record.MainPersonName,
                                PageSize:$scope.MainPersonpageSize,
                                PageIndex: $scope.MainPersonCurrentPage
                            }
                            , function (response) {
                                $scope.mainPersons = response.Data;
                                $scope.MainPersontotalCount = response.Total;
                                //if ($scope.mainPersons && $scope.mainPersons.length > 0) {
                                //    for (var i = 0; i < $scope.mainPersons.length; i++) {
                                //        $scope.mainPersons[i].Selected = false;
                                //    }
                                //}
                                $scope.$apply();
                            }, function () {
                        });
                    };

                    $scope.onAddMianPerson = function () {
                        $scope.onGetMainPerson();
                        $("#modal-selectMainPerson").modal("show");
                    };

                    $scope.onCleanMianPerson = function () {
                        $scope.Record.MianPersonStr = '';
                        if ($scope.mainPersons != undefined && $scope.mainPersons.length > 0) {
                            for (var i = 0; i < $scope.mainPersons.length; i++) {
                                $scope.mainPersons[i].Selected = false;
                            }
                        }
                        $scope.Record.MianPersons = [];
                    };

                    $scope.onMainPersonSelect = function () {
                        if ($scope.Record.MianPersons == undefined) {
                            $scope.Record.MianPersons = [];
                        }
                        $scope.onPersonSelect($scope.mainPersons, $scope.Record.MianPersons);

                        $scope.Record.MianPersonStr = '';
                        if ($scope.Record.MianPersons && $scope.Record.MianPersons.length > 0) {
                            for (var i = 0; i < $scope.Record.MianPersons.length; i++) {
                                $scope.Record.MianPersonStr += $scope.Record.MianPersons[i].NameCN + ',';
                            }
                        }
                        $("#modal-selectMainPerson").modal("hide");
                    };


                    $scope.OurPersonpageSize = 10;
                    $scope.OurPersonCurrentPage = 1;
                    $scope.OurPersontotalCount = 0;

                    $scope.onGetOurPerson = function () {
                        apiUtil.requestWebApi("Person/GetList", "Post",
                            {
                                UserName: $scope.Record.OurPersonName,
                                PageSize: $scope.OurPersonpageSize,
                                PageIndex: $scope.OurPersonCurrentPage
                            }
                            , function (response) {
                                $scope.ourPersons = response.Data;
                                $scope.OurPersontotalCount = response.Total;
                                $scope.$apply();
                            }, function () {
                            });
                    };

                    $scope.onAddOurPerson = function () {
                        $scope.onGetOurPerson();
                        $("#modal-selectOurPerson").modal("show");
                    };

                    $scope.onCleanOurPerson = function () {
                        $scope.Record.OurPersonStr = '';
                        if ($scope.ourPersons != undefined && $scope.ourPersons.length > 0) {
                            for (var i = 0; i < $scope.ourPersons.length; i++) {
                                $scope.ourPersons[i].Selected = false;
                            }
                        }
                        $scope.Record.OurPersons = [];
                    };

                    $scope.onOurPersonSelect = function () {
                        if ($scope.Record.OurPersons == undefined) {
                            $scope.Record.OurPersons = [];
                        }
                        $scope.onPersonSelect($scope.ourPersons, $scope.Record.OurPersons);

                        $scope.Record.OurPersonStr = '';
                        if ($scope.Record.OurPersons && $scope.Record.OurPersons.length > 0) {
                            for (var i = 0; i < $scope.Record.OurPersons.length; i++) {
                                $scope.Record.OurPersonStr += $scope.Record.OurPersons[i].NameCN + ',';
                            }
                        }
                        $("#modal-selectOurPerson").modal("hide");
                    };

                    $scope.OurOtherPersonpageSize = 10;
                    $scope.OurOtherPersonCurrentPage = 1;
                    $scope.OurOtherPersontotalCount = 0;

                    $scope.onGetOurOtherPerson = function () {
                        apiUtil.requestWebApi("Person/GetList", "Post",
                            {
                                UserName: $scope.Record.OurOtherPersonName,
                                PageSize: $scope.OurOtherPersonpageSize,
                                PageIndex: $scope.OurOtherPersonCurrentPage
                            }
                            , function (response) {
                                $scope.ourOtherPersons = response.Data;
                                $scope.OurOtherPersontotalCount = response.Total;
                                $scope.$apply();
                            }, function () {
                            });
                    };

                    $scope.onAddOurOtherPerson = function () {
                        $scope.onGetOurOtherPerson();
                        $("#modal-selectOurOtherPerson").modal("show");
                    };

                    $scope.onCleanOurOtherPerson = function () {
                        $scope.Record.OurOtherPersonStr = '';
                        if ($scope.ourOtherPersons != undefined && $scope.ourOtherPersons.length > 0) {
                            for (var i = 0; i < $scope.ourOtherPersons.length; i++) {
                                $scope.ourOtherPersons[i].Selected = false;
                            }
                        }
                        $scope.Record.OurOtherPersons = [];
                    };

                    $scope.onOurOtherPersonSelect = function () {
                        if ($scope.Record.OurOtherPersons == undefined) {
                            $scope.Record.OurOtherPersons = [];
                        }
                        $scope.onPersonSelect($scope.ourOtherPersons, $scope.Record.OurOtherPersons);

                        $scope.Record.OurOtherPersonStr = '';
                        if ($scope.Record.OurOtherPersons && $scope.Record.OurOtherPersons.length > 0) {
                            for (var i = 0; i < $scope.Record.OurOtherPersons.length; i++) {
                                $scope.Record.OurOtherPersonStr += $scope.Record.OurOtherPersons[i].NameCN + ',';
                            }
                        }
                        $("#modal-selectOurOtherPerson").modal("hide");
                    };

                    $scope.TheyPersonpageSize = 10;
                    $scope.TheyPersonCurrentPage = 1;
                    $scope.TheyPersontotalCount = 0;

                    $scope.onGetTheyPerson = function () {
                        apiUtil.requestWebApi("Person/GetList", "Post",
                            {
                                UserName: $scope.Record.TheyPersonName,
                                PageSize: $scope.TheyPersonpageSize,
                                PageIndex: $scope.TheyPersonCurrentPage
                            }
                            , function (response) {
                                $scope.theyPersons = response.Data;
                                $scope.TheyPersontotalCount = response.Total;
                                $scope.$apply();
                            }, function () {
                            });
                    };

                    $scope.onAddTheyPerson = function () {
                        $scope.onGetTheyPerson();
                        $("#modal-selectTheyPerson").modal("show");
                    };

                    $scope.onCleanTheyPerson = function () {
                        $scope.Record.TheyPersonStr = '';
                        if ($scope.theyPersons != undefined && $scope.theyPersons.length > 0) {
                            for (var i = 0; i < $scope.theyPersons.length; i++) {
                                $scope.theyPersons[i].Selected = false;
                            }
                        }
                        $scope.Record.TheyPersons = [];
                    };

                    $scope.onTheyPersonSelect = function () {
                        if ($scope.Record.TheyPersons == undefined) {
                            $scope.Record.TheyPersons = [];
                        }
                        $scope.onPersonSelect($scope.theyPersons, $scope.Record.TheyPersons);

                        $scope.Record.TheyPersonStr = '';
                        if ($scope.Record.TheyPersons && $scope.Record.TheyPersons.length > 0) {
                            for (var i = 0; i < $scope.Record.TheyPersons.length; i++) {
                                $scope.Record.TheyPersonStr += $scope.Record.TheyPersons[i].NameCN + ',';
                            }
                        }
                        $("#modal-selectTheyPerson").modal("hide");
                    };

                    $scope.TheyOtherPersonpageSize = 10;
                    $scope.TheyOtherPersonCurrentPage = 1;
                    $scope.TheyOtherPersontotalCount = 0;

                    $scope.onGetTheyOtherPerson = function () {
                        apiUtil.requestWebApi("Person/GetList", "Post",
                            {
                                UserName: $scope.Record.TheyOtherPersonName,
                                PageSize: $scope.TheyOtherPersonpageSize,
                                PageIndex: $scope.TheyOtherPersonCurrentPage
                            }
                            , function (response) {
                                $scope.theyOtherPersons = response.Data;
                                $scope.TheyOtherPersontotalCount = response.Total;
                                $scope.$apply();
                            }, function () {
                            });
                    };

                    $scope.onAddTheyOtherPerson = function () {
                        $scope.onGetTheyOtherPerson();
                        $("#modal-selectTheyOtherPerson").modal("show");
                    };

                    $scope.onCleanTheyOtherPerson = function () {
                        $scope.Record.TheyOtherPersonStr = '';
                        if ($scope.theyOtherPersons != undefined && $scope.theyOtherPersons.length > 0) {
                            for (var i = 0; i < $scope.theyOtherPersons.length; i++) {
                                $scope.theyOtherPersons[i].Selected = false;
                            }
                        }
                        $scope.Record.TheyOtherPersons = [];
                    };

                    $scope.onTheyOtherPersonSelect = function () {
                        if ($scope.Record.TheyOtherPersons == undefined) {
                            $scope.Record.TheyOtherPersons = [];
                        }
                        $scope.onPersonSelect($scope.theyOtherPersons, $scope.Record.TheyOtherPersons);

                        $scope.Record.TheyOtherPersonStr = '';
                        if ($scope.Record.TheyOtherPersons && $scope.Record.TheyOtherPersons.length > 0) {
                            for (var i = 0; i < $scope.Record.TheyOtherPersons.length; i++) {
                                $scope.Record.TheyOtherPersonStr += $scope.Record.TheyOtherPersons[i].NameCN + ',';
                            }
                        }
                        $("#modal-selectTheyOtherPerson").modal("hide");
                    };


                    $scope.onOrgSelect = function (viewModels, entiyModels) {
                        if (viewModels == undefined || viewModels.length <= 0) {
                            return;
                        }
                        for (var i = 0; i < viewModels.length; i++) {
                            if (viewModels[i].Selected) {
                                var existFlag = false;
                                if (entiyModels != undefined) {
                                    for (var j = 0; j < entiyModels.length; j++) {
                                        if (entiyModels[j].OrgID == viewModels[i].OrgID) {
                                            existFlag = true;
                                            break;
                                        }
                                    }
                                }
                                if (!existFlag) {
                                    entiyModels.push(viewModels[i]);
                                }
                            }
                        }
                    };

                    $scope.OurOrgpageSize = 10;
                    $scope.OurOrgCurrentPage = 1;
                    $scope.OurOrgtotalCount = 0;

                    $scope.onGetOurOrg = function () {
                        apiUtil.requestWebApi("Org/GetList", "Post",
                            {
                                OrgName: $scope.Record.OurOrgName,
                                PageSize: $scope.OurOrgpageSize,
                                PageIndex: $scope.OurOrgCurrentPage
                            }
                            , function (response) {
                                $scope.ourOrgs = response.Data;
                                $scope.OurOrgtotalCount = response.Total;
                                $scope.$apply();
                            }, function () {
                            });
                    };

                    $scope.onAddOurOrg = function () {
                        $scope.onGetOurOrg();
                        $("#modal-selectOurOrg").modal("show");
                    };

                    $scope.onCleanOurOrg = function () {
                        $scope.Record.OurOrgStr = '';
                        if ($scope.ourOrgs != undefined && $scope.ourOrgs.length > 0) {
                            for (var i = 0; i < $scope.ourOrgs.length; i++) {
                                $scope.ourOrgs[i].Selected = false;
                            }
                        }
                        $scope.Record.OurOrgs = [];
                    };

                    $scope.onOurOrgSelect = function () {
                        if ($scope.Record.OurOrgs == undefined) {
                            $scope.Record.OurOrgs = [];
                        }
                        $scope.onOrgSelect($scope.ourOrgs, $scope.Record.OurOrgs);

                        $scope.Record.OurOrgStr = '';
                        if ($scope.Record.OurOrgs && $scope.Record.OurOrgs.length > 0) {
                            for (var i = 0; i < $scope.Record.OurOrgs.length; i++) {
                                $scope.Record.OurOrgStr += $scope.Record.OurOrgs[i].OrgName + ',';
                            }
                        }
                        $("#modal-selectOurOrg").modal("hide");
                    };

                    $scope.TheyOrgpageSize = 10;
                    $scope.TheyOrgCurrentPage = 1;
                    $scope.TheyOrgtotalCount = 0;

                    $scope.onGetTheyOrg = function () {
                        apiUtil.requestWebApi("Org/GetList", "Post",
                            {
                                OrgName: $scope.Record.TheyOrgName,
                                PageSize: $scope.TheyOrgpageSize,
                                PageIndex: $scope.TheyOrgCurrentPage
                            }
                            , function (response) {
                                $scope.theyOrgs = response.Data;
                                $scope.TheyOrgtotalCount = response.Total;
                                $scope.$apply();
                            }, function () {
                            });
                    };

                    $scope.onAddTheyOrg = function () {
                        $scope.onGetTheyOrg();
                        $("#modal-selectTheyOrg").modal("show");
                    };

                    $scope.onCleanTheyOrg = function () {
                        $scope.Record.TheyOrgStr = '';
                        if ($scope.theyOrgs != undefined && $scope.theyOrgs.length > 0) {
                            for (var i = 0; i < $scope.theyOrgs.length; i++) {
                                $scope.theyOrgs[i].Selected = false;
                            }
                        }
                        $scope.Record.TheyOrgs = [];
                    };

                    $scope.onTheyOrgSelect = function () {
                        if ($scope.Record.TheyOrgs == undefined) {
                            $scope.Record.TheyOrgs = [];
                        }
                        $scope.onOrgSelect($scope.theyOrgs, $scope.Record.TheyOrgs);

                        $scope.Record.TheyOrgStr = '';
                        if ($scope.Record.TheyOrgs && $scope.Record.TheyOrgs.length > 0) {
                            for (var i = 0; i < $scope.Record.TheyOrgs.length; i++) {
                                $scope.Record.TheyOrgStr += $scope.Record.TheyOrgs[i].OrgName + ',';
                            }
                        }
                        $("#modal-selectTheyOrg").modal("hide");
                    };

                    $scope.BeViOrgpageSize = 10;
                    $scope.BeViOrgCurrentPage = 1;
                    $scope.BeViOrgtotalCount = 0;

                    $scope.onGetBeViOrg = function () {
                        apiUtil.requestWebApi("Org/GetList", "Post",
                            {
                                OrgName: $scope.Record.BeViOrgName,
                                PageSize: $scope.BeViOrgpageSize,
                                PageIndex: $scope.BeViOrgCurrentPage
                            }
                            , function (response) {
                                $scope.beViOrgs = response.Data;
                                $scope.BeViOrgtotalCount = response.Total;
                                $scope.$apply();
                            }, function () {
                            });
                    };

                    $scope.onAddBeViOrg = function () {
                        $scope.onGetBeViOrg();
                        $("#modal-selectBeViOrg").modal("show");
                    };

                    $scope.onCleanBeViOrg = function () {
                        $scope.Record.BeViOrgStr = '';
                        if ($scope.beViOrgs != undefined && $scope.beViOrgs.length > 0) {
                            for (var i = 0; i < $scope.beViOrgs.length; i++) {
                                $scope.beViOrgs[i].Selected = false;
                            }
                        }
                        $scope.Record.BeViOrgs = [];
                    };

                    $scope.onBeViOrgSelect = function () {
                        if ($scope.Record.BeViOrgs == undefined) {
                            $scope.Record.BeViOrgs = [];
                        }
                        $scope.onOrgSelect($scope.beViOrgs, $scope.Record.BeViOrgs);

                        $scope.Record.BeViOrgStr = '';
                        if ($scope.Record.BeViOrgs && $scope.Record.BeViOrgs.length > 0) {
                            for (var i = 0; i < $scope.Record.BeViOrgs.length; i++) {
                                $scope.Record.BeViOrgStr += $scope.Record.BeViOrgs[i].OrgName + ',';
                            }
                        }
                        $("#modal-selectBeViOrg").modal("hide");
                    };


                    $scope.delSJWLUrl = function (fileUrl, idindex) {
                        var index = -1;
                        for (var i = 0; i < $scope.Record.SJWLFiles.length; i++) {
                            if ($scope.Record.SJWLFiles[i].FileUrl == fileUrl) {
                                index = i;
                                break;
                            }
                        }
                        if (index >= 0) {
                            $scope.Record.SJWLFiles.splice(index, 1);
                            $('#SJWLFile' + idindex).remove();
                        }
                    };

                    $(document).on("change", "#SJWLUrl_file", function (event) {
                        var uploadFile = event.currentTarget
                        //触发文件上传
                        uploader.onFileUpload(uploadFile.files[0], function (params, process) {
                            //执行
                            process(function (uploadResp) {
                                //上传成功
                                var flag = false;
                                if ($scope.Record.SJWLFiles == undefined) {
                                    $scope.Record.SJWLFiles = [];
                                }

                                for (var i = 0; i < $scope.Record.SJWLFiles.length; i++) {
                                    if ($scope.Record.SJWLFiles[i].FileUrl == uploadResp.Data) {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag) {
                                    $scope.Record.SJWLFiles.push({
                                        FileName: uploadFile.files[0].name, FileUrl: uploadResp.Data
                                    });
                                    $scope.$apply();
                                }
                            }, function (resp) {
                                console.log(resp.Msg);
                            })

                        });
                    });

                    $scope.delLBWLUrl = function (fileUrl, idindex) {
                        var index = -1;
                        for (var i = 0; i < $scope.Record.LBWLFiles.length; i++) {
                            if ($scope.Record.LBWLFiles[i].FileUrl == fileUrl) {
                                index = i;
                                break;
                            }
                        }
                        if (index >= 0) {
                            $scope.Record.LBWLFiles.splice(index, 1);
                            $('#LBWLFile' + idindex).remove();
                        }
                    };

                    $(document).on("change", "#LBWLUrl_file", function (event) {
                        var uploadFile = event.currentTarget
                        //触发文件上传
                        uploader.onFileUpload(uploadFile.files[0], function (params, process) {
                            //执行
                            process(function (uploadResp) {
                                //上传成功
                                var flag = false;
                                if ($scope.Record.LBWLFiles == undefined) {
                                    $scope.Record.LBWLFiles = [];
                                }

                                for (var i = 0; i < $scope.Record.LBWLFiles.length; i++) {
                                    if ($scope.Record.LBWLFiles[i].FileUrl == uploadResp.Data) {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag) {
                                    $scope.Record.LBWLFiles.push({
                                        FileName: uploadFile.files[0].name, FileUrl: uploadResp.Data
                                    });
                                    $scope.$apply();
                                }
                            }, function (resp) {
                                console.log(resp.Msg);
                            })

                        });
                    });

                    $scope.delNBGLUrl = function (fileUrl, idindex) {
                        var index = -1;
                        for (var i = 0; i < $scope.Record.NBGLFiles.length; i++) {
                            if ($scope.Record.NBGLFiles[i].FileUrl == fileUrl) {
                                index = i;
                                break;
                            }
                        }
                        if (index >= 0) {
                            $scope.Record.NBGLFiles.splice(index, 1);
                            $('#NBGLFile' + idindex).remove();
                        }
                    };

                    $(document).on("change", "#NBGLUrl_file", function (event) {
                        var uploadFile = event.currentTarget
                        //触发文件上传
                        uploader.onFileUpload(uploadFile.files[0], function (params, process) {
                            //执行
                            process(function (uploadResp) {
                                //上传成功
                                var flag = false;
                                if ($scope.Record.NBGLFiles == undefined) {
                                    $scope.Record.NBGLFiles = [];
                                }

                                for (var i = 0; i < $scope.Record.NBGLFiles.length; i++) {
                                    if ($scope.Record.NBGLFiles[i].FileUrl == uploadResp.Data) {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag) {
                                    $scope.Record.NBGLFiles.push({
                                        FileName: uploadFile.files[0].name, FileUrl: uploadResp.Data
                                    });
                                    $scope.$apply();
                                }
                            }, function (resp) {
                                console.log(resp.Msg);
                            })

                        });
                    });

                    $scope.delHYXGUrl = function (fileUrl, idindex) {
                        var index = -1;
                        for (var i = 0; i < $scope.Record.HYXGFiles.length; i++) {
                            if ($scope.Record.HYXGFiles[i].FileUrl == fileUrl) {
                                index = i;
                                break;
                            }
                        }
                        if (index >= 0) {
                            $scope.Record.HYXGFiles.splice(index, 1);
                            $('#HYXGFile' + idindex).remove();
                        }
                    };

                    $(document).on("change", "#HYXGUrl_file", function (event) {
                        var uploadFile = event.currentTarget
                        //触发文件上传
                        uploader.onFileUpload(uploadFile.files[0], function (params, process) {
                            //执行
                            process(function (uploadResp) {
                                //上传成功
                                var flag = false;
                                if ($scope.Record.HYXGFiles == undefined) {
                                    $scope.Record.HYXGFiles = [];
                                }

                                for (var i = 0; i < $scope.Record.HYXGFiles.length; i++) {
                                    if ($scope.Record.HYXGFiles[i].FileUrl == uploadResp.Data) {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag) {
                                    $scope.Record.HYXGFiles.push({
                                        FileName: uploadFile.files[0].name, FileUrl: uploadResp.Data
                                    });
                                    $scope.$apply();
                                }
                            }, function (resp) {
                                console.log(resp.Msg);
                            })

                        });
                    });

                    $scope.delNewsUrl = function (fileUrl, idindex) {
                        var index = -1;
                        for (var i = 0; i < $scope.Record.NewsFiles.length; i++) {
                            if ($scope.Record.NewsFiles[i].FileUrl == fileUrl) {
                                index = i;
                                break;
                            }
                        }
                        if (index >= 0) {
                            $scope.Record.NewsFiles.splice(index, 1);
                            $('#NewsFile' + idindex).remove();
                        }
                    };

                    $(document).on("change", "#NewsUrl_file", function (event) {
                        var uploadFile = event.currentTarget
                        //触发文件上传
                        uploader.onFileUpload(uploadFile.files[0], function (params, process) {
                            //执行
                            process(function (uploadResp) {
                                //上传成功
                                var flag = false;
                                if ($scope.Record.NewsFiles == undefined) {
                                    $scope.Record.NewsFiles = [];
                                }

                                for (var i = 0; i < $scope.Record.NewsFiles.length; i++) {
                                    if ($scope.Record.NewsFiles[i].FileUrl == uploadResp.Data) {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag) {
                                    $scope.Record.NewsFiles.push({
                                        FileName: uploadFile.files[0].name, FileUrl: uploadResp.Data
                                    });
                                    $scope.$apply();
                                }
                            }, function (resp) {
                                console.log(resp.Msg);
                            })

                        });
                    });

                    $scope.delOtherUrl = function (fileUrl, idindex) {
                        var index = -1;
                        for (var i = 0; i < $scope.Record.OtherFiles.length; i++) {
                            if ($scope.Record.OtherFiles[i].FileUrl == fileUrl) {
                                index = i;
                                break;
                            }
                        }
                        if (index >= 0) {
                            $scope.Record.OtherFiles.splice(index, 1);
                            $('#OtherFile' + idindex).remove();
                        }
                    };

                    $(document).on("change", "#OtherUrl_file", function (event) {
                        var uploadFile = event.currentTarget
                        //触发文件上传
                        uploader.onFileUpload(uploadFile.files[0], function (params, process) {
                            //执行
                            process(function (uploadResp) {
                                //上传成功
                                var flag = false;
                                if ($scope.Record.OtherFiles == undefined) {
                                    $scope.Record.OtherFiles = [];
                                }

                                for (var i = 0; i < $scope.Record.OtherFiles.length; i++) {
                                    if ($scope.Record.OtherFiles[i].FileUrl == uploadResp.Data) {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag) {
                                    $scope.Record.OtherFiles.push({
                                        FileName: uploadFile.files[0].name, FileUrl: uploadResp.Data
                                    });
                                    $scope.$apply();
                                }
                            }, function (resp) {
                                console.log(resp.Msg);
                            })

                        });
                    });

                    
                    $scope.onAddVisitDetail = function () {
                        if ($scope.Record.VisitDetails == undefined) {
                            $scope.Record.VisitDetails = [];
                        }
                        $scope.Record.VisitDetails.push({ No: $scope.DetailNo });
                        $scope.DetailNo = $scope.DetailNo + 1;
                    }

                    $scope.onRemoveVisitDetail = function (no) {
                        var index = -1;
                        for (var i = 0; i < $scope.Record.VisitDetails.length; i++) {
                            if (parseInt($scope.Record.VisitDetails[i].No) == parseInt(no)) {
                                index = i;
                            }
                            else if (parseInt($scope.Record.VisitDetails[i].No) > parseInt(no)) {
                                $scope.Record.VisitDetails[i].No = parseInt($scope.Record.VisitDetails[i].No) - 1;
                            }
                        }
                        $scope.Record.VisitDetails.splice(index, 1);
                        $scope.checkDate();
                    }

                    $scope.checkDate = function () {
                        if (!$scope.Record.VisitDetails || $scope.Record.VisitDetails.length <= 0) {
                            return;
                        }
                        $scope.Record.FromDate = $scope.Record.VisitDetails[0].FromDate.format('yyyy-MM-dd');
                        $scope.Record.EndDate = $scope.Record.VisitDetails[0].EndDate.format('yyyy-MM-dd');

                        if ($scope.Record.VisitDetails.length < 2) {
                            return;
                        }

                        for (var i = 1; i < $scope.Record.VisitDetails.length; i++) {
                            var datestr = $scope.Record.VisitDetails[i].FromDate.format('yyyy-MM-dd');
                            if (datestr < $scope.Record.FromDate) {
                                $scope.Record.FromDate = datestr;
                            }

                            datestr = $scope.Record.VisitDetails[i].EndDate.format('yyyy-MM-dd');
                            if (datestr > $scope.Record.EndDate) {
                                $scope.Record.EndDate = datestr;
                            }
                        }
                    }

                    $scope.onFromChange = function (index) {
                        if ($scope.Record.VisitDetails[index].FromDate.format('yyyy-MM-dd') > $scope.Record.VisitDetails[index].EndDate.format('yyyy-MM-dd')) {
                            $scope.Record.VisitDetails[index].FromDate = $scope.Record.VisitDetails[index].EndDate;
                        }

                        $scope.checkDate();
                    }

                    $scope.onEndChange = function (index) {
                        if ($scope.Record.VisitDetails[index].EndDate.format('yyyy-MM-dd') < $scope.Record.VisitDetails[index].FromDate.format('yyyy-MM-dd')) {
                            $scope.Record.VisitDetails[index].EndDate = $scope.Record.VisitDetails[index].FromDate;
                        }

                        $scope.checkDate();
                    }

                    $scope.getWaterNo = function () {
                        var type = 5;
                        if ($scope.Record.VisitType == "来访") {
                            type = 6;
                        }
                        //请求
                        apiUtil.requestWebApi("User/GetWaterNo?type=" + type, "Get", null, function (response) {
                            if (response.Status == 0) {
                                $scope.Record.VisitID = response.Data;
                                return;
                            }
                            else {
                                layer.msg(response.Msg);
                            }
                        }, function (response) {
                            layer.msg(response.Msg);
                        });
                    };

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
                    };

                    $scope.VisitTagOps = ['全国两会', '国宗', '统战部', '政府机构', '访问院校', '其他'];
                    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
                        // 下拉菜单多选
                        $('.selectpicker').selectpicker({
                            size: 'auto',
                            noneSelectedText: '请选择访问标注'
                        })
                    });

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
                                        $scope.Record.VisitDetails[i].FromDate = $scope.Record.VisitDetails[i].FromDate.toDate();
                                        $scope.Record.VisitDetails[i].EndDate = $scope.Record.VisitDetails[i].EndDate.toDate();
                                    }
                                    $scope.DetailNo = $scope.Record.VisitDetails.length + 1;
                                }
                                else {
                                    $scope.DetailNo = 1;
                                }
                                
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

                    $scope.onSubmit = function () {
                        if (!$("#myForm").valid()) {
                            return;
                        }
                        if (!$("#myForm1").valid()) {
                            return;
                        }

                        //请求
                        apiUtil.requestWebApi("Record/Edit", "Post", $scope.Record, function (response) {
                            if (response.Status == 0) {
                                layer.msg(response.Msg);
                                $state.go("Index.Record");
                                return;
                            }
                            else {
                                layer.msg(response.Msg);
                            }
                        }, function (response) {
                            layer.msg(response.Msg);
                        });
                    }

                    $scope.GoBack = function () {
                        history.back();
                    };

                    if ($state.params.VisitID) {
                        $scope.VisitID = $state.params.VisitID;
                        $scope.onLoad();
                    }
                    else {
                        $scope.DetailNo = 1;
                        $scope.Record = {
                            VisitType: '来访'
                        };
                        $scope.getWaterNo();
                    }
                }
            ]);

            app.directive('onFinishRenderFilters', ["$timeout", function ($timeout) {
                return {
                    restrict: 'A',
                    link: function (scope, element, attr) {
                        if (scope.$last === true) {
                            $timeout(function () {
                                scope.$emit('ngRepeatFinished');
                            });
                        }
                    }
                };
            }]);

        });
