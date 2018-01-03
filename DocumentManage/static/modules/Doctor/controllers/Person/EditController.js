"use strict";
define(["module-services-apiUtil", "module-Services-uploader", "plugins-extend-date", "plugins-extend-date"
    , "jquery-validate","module-directive-bundling-all"], function (apiUtil, uploader) {

            var app = angular.module("myApp", [
          "pascalprecht.translate",
          'ui.router',
          "ui.bootstrap",
          "ngAnimate"]);

            app.controller('PersonEditController', [
                '$scope',
                '$http',
                "$q",
                '$location',
                '$state',
                '$translate',
                function ($scope, $http, $q, $location, $state, $translate) {

                    /*************************以下是和服务端交互的数据*****************/
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

                    //获取机构
                    $scope.getOrgs = function (val) {
                        if ($scope.GetStringLength($.trim(val)) > 0) {
                            var deferred = $q.defer();//声明承诺

                            apiUtil.requestWebApi("Org/GetList", "Post", { OrgName: val }, function (response) {
                                deferred.resolve(response.Data);//请求成功

                            }, function () {

                                deferred.reject([]); //请求失败
                            });

                            return deferred.promise;
                        }
                    };

                    $scope.onOrgSelect = function ($model, $label) {
                        $scope.Record.OrgName = $model.OrgName;
                        $scope.Record.OrgID = $model.OrgID;
                    }

                    $scope.getWaterNo = function () {
                        var type = 3;
                        if ($scope.Record.FromType == "外方") {
                            type = 4;
                        }
                        //请求
                        apiUtil.requestWebApi("User/GetWaterNo?type=" + type, "Get", null, function (response) {
                            if (response.Status == 0) {
                                $scope.Record.PersonID = response.Data;
                                return;
                            }
                            else {
                                layer.msg(response.Msg);
                            }
                        }, function (response) {
                            layer.msg(response.Msg);
                        });
                    }

                    $scope.onLoad = function () {
                        var data = { PersonID: $scope.PersonID };
                        //请求
                        apiUtil.requestWebApi("Person/GetDetail", "Post", data, function (response) {
                            if (response.Status == 0) {
                                $scope.Record = response.Data;
                                if ($scope.Record.PassportFiles == undefined) {
                                    $scope.Record.PassportFiles = [];
                                }

                                if (!$scope.Record.PhotoFiles == undefined) {
                                    $scope.Record.PhotoFiles = [];
                                }

                                if (!$scope.Record.IDNumberFiles == undefined) {
                                    $scope.Record.IDNumberFiles = [];
                                }

                                if (response.Data.PassportDate) {
                                    $scope.Record.PassportDate = response.Data.PassportDate.toDate();
                                }
                                if (response.Data.PassportSignDate) {
                                    $scope.Record.PassportSignDate = response.Data.PassportSignDate.toDate();
                                }
                                if (response.Data.Birth) {
                                    $scope.Record.Birth = response.Data.Birth.toDate();
                                }
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

                    $scope.onSubmit = function () {
                        if (!$("#myForm").valid()) {
                            return;
                        }
                        if (!$("#myForm2").valid()) {
                            return;
                        }

                        //请求
                        apiUtil.requestWebApi("Person/Edit", "Post", $scope.Record, function (response) {
                            if (response.Status == 0) {
                                layer.msg(response.Msg);
                                $state.go("Index.Person");
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

                    $scope.delPassportUrl = function (fileUrl,idindex) {
                        var index = -1;
                        for (var i = 0; i < $scope.Record.PassportFiles.length; i++) {
                            if ($scope.Record.PassportFiles[i].FileUrl == fileUrl) {
                                index = i;
                                break;
                            }
                        }
                        if (index >= 0) {
                            $scope.Record.PassportFiles.splice(index,1);
                            $('#PassportFile' + idindex).remove();
                        }
                    };

                    $scope.delIDNumberUrl = function (fileUrl, idindex) {
                        var index = -1;
                        for (var i = 0; i < $scope.Record.IDNumberFiles.length; i++) {
                            if ($scope.Record.IDNumberFiles[i].FileUrl == fileUrl) {
                                index = i;
                                break;
                            }
                        }
                        if (index >= 0) {
                            $scope.Record.IDNumberFiles.splice(index, 1);
                            $('#IDNumberFile' + idindex).remove();
                        }
                    };

                    $scope.delPhotoUrl = function (fileUrl, idindex) {
                        var index = -1;
                        for (var i = 0; i < $scope.Record.PhotoFiles.length; i++) {
                            if ($scope.Record.PhotoFiles[i].FileUrl == fileUrl) {
                                index = i;
                                break;
                            }
                        }
                        if (index >= 0) {
                            $scope.Record.PhotoFiles.splice(index, 1);
                            $('#PhotoFile' + idindex).remove();
                        }
                    };
                    

                    $(document).on("change", "#PassportUrl_file", function (event) {
                        var uploadFile = event.currentTarget
                        //触发文件上传
                        uploader.onFileUpload(uploadFile.files[0], function (params, process) {
                            //执行
                            process(function (uploadResp) {
                                //上传成功
                                var flag = false;
                                for (var i = 0; i < $scope.Record.PassportFiles.length; i++) {
                                    if ($scope.Record.PassportFiles[i].FileUrl == uploadResp.Data) {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag) {
                                    $scope.Record.PassportFiles.push({
                                        FileName: uploadFile.files[0].name, FileUrl: uploadResp.Data
                                    });
                                    $scope.$apply();
                                }
                            }, function (resp) {
                                console.log(resp.Msg);
                            })

                        });
                    });

                    $(document).on("change", "#IDNumberUrl_file", function (event) {
                        var uploadFile = event.currentTarget
                        //触发文件上传
                        uploader.onFileUpload(uploadFile.files[0], function (params, process) {
                            //执行
                            process(function (uploadResp) {
                                //上传成功
                                var flag = false;
                                for (var i = 0; i < $scope.Record.IDNumberFiles.length; i++) {
                                    if ($scope.Record.IDNumberFiles[i].FileUrl == uploadResp.Data) {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag) {
                                    $scope.Record.IDNumberFiles.push({
                                        FileName: uploadFile.files[0].name, FileUrl: uploadResp.Data
                                    });
                                    $scope.$apply();
                                }
                            }, function (resp) {
                                console.log(resp.Msg);
                            })

                        });
                    });

                    $(document).on("change", "#PhotoUrl_file", function (event) {
                        var uploadFile = event.currentTarget
                        //触发文件上传
                        uploader.onFileUpload(uploadFile.files[0], function (params, process) {
                            //执行
                            process(function (uploadResp) {
                                //上传成功
                                var flag = false;
                                for (var i = 0; i < $scope.Record.PhotoFiles.length; i++) {
                                    if ($scope.Record.PhotoFiles[i].FileUrl == uploadResp.Data) {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag) {
                                    $scope.Record.PhotoFiles.push({
                                        FileName: uploadFile.files[0].name, FileUrl: uploadResp.Data
                                    });
                                    $scope.$apply();
                                }
                                
                            }, function (resp) {
                                console.log(resp.Msg);
                            })

                        });
                    });

                    if ($state.params.PersonID) {
                        $scope.PersonID = $state.params.PersonID;
                        $scope.onLoad();
                    }
                    else {
                        $scope.Record = {
                            FromType: '外方', Continent: '亚洲', Tag: '', PassportFiles: [],
                            PhotoFiles: [], IDNumberFiles: [],
                        };
                        
                        $scope.onTagChange();
                        $scope.getWaterNo();
                    }
                }
            ]);

        });
