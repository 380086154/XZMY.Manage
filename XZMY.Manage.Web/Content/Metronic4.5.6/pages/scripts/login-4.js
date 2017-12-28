var Login = function () {
    var form1 = $('#defaultForm');

    var handleLogin = function () {
        $('.login-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            rules: {
                LoginName: {
                    required: true
                },
                Password: {
                    required: true
                },
                remember: {
                    required: false
                }
            },

            messages: {
                LoginName: {
                    required: ""//\u4E0D\u80FD\u4E3A\u7A7A 不能为空
                },
                Password: {
                    required: ""//\u4E0D\u80FD\u4E3A\u7A7A 不能为空
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit   
                $('.alert-danger', $('.login-form')).show();
            },

            highlight: function (element) { // hightlight error inputs
                $(element).closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function (error, element) {
                error.insertAfter(element.closest('.input-icon'));
            },

            submitHandler: function (form) {
                //form.submit();

                $('.alert-danger', $('.login-form')).text('\u8BF7\u8F93\u5165\u5E10\u53F7\u548C\u5BC6\u7801').hide();//请输入帐号和密码
                var data = form1.serializeObject();
                $.ajax({
                    type: 'POST',
                    url: '/Login/AjaxLogin',
                    data: data,
                    dataType: 'JSON',
                    success: function (result) {
                        if (result.success) {
                            if (result.url.length > 0) {
                                location.replace(result.url);
                            } else {
                                location.replace('/home/index');
                            }
                        } else {
                            $('.alert-danger', $('.login-form')).text(result.errors).show();
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //showToast(3, '系统消息', JSON.stringify(errorThrown));//错误信息
                        alert(JSON.stringify(errorThrown));
                    },
                    async: false
                });
            }
        });

        $('.login-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.login-form').validate().form()) {
                    $('.login-form').submit();
                }
                return false;
            }
        });
    }

    var handleForgetPassword = function () {
        $('.forget-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",
            rules: {
                email: {
                    required: true,
                    email: true
                }
            },

            messages: {
                email: {
                    required: "\u4E0D\u80FD\u4E3A\u7A7A"//不能为空
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit   

            },

            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function (error, element) {
                error.insertAfter(element.closest('.input-icon'));
            },

            submitHandler: function (form) {
                form.submit();
            }
        });

        $('.forget-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.forget-form').validate().form()) {
                    $('.forget-form').submit();
                }
                return false;
            }
        });

        jQuery('#forget-password').click(function () {
            jQuery('.login-form').hide();
            jQuery('.forget-form').show();
        });

        jQuery('#back-btn').click(function () {
            jQuery('.login-form').show();
            jQuery('.forget-form').hide();
        });

    }

    var handleRegister = function () {

        function format(state) {
            if (!state.id) { return state.text; }
            var $state = $('<span><img src="/Content/Metronic4.5.6/global/img/flags/' + state.element.value.toLowerCase() + '.png" class="img-flag" /> ' + state.text + '</span>');

            return $state;
        }

        if (jQuery().select2 && $('#country_list').size() > 0) {
            $("#country_list").select2({
                placeholder: '<i class="fa fa-map-marker"></i>&nbsp;Select a Country',
                templateResult: format,
                templateSelection: format,
                width: 'auto',
                escapeMarkup: function (m) {
                    return m;
                }
            });

            $('#country_list').change(function () {
                $('.register-form').validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input
            });
        }

        $('.register-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",
            rules: {
                fullname: {
                    required: true
                },
                email: {
                    required: true,
                    email: true
                },
                address: {
                    required: true
                },
                city: {
                    required: true
                },
                country: {
                    required: true
                },

                LoginName: {
                    required: true
                },
                Password: {
                    required: true
                },
                rpassword: {
                    equalTo: "#register_password"
                },

                tnc: {
                    required: true
                }
            },

            messages: { // custom messages for radio buttons and checkboxes
                tnc: {
                    required: "Please accept TNC first."
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit   

            },

            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function (error, element) {
                if (element.attr("name") == "tnc") { // insert checkbox errors after the container                  
                    error.insertAfter($('#register_tnc_error'));
                } else if (element.closest('.input-icon').size() === 1) {
                    error.insertAfter(element.closest('.input-icon'));
                } else {
                    error.insertAfter(element);
                }
            },

            submitHandler: function (form) {

                
            }
        });

        $('.register-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.register-form').validate().form()) {
                    $('.register-form').submit();
                }
                return false;
            }
        });

        jQuery('#register-btn').click(function () {
            jQuery('.login-form').hide();
            jQuery('.register-form').show();
        });

        jQuery('#register-back-btn').click(function () {
            jQuery('.login-form').show();
            jQuery('.register-form').hide();
        });
    }

    return {
        //main function to initiate the module
        init: function () {

            handleLogin();
            handleForgetPassword();
            handleRegister();
            $('#LoginName').focus();
            // init background slide images
            $.backstretch([
		        "/Content/Metronic4.5.6/pages/media/bg/1.jpg",
		        "/Content/Metronic4.5.6/pages/media/bg/2.jpg",
		        "/Content/Metronic4.5.6/pages/media/bg/3.jpg",
		        "/Content/Metronic4.5.6/pages/media/bg/4.jpg"
            ], {
                fade: 1000,
                duration: 8000
            });


        }
    };

}();

jQuery(document).ready(function () {
    Login.init();
});