﻿$(document).ready(function () {
    $.get('login.html', function (data) {
        $('.login-extra').remove();
        $('.account-container').replaceWith(data);
    });
    $('.close').live('click', function () {
        $(this).parent().addClass('hidden');
    });
    $("#btnSignIn").live('click', function () {
        $.ajax({
            url: "http://localhost/Rest2/Auth/v1/tokenrequest",
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ username: $("#username").val(), password: $("#password").val() }),
            success: function (data, textStatus, xhr) {
                console.log(xhr.getAllResponseHeaders());
                document.location = 'home.html';
            },
            error: function (err) {
                $('.alert-error').html('<a class="close">×</a><strong>Error</strong> Invalid username or password. Please try again using your full Connect username').removeClass('hidden');
            }
        });
    });
    $("#btnRegister").live('click', function (event) {
        event.preventDefault(); 
        if ($('#register-form').valid()) {
            $.ajax({
                url: "http://localhost/Rest2/auth/v1/registermember",
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ email: $('#email').val(), password: $("#password").val(), username: $("#txtUsername").val() }),
                success: function(data, textStatus, xhr) {
                    $('.alert-error').html('<a class="close">×</a><strong>Success</strong> The user was created successfully').addClass('alert-success').removeClass('hidden alert-error');
                },
                error: function(err) {
                    $('.alert-error').html('<a class="close">×</a><strong>Error</strong> This user already exist.').removeClass('hidden');
                }
            });
        } else {
            $('.alert-error').html('<a class="close">×</a><strong>Error</strong> Your registration form is not valid.').removeClass('hidden');
        }
    });
    $('input').live('keydown',function (e) {
        if (e.keyCode == 13) {
            $('#btnSignIn').click();
            return false;
        }
    });
    $('#username').focus();
    $('.createaccount').click(function (e) {
        navigate($(this).attr('href'), $(this), e);
    });
    $('.brand').click(function (e) {
        navigate($(this).attr('href'), $(this), e);
    });
    $('.faq').click(function (e) {
        navigate($(this).attr('href'), $(this), e);
    });
    function navigate(url, element, event) {
        event.preventDefault();
        var $href = url;
        $.get($href, function (data) {
            $('.login-extra').remove();
            $('.mainnav li').removeClass();
            element.parent().addClass('active');
            if ($('.account-container').length > 0) {
                $('.account-container').fadeOut("slow", function () {
                    var div = $(data).hide();
                    $(this).replaceWith(div);
                    div.fadeIn("slow", function () {
                        if ($href == 'signup.html') {
                            $('#register-form').validate({
                                rules: {
                                    txtUsername: { minlength: 6, required: true },
                                    email: { required: true, email: true },
                                    password: { minlength: 6, required: true },
                                    confirm_password: { minlength: 6, equalTo: "#password", required: true }

                                },
                                highlight: function (label) {
                                    $(label).closest('.control-group').removeClass('success').addClass('error');
                                    $(label).parent().children('.icon-ok').removeClass('icon-ok').removeAttr('style');
                                },
                                success: function (label) {
                                    label.addClass('icon-ok').attr('style','display:inline-block;height:20px;width:15px;position:absolute;margin:-30px 0 0 328px;').closest('.control-group').addClass('success');
                                }
                            });
                        }
                    });

                });
            } else {
                $('.main').fadeOut("slow", function () {
                    var div = $(data).hide();
                    $(this).replaceWith(div);
                    div.fadeIn("slow", function () {
                        if ($href == 'signup.html') {
                            $('#register-form').validate({
                                rules: {
                                    txtUsername: { minlength: 6, required: true },
                                    email: { required: true, email: true },
                                    password: { minlength: 6, required: true },
                                    confirm_password: { minlength: 6, equalTo: "#password", required: true }
                                },
                                highlight: function (label) {
                                    $(label).closest('.control-group').removeClass('success').addClass('error');
                                    $(label).parent().children('.icon-ok').removeClass('icon-ok').removeAttr('style');
                                },
                                success: function (label) {
                                    label.addClass('icon-ok').attr('style', 'display:inline-block;height:20px;width:15px;position:absolute;margin:-30px 0 0 328px;').closest('.control-group').addClass('success');
                                }
                            });
                        }
                    });

                });
            }
        });
    }
});


 