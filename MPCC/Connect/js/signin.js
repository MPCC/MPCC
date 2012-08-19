$(document).ready(function () {
    $("#btnSignIn").click(function () {
        $.ajax({
            url: "http://localhost/Rest2/Auth/v1/tokenrequest",
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ username: $("#username").val(), password: $("#password").val() }),
            success: function () {
                document.location = 'home.html';
            },
            error: function (err) {
                console.log('error logging in');
            }
        });
    });
    $('input').keydown(function (e) {
        if (e.keyCode == 13) {
            $('#btnSignIn').click();
            return false;
        }
    });
    $('#username').focus();
});
