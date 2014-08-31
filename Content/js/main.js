var globals = {
    LoginUrl: '',
    MainUrl: '',
};

$(document).ready(function () {
    WireUpClickEventHandlers();
});

function Initialise(loginUrl) {
    globals.LoginUrl = loginUrl + "?pageId=1";
}

function WireUpClickEventHandlers() {
    $("#login").click(function() {
        var username = $("#username").val();
        var password = $("#password").val();

        var data = {
            username: username,
            password: password
        };

        Ajax(globals.LoginUrl, data);
    });
}

function Ajax(uri, json) {
    $.ajax({
        url: uri,
        type: "POST",
        contentType: 'application/json',
        dataType: "json",
        data: JSON.stringify(json),
        success: function (data) {
            if (data.Authorised) {
                window.location = data.RedirectUrl;
            }
        }
    });
}
