/*
 * [登录页面事件封装] */
CTFO.Model.Login = (function(container) {
    var p = {};
    var container = null;
    var errorTip = null;
    var userName = $.cookie('ctfo_bs_userName');
    var userPassword = '';
    var corpCode = $.cookie('ctfo_bs_corpCode');
    var userStorageFlag = $.cookie('ctfo_bs_rememberLoginFlag');

    var refreshImgCode = function() {
        $(container).find('.imgCode').attr('src', CTFO.config.sources.rondamImage + '?d' + new Date().getTime());
    };

    var validate = function(fields) {
        $(fields).each(function(event) {
            var val = $(this).val(),
                errorText = $(this).attr('title');
            if (!val || val === errorText) {
                errorTip.text(errorText);
                return false;
            }
        });
        errorTip.text('');
        return true;
    };
    /**
     * [storeUser 保存/删除cookie中的用户信息]
     * @param  {[Boolean]} flag    [存/删状态]
     * @param  {[String]} userName [用户名]
     * @param  {[String]} corpCode [企业编码]
     * @return {[Null]}            [无返回]
     */
    var storeUser = function(flag, userName, corpCode) {
        if (flag) {
            $.cookie("ctfo_bs_userName", userName, {
                path: '/',
                expires: 30
            });
            $.cookie("ctfo_bs_corpCode", corpCode, {
                path: '/',
                expires: 30
            });
            $.cookie("ctfo_bs_rememberLoginFlag", flag, {
                path: '/',
                expires: 30
            });
        } else {
            $.cookie("ctfo_bs_rememberLoginFlag", null, {
                path: '/'
            });
            $.cookie("ctfo_bs_userName", null, {
                path: '/'
            });
            $.cookie("ctfo_bs_corpCode", null, {
                path: '/'
            });
        }
    };
    /**
     * [submitLogin 提交登录]
     * @return {[Null]} [无返回,跳转index.html]
     */
    var submitLogin = function() {
        if (!validate($(container).find('.input'))) return false;
        $(container).loadMask('');
        userPassword = $(container).find("input[name=password]").val();
        var checkedFlag = $(container).find('input[name=userStorage]').attr('checked');
        var userName = $(container).find("input[name=userName]").val().toLowerCase();
        var password = hex_sha1(userPassword).toLowerCase();
        var imgCode = $(container).find("input[name=imgCode]").val();
        var corpCode = $(container).find("input[name=corpCode]").val();

        storeUser(checkedFlag, userName, corpCode);

        var param = {
            'requestParam.equal.opLoginname': userName,
            'requestParam.equal.opPass': password,
            'imgCode': imgCode,
            'requestParam.equal.corpCode': corpCode
        };

        // if(!userName) {
        //     errorTip.text($(container).find('input[name=userName]').attr('title'));
        // } else if(!password) {
        //     errorTip.text($(container).find('input[name=password]').attr('title'));
        // } else if(!imgCode) {
        //     errorTip.text($(container).find('input[name=imgCode]').attr('title'));
        // } else if(!corpCode) {
        //     errorTip.text($(container).find('input[name=corpCode]').attr('title'));
        // }

        $.ajax({
            url: CTFO.config.sources.login,
            type: 'POST',
            dataType: 'json',
            data: param,
            complete: function(xhr, textStatus) {
                //called when complete
                $(container).unLoadMask();
            },
            success: function(data, textStatus, xhr) {
                if (data && data.displayMessage == "success") {
                    //$.cookie("ctfo_bs_user_info", JSON.stringify(data));
                    $(container).remove();
                    window.location.replace("index.html");
                } else {
                    refreshImgCode();
                    errorTip.text(data.opInfo);
                }
                /*if (data && data.error) {
                    refreshImgCode();
                    errorTip.text(data.error[0].errorMessage);

                    return false;
                }*/
                /*if (data && data.opEndutc && data.opEndutc < new Date().getTime()) {
                    refreshImgCode();
                    errorTip.text('用户权限过期');
                    return false;
                }*/
                /*if(!checkPassword()){
                	CTFO.Model.passwordWindow.getInstance().popResetPasswordWin();
                	return false;
                }*/
            },
            error: function(xhr, textStatus, errorThrown) {
                refreshImgCode();
                errorTip.text('登录出错');
            }
        });
    };

    /**
     * 检测用户密码是纯数字或者纯字母，则需要修改
     */
    var checkPassword = function() {
        var reg = new RegExp("^[0-9]*$", "g");
        var reg2 = new RegExp("^[a-zA-Z]*$", "g");
        var rs1 = userPassword.search(reg);
        var rs2 = userPassword.search(reg2);

        return (rs1 == 0 || rs2 == 0) ? false : true;
    };

        /**
         * [bindEvent 绑定全局事件]
         * @return {[type]} [description]
         */
    var bindEvent = function() {
        /**
         * 初始化和事件绑定
         */
        document.onkeydown = function(e) {
            var theEvent = window.event || e;
            var code = theEvent.keyCode || theEvent.which;
            if (+code === 13) {
                $(container).find('.login_submit').trigger('click');
            }
        };

        if (CTFO.config.globalObject.isShowCheckCode) {
            refreshImgCode();
        } else {
            $(container).find('.checkcode').addClass("none");
        }


        $(container)
            .find('input[name=userName]').val(userName ? userName : '用户名').end()
            .find('input[name=corpCode]').val(corpCode ? corpCode : '组织编码').end()
            // .find('input[name=password]').focus(function(event) {
            //     $(this).siblings('div:eq(0)').hide();
            // }).end()
            .find('input')
            .focus(function(event) {
                var val = $(this).val(),
                    errorText = $(this).attr('title');
                if (!val || val === errorText) {
                    if ($(this).attr('name') === 'password') {
                        $(this).siblings('div:eq(0)').hide();
                    }
                    $(this).val('');
                }
            })
            .blur(function(event) {
                var val = $(this).val(),
                    errorText = $(this).attr('title');
                if (!val || val === errorText) {
                    $(this).val(errorText);
                    if ($(this).attr('name') === 'password') {
                        $(this).siblings('div:eq(0)').show();
                        $(this).val('');
                    }
                    errorTip.text('请输入' + errorText);
                }
            }).end()
            .find('.imgCode').click(function(event) {
                refreshImgCode();
            }).end()
            .find('input[name=userStorage]').attr('checked', userStorageFlag || false).end()
            // .change(function(event) {
            //     var flag = $(this).attr('checked'),
            //         userName = $(container).find('input[name=userName]').val(),
            //         corpCode = $(container).find('input[name=corpCode]').val();
            //     storeUser(flag, userName, corpCode);
            // }).end()
            .find('.login_submit').click(function(event) {
                submitLogin();
            }).end()
            .find('.saveFavorite').click(function(event) {
                try {
                    var url = window.location.href,
                        title = document.title || '车后平台支撑系统';
                    if (document.all)
                        window.external.AddFavorite(url, title);
                    else if (window.sidebar)
                        window.sidebar.addPanel(title, url, "");
                    else
                        $.ligerDialog.success("您的浏览器不支持自动加入收藏，请手动设置！");
                    //alert('您的浏览器不支持自动加入收藏，请手动设置！');
                } catch (e) {
                    $.ligerDialog.success("您的浏览器不支持自动加入收藏，请手动设置！");
                    //alert("您的浏览器不支持自动加入收藏，请手动设置！");
                }
            });

        $(container).find('input[name=userName]').focus();

    }

    return {
        init: function(options) {
            p = $.extend({}, p || {}, options || {});
            container = p.loginForm;
            errorTip = $(container).find('.errorTip');
            bindEvent();
            return this;
        }
    };


})();