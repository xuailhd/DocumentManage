require.config({
    baseUrl: '/static/',
    waitSeconds: 0,
    urlArgs: "bust=V1.0" + Math.random(),
    packages: [
    {
        name: 'echarts',
        location: 'framework/plugins/echarts',
        main: 'echarts'
    }
    ],
    paths: {
        "require-text": "framework/require/require-text",
        "require-css": "framework/require/require-css",
        "jquery": "framework/jquery/jquery-1.10.2",
        "jquery-cookie": "framework/jquery/jquery.cookie",
        "jquery-inputmask": "framework/jquery/jquery.inputmask",
        "jquery-metisMenu": "framework/jquery/jquery.metisMenu",
        "jquery-slimscroll": "framework/jquery/jquery.slimscroll.min",
        "jquery-validate": "framework/jquery/jquery.validate",
        "jquery.metadata": "framework/jquery/jquery.metadata",
        "jquery-uploadfy": "framework/jquery/uploadfy/jquery.uploadify",
        "jquery-ui": "framework/jquery/jquery-ui-1.11.4",
        "jquery-select2": "framework/jquery/jquery.select2",
        "jquery-select2_locale_zh-CN": "framework/jquery/jquery.select2_locale_zh-CN",
        "jquery-form": "framework/jquery/jquery-form",
        "jquery-flot": "framework/jquery/flot/jquery.flot.min",
        "jquery-flot-time": "framework/jquery/flot/jquery.flot.time",
        "jquery-flot-resize": "framework/jquery/flot/jquery.flot.resize",
        "jquery-splitter": "framework/jquery/jquery.splitter",

        "bootstrap": "framework/bootstrap/js/bootstrap",
        "bootstrap-colorpicker": "framework/bootstrap/js/bootstrap-colorpicker.min",
        "bootstrap-prettyfile": "framework/bootstrap/js/bootstrap-prettyfile",
        "bootstrap-table": "framework/bootstrap/js/bootstrap-table",
        "bootstrap-select": "framework/bootstrap/js/bootstrap-select",
        "bootstrap-table-mobile": "framework/bootstrap/js/bootstrap-table-mobile",
        "bootstrap-typeahead": "framework/bootstrap/js/bootstrap-typeahead.min",
        "bootstrap-notify": "framework/bootstrap/js/bootstrap-notify",

        "angular": "framework/angular/angular",
        "angular-resource": "framework/angular/angular-resource",
        "angular-amd": "framework/angular/angular-amd",
        "angular-cookies": "framework/angular/angular-cookies",
        "angular-ui-route": "framework/angular/angular-ui-route",
        "angular-ui-bootstrap": "framework/angular/angular-ui/ui-bootstrap",
        "angular-route": "framework/angular/angular-route",
        "angular-translate": "framework/angular/angular-translate",
        "angular-translate-loader-static-files": "framework/angular/angular-translate-loader-static-files",
        "angular-animate": "framework/angular/angular-animate",
        "angular-animate-css": "framework/angular/angular-animate-css",
        "angular-upload-shim": "framework/angular/angular-upload/angular-upload-shim.min",
        "angular-upload": "framework/angular/angular-upload/angular-upload.min",

        "plugins-iscroll": "framework/plugins/iscroll",
        "plugins-md5": "framework/plugins/md5",
        "plugins-recorder": "framework/plugins/recorder/recorder",
        "plugins-pace": "framework/plugins/pace/pace",
        "plugins-laydate": "framework/plugins/laydate/laydate",
        "plugins-layer": "framework/plugins/layer/layer",/*这里不能使用压缩*/
        "plugins-pdf": "framework/plugins/pdf/pdf",
        "plugins-ueditor": "framework/plugins/ueditor/ueditor.all",        

        "plugins-extend-array": "framework/plugins/extend/array",
        "plugins-extend-date": "framework/plugins/extend/Date",
        "plugins-extend-string": "framework/plugins/extend/String",
        "plugins-dateTime": "framework/plugins/dateTime",
        "plugins-echarts": "framework/plugins/echarts/echarts",
        "plugins-localAll": "framework/plugins/fullCalendar/locale-all",
        "plugins-fullCalendar": "framework/plugins/fullCalendar/fullcalendar.min",

        "moment": "framework/plugins/fullCalendar/moment.min",

        "module-directive-bundling-all": "modules/Common/directives/bundling-all",
        "module-directive-hasPermission": "modules/Common/directives/hasPermission",
        "module-directive-spinner": "modules/Common/directives/spinner",
        "module-directive-pager": "modules/Common/directives/pager",
        "module-directive-grid": "modules/Common/directives/grid",
        "module-directive-countdown": "modules/Common/directives/countdown",
        "module-directive-scrollbar": "modules/Common/directives/scrollbar",
        "module-directive-preview": "modules/Common/directives/preview",
        "module-directive-dcmViewer": "modules/Common/directives/dcmViewer",
        "module-directive-uploader": "modules/Common/directives/uploader",
        "module-directive-capture": "modules/Common/directives/capture",
        "module-directive-recorder": "modules/Common/directives/recorder",
        "module-directive-nav-tabs": "modules/Common/directives/nav-tabs",
        "module-directive-splitter": "modules/Common/directives/splitter",

        "module-directive-form-validate": "modules/Common/directives/form-validate",
        "module-directive-form-control": "modules/Common/directives/form-control",
        "module-directive-form-control-switch": "modules/Common/directives/form-control-switch",
        "module-directive-form-control-colorpicker": "modules/Common/directives/form-control-colorpicker",
        "module-directive-form-control-inputmask": "modules/Common/directives/form-control-inputmask",
        "module-directive-form-control-datepicker": "modules/Common/directives/form-control-datepicker",
        "module-directive-form-control-imagepicker": "modules/Common/directives/form-control-imagepicker",
        "module-directive-form-control-select": "modules/Common/directives/form-control-select",
        "module-directive-form-control-selectTree": "modules/Common/directives/form-control-selectTree",
        "module-directive-form-control-file": "modules/Common/directives/form-control-file",
        "module-directive-form-control-editor": "modules/Common/directives/form-control-editor",

        "module-config": "modules/Common/config",        
        "module-Doctor-bootstrap": "modules/Doctor/bootstrap",
        "module-Doctor-routes": "modules/Doctor/Routes",
        "module-services-apiUtil": "modules/Common/services/api.Util",
        "module-Services-uploader": "modules/Common/services/uploader",

        
    },
    map:
        {
            "*": {
                "css": "require-css",
                "text": "require-text"
            }
        },
    //配置模块依赖关系
    shim: {
        "jquery": {
        },
        "jquery-inputmask": {
            deps: ["jquery"]
        },
        "jquery-cookie":{
            deps: ["jquery"],
        },
        "jquery-metisMenu": {
            deps: ["jquery"]
        },
        "jquery-slimscroll": {
            deps: ["jquery"]
        },
        "jquery-select2": {
            deps: ["css!framework/jquery/jquery.select2.css"]
        },
        "jquery-select2_locale_zh-CN": {
            deps: ["jquery-select2"]
        },
        "jquery-metadata": { deps: ["jquery"] },
        "jquery-splitter": { deps: ["jquery"] },
        "jquery-validate": { deps: ["jquery", "jquery.metadata"], exports: "$.fn" },
        "jquery-uploadfy": { deps: ["jquery", "css!framework/jquery/uploadfy/jquery.uploadify.css"] },
        "jquery-flot-time": { deps: ["jquery", "jquery-flot"] },
        "jquery-flot-resize": { deps: ["jquery", "jquery-flot"] },

        'bootstrap': { deps: ["jquery"] },
        'bootstrap-select': { deps: ["jquery", "bootstrap", "css!framework/bootstrap/css/bootstrap-select.css"] },
        'bootstrap-table': { deps: ["jquery", "bootstrap"] },
        'bootstrap-colorpicker': { deps: ["jquery", "bootstrap", "css!framework/bootstrap/css/bootstrap-colorpicker.min.css"] },
        'bootstrap-prettyfile': { deps: ["jquery", "bootstrap"] },
        'bootstrap-table-mobile': {
            deps: ["jquery", "bootstrap"],
        },
        "bootstrap-notify": {
            deps: [
                "jquery",
                "bootstrap",
                "css!framework/bootstrap/css/bootstrap-notify.css"
            ]
        },

        'angular': {
            deps:["jquery"],
            exports: "angular"
        },
        'angular-amd': {
            deps: ["angular"],
        },
        'angular-cookies': {
            deps: ["angular"],
        },
        'angular-ui-route': {
            deps: ["angular", "angular-amd"],
        },
        "angular-ui-bootstrap": { deps: ["angular", "bootstrap"] },
        'angular-route': {
            deps: ["angular"],
        },
        "angular-resource": { deps: ["angular"] },
        'angular-animate': {
            deps: ["angular"],
        },
        'angular-animate-css': {
            deps: ["angular", "angular-animate", "css!framework/plugins/animate/animate.css"],
        },
        'angular-translate': {
            deps: ["angular"],
        },
        'angular-translate-loader-static-files': {
            deps: ["angular", "angular-translate"],
        },
        "angular-upload": { deps: ["angular", "angular-upload-shim"] },

        //插件-加载进度
        'plugins-pace': {
            deps: ["jquery", "css!framework/plugins/pace/themes/pace-theme-minimal.css"]
        },
        //插件日期控件
        'plugins-laydate': { deps: ["jquery", "css!framework/plugins/laydate/need/laydate.css", "css!framework/plugins/laydate/skins/default/laydate.css"] },
        //插件弹出层
        'plugins-layer': { deps: ["jquery", "css!framework/plugins/layer/skin/layer.css"] },
        //插件-编辑器
        'plugins-ueditor': { deps: ["jquery", "framework/plugins/ueditor/ueditor.config", "css!framework/plugins/ueditor/themes/default/css/ueditor.css"] },

        "plugins-fullCalendar": {
            deps: ["css!framework/plugins/fullCalendar/fullcalendar.css", "jquery", "jquery-ui", "moment"]
        },
        "plugins-localAll":{
            deps:["plugins-fullCalendar"]
        },
        //路由
        "module-routes": { deps: ["angular", "angular-ui-route"] },
        //指令-表单验证
        "module-directive-form-validate": { deps: ["jquery", "jquery-validate"] }
    }
});