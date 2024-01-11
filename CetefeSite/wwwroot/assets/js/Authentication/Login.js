function login() {
    var username = $("#username").val();
    var password = $("#password").val();

    if (!username || !password) {
        alert("Por favor entre com ambos Email e Senha");
        return;
    }

    $("#loading-spinner").show();

    $.ajax({
        url: 'Home/Login',
        type: 'POST',
        data: { username: username, password: password },
        success: function (data) {
            if (data) {
                window.location.href = "/Home/Index";
            } else {
                alert("Invalid username or password");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            var errorMessage = "Invalid username or password";

            if (xhr.status === 401) {
                errorMessage = "Unauthorized access";
            } else if (xhr.status === 500) {
                errorMessage = "Internal server error";
            }

            alert(errorMessage);
        },
        complete: function () {
            // Hide loading spinner regardless of success or error
            $("#loading-spinner").hide();
        }
    });
}

$(document).ready(function () {
    $("#Formlogin").submit(function (e) {
        e.preventDefault();
        login();
    });
});