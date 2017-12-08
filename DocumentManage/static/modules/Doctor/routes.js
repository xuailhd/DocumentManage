define(["angular",
        "angular-amd",
        "angular-ui-route",
], function (angular, angularAMD) {
    // routes
    var registerRoutes = function ($stateProvider, $urlRouterProvider) {

        function amdRoute(url, view, controller, onEnter, onLeave) {
            
            controller = controller || view;
            return angularAMD.route({
                url: url,
                templateUrl: function ($stateParams) {
                    return "/static/modules/" + view + ".html"
                },
                controllerUrl: "/static/modules/" + controller + "Controller.js",
                onEnter: onEnter || function () { },
                onLeave: onEnter || function () { },
            });
        }
        $urlRouterProvider.otherwise("Index");

        // route
        $stateProvider     
        //医生首页
            .state("Index", amdRoute("/Index", "Doctor/controllers/index"))
            //系统管理
            .state("Index.Manage", amdRoute("/Manage", "Doctor/controllers/Manage/Index"))
            .state("Index.Manage.UserList", amdRoute("/UserList", "Doctor/controllers/Manage/UserList"))
            .state("Index.Manage.RoleList", amdRoute("/UserList", "Doctor/controllers/Manage/RoleList"))
        //修改密码
            .state("Index.ChangePassword", amdRoute("/ChangePassword", "Common/controllers/ChangePassword"))
            .state("Index.PersonalInfo", amdRoute("/PersonalInfo", "Common/controllers/PersonalInfo"))
        //档案
        .state("Index.Record", amdRoute("/Record", "Doctor/controllers/Record/Index"))
        //档案编辑
        .state("Index.RecordEdit", amdRoute("/RecordEdit/:VisitID", "Doctor/controllers/Record/Edit"))
        //档案详情
        .state("Index.RecordDetail", amdRoute("/RecordDetail/:VisitID", "Doctor/controllers/Record/Detail"))
        //机构
        .state("Index.Org", amdRoute("/Org", "Doctor/controllers/Org/Index"))
        //机构编辑
        .state("Index.OrgEdit", amdRoute("/OrgEdit/:OrgID", "Doctor/controllers/Org/Edit"))
        //机构详情
        .state("Index.OrgDetail", amdRoute("/OrgDetail/:OrgID", "Doctor/controllers/Org/Detail"))
        //人员资料
        .state("Index.Person", amdRoute("/Person", "Doctor/controllers/Person/Index"))
        //人员资料编辑
        .state("Index.PersonEdit", amdRoute("/PersonEdit/:PersonID", "Doctor/controllers/Person/Edit"))
        //人员资料详情
        .state("Index.PersonDetail", amdRoute("/PersonDetail/:PersonID", "Doctor/controllers/Person/Detail"))
        //综合查询
        .state("Index.Query", amdRoute("/Query", "Doctor/controllers/Query/Index"))

    };

    return {
        registerRoutes: registerRoutes
    }

});