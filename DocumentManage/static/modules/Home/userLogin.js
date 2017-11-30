define(["jquery",
        "plugins-layer",
        "module-services-apiUtil",
        "jquery-validate",
        "css!styles/layout.login.css"], function ($, layer, apiUtil) {

            //登录提交按钮
            var login = function () {

                var Data = {};

                //用户账号
                Data.UserID = $("#UserID").val();
                //密码
                Data.Password = $("#Password").val();


                //请求 网络医院验证
                apiUtil.requestWebApi("User/Login", "post", Data, function (response) {
                    //登录成功
                    if (response.Status == 0) {
                        //记录登录状态，在路由状态改变事件中会对此状态进行验证保证用户已登录                          
                        apiUtil.setUserToken(response.Data.UserToken)
                        //SPA应用需要使用
                        apiUtil.setLoginInfo(response.Data);
                        location.href = '/';
                        return;
                    }
                    else {
                        layer.msg(response.Msg);
                    }
                }, function (response) {
                    layer.msg(response.Msg);
                });

                return false;
            }

            var formValid = {

                errorElement: 'label',
                errorClass: 'error',
                focusInvalid: true,
                onfocusout: function (element) {
                    var t = $(element).valid();
                },
                errorPlacement: function (error, element) {
                    error.appendTo(element.parent());
                    element.parents(".loginInput").addClass("has-error");
                },
                success: function (element) {
                    element.parents(".loginInput").removeClass("has-error");
                    element.remove();
                },
                submitHandler: login,
                rules: {
                    UserID: {
                        required: true,
                        minlength: 2
                    },
                    Password: {
                        required: true,
                        minlength: 6
                    },
                },
                messages: {
                    UserID: {
                        required: "请输入登陆账号或手机号",
                        minlength: "长度不能小于2"
                    },
                    Password: {
                        required: "请输入登陆密码",
                        minlength: "长度不能小于6"
                    },
                }

            };

            $("#loginform").validate(formValid);

        });