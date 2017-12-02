define(["module-services-apiUtil",
        "css!styles/layout.space.min.css",
        "bootstrap-notify"], function (apiUtil, eventBus, heartbeat) {


            var app = angular.module("myApp", [
                "pascalprecht.translate",
                'ui.router',
                "ui.bootstrap",
                "ngAnimate"]);


            //国际化控制器
            app.controller('IndexController', ['$scope',
                '$translate',
                "$state",
                "$rootScope",
                function ($scope,
                    $translate,
                    $state,
                    $rootScope) {

                    //当前语言
                    $scope.lang = $translate.use();
                    $scope.fn = {};
                    $scope.loginInfo = {};


                    //系统权限
                    $scope.Modules = golbal_Modules;


                    //当前菜单特殊显示
                    $scope.currentMenu = $state.current.name;
                    //设置语言
                    $scope.fn.onSetLang = function (lang) {
                        $scope.lang = lang

                        $translate.use(lang)
                    }
                    //退出
                    $scope.fn.onLogout = function () {
                        //退出登录
                        apiUtil.requestWebApi("User/LoginOut","Post",null, function () {
                            apiUtil.setLoginInfo({})
                            location.href = "/Login";
                        })
                    }

                    $scope.fn.onOpenMenu = function (item)
                    {
                        $scope.currentMenu = item.AuthUrl;
                    }

                    
                    //跳转到默认页面
                    var gotoDefaultPage = function () {
                        if ($state.current.name == "Index") 
                        {
                            window.location.href = $scope.Modules[0].AuthUrl;
                        }
                    }

                    var getLoginInfo=function()
                    {
                        try
                        {
                            //获取登录信息
                            $scope.loginInfo = apiUtil.getLoginInfo();
                        }
                        catch (e)
                        {
                            location.href = "/Login";
                            return;
                        }
                    }

                    

                    // 火狐乱码
                    var firexFlashingHide = function () {
                        document.getElementsByTagName('html')[0].style.display = 'none';
                    }
                    var firexFlashingShow = function () {
                        document.getElementsByTagName('html')[0].style.display = 'block';
                    }

                    //页面初始化
                    var pageInit = function () {
                        firexFlashingHide();
                        gotoDefaultPage();
                        getLoginInfo();
                        firexFlashingShow();
                    }

                    pageInit();

                }]);
        });