"use strict";
define(["jquery",
           "module-Doctor-routes",
           "module-services-apiUtil",
           "angular",
           "angular-amd",
           "angular-animate",
           "angular-translate",
           "angular-translate-loader-static-files",
           "angular-ui-bootstrap",
           "bootstrap",
           "plugins-layer",
           "plugins-extend-array",
           "plugins-extend-date",
           "plugins-extend-string",
], function ($, registerRoutes, apiUtil, angular, angularAMD) {

    var app = angular.module("myApp", [
        "pascalprecht.translate",
        'ui.router',
        "ui.bootstrap",
        "ngAnimate"]);//templates-directive 加载已经打包好的指令模板（减少HTTP请求）

    //权限控制
    angular.module('myApp').factory('permissions',["$rootScope", function ($rootScope) {
        var permissionList;

        return {
            //设置权限
            setPermissions: function (permissions) {
                permissionList = permissions;
                $rootScope.$broadcast('permissionsChanged')
            },
            //是否拥有权限
            hasPermission: function (permission) {
                permission = permission.trim();
                return permissionList.some(function (item) {
                    return item.AuthID.trim() === permission
                });
            }
        };
    }]);

    //配置国际化
    app.config(['$translateProvider', function ($translateProvider) {

        var lang = window.localStorage.lang || 'zh-cn';

        $translateProvider.preferredLanguage(lang);

        //when can not determine lang, choose en lang.
        $translateProvider.fallbackLanguage('zh-cn');

        $translateProvider.useStaticFilesLoader({
            prefix: '/static/resource/translate/',
            suffix: '.json?random' + Math.random()
        });
    }]);

    //配置路由
    app.config(["$stateProvider", "$urlRouterProvider", registerRoutes.registerRoutes]);

    //设置路由事件
    app.run(['$rootScope', '$log', "$state", "permissions", function ($rootScope, $log, $state, permissions) {

        //加载权限
        if (global_Permission) {
            //设置权限
            permissions.setPermissions(global_Permission || [])
        }
        else {
            console.error("not set global_Permission");
        }

        //状态改变成功
        $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
            $log.debug('successfully changed states');
            $log.debug('event', event);
            $log.debug('toState', toState);
            $log.debug('toParams', toParams);
            $log.debug('fromState', fromState);
            $log.debug('fromParams', fromParams);

            //if (golbal_Modules == undefined || golbal_Modules.length <= 0) {
            //    $state.go('Index.PersonalInfo');
            //}

            //var flag = false;
            //for (var i = 0; i < golbal_Modules.length; i++) {
            //    if (golbal_Modules[i].AuthUrl == "#/Index" + toState.url) {
            //        flag = true;
            //    }
            //}

            //if (!flag) {
            //    $state.go('Index.PersonalInfo');
            //}
        });

        //未找到状态配置
        $rootScope.$on('$stateNotFound', function (event, unfoundState, fromState, fromParams) {
            $log.error('The request state was not found: ' + unfoundState);
        });

        //状态改变出现异常时
        $rootScope.$on('$stateChangeError', function (event, toState, toParams, fromState, fromParams, error) {

            $log.error('An error occurred while changing states: ' + error);

            $log.debug('event', event);
            $log.debug('toState', toState);
            $log.debug('toParams', toParams);
            $log.debug('fromState', fromState);
            $log.debug('fromParams', fromParams);

          
        });

    }]);

    angularAMD.bootstrap(app);



});