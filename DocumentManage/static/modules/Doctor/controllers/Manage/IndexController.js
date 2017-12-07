"use strict";
define([
        "module-directive-bundling-all",
    ],
    function () {

        var app = angular.module("myApp",
            [
                "pascalprecht.translate",
                'ui.router',
                "ui.bootstrap",
                "ngAnimate"
            ]);

        app.controller('ManageController',
            [
                '$scope', "$state", '$translate', function ($scope,  $state, $translate) {
                    $scope.currentStateName = $state.current.name;
                }
            ]);
    });