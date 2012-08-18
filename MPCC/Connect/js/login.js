require(['common'],
    function () {
        function loginSubmit() {
            common.Loader({ dialog: true, overlay: true, annotated: true });
            $('.loader-node').html('<i></i><b>Validating Credentials</b');

            $.ajax({
                type: "POST",
                url: "scripts/rest/loginhandler",
                data: "action=login&_authUserName=" + $(".username").val() + "&_authUserPassword=" + $(".password").val(),
                success: function (data) {
                    common.Loader('destroy');
                    if (data.status == 'ok') {
                        if (document.images) {
                            location.replace('./home.html');
                        } else {
                            location.href = 'home.html';
                        }
                    } else {
                        $('#messageBox').fuelNotification();
                        $('#messageBox').fuelNotification('show', 'error', 'Login Failure:', data.message);
                    }
                },
                error: function (data) {
                    common.Loader('destroy');
                    common.notification('show', 'error', 'Login Failure:', '&nbsp; The username or password you entered is incorrect.');
                },
                dataType: 'json'
            });
        }

        $(document).ready(function () {

            $("#loginButton").click(function () {
                loginSubmit();
                return false;
            });

            $(function () {
                $("#username").focus();
            });

            $('input').keydown(function (e) {
                if (e.keyCode == 13) {
                    loginSubmit();
                    return false;
                }
            });

        });

        return {};
    }
);