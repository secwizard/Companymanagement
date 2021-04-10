function LogAfterValidate() {
    try {
        //ShowLoader();
        var strCompany = document.getElementById('ddlCompany').value;
        var PostSecurityManagerData = {
            CompanyId: strCompany
        };

        if (strCompany == 0) {
            window.location = baseURL + "OnBoard/Index";
        }
        else {

            $.ajax({
                url: baseURL + "Login/LogVerify",
                type: "POST",
                dataType: "json",
                data: PostSecurityManagerData,
                success: function (data) {
                    if (data.errorMessage == "OK") {
                        window.location = baseURL + data.returnUrl;
                        LogonHideLoader();
                    }
                    else {
                        document.getElementById('spanDatasaved').innerHTML = data.errorMessage;
                        LogonHideLoader();
                    }
                },
                error: function (data) {
                    alert(2);
                    LogonHideLoader();
                }

            });
        }
    }
    catch (e) {
        console.log("catch", e);
        ConLog(e);
        LogonHideLoader();
    }
}

function LogonUser() {
    try {
    LogonShowLoader();
        
        //ShowLoader();
        var strUserName = document.getElementById('userid').value;
        var strPassword = document.getElementById('userpassword').value;
        if (strUserName == "") {
            document.getElementById('spanDatasaved').innerHTML = "Email Address Is Required";
            $("#userid").focus();
            LogonHideLoader();
            return false;
        }
        if (strPassword == "") {
            document.getElementById('spanDatasaved').innerHTML = "Password Is Required";
            $("#userpassword").focus();
            LogonHideLoader();
            return false;
        }
        var PostSecurityManagerData = {
            UserName: strUserName,
            Password: strPassword,
            ReturnUrl: "",
            ErrorMessage: ""
        };

        $.ajax({
            url: baseURL + "Login/LogVerify",
            type: "POST",
            dataType: "json",
            data: PostSecurityManagerData,
            success: function (data) {
                if (data.errorMessage == "OK") {
                    window.location = baseURL + data.returnUrl;
                    LogonHideLoader();
                }
                else {
                    document.getElementById('spanDatasaved').innerHTML = data.errorMessage;
                    LogonHideLoader();
                }
            },
            error: function (data) {
                alert(2);
                LogonHideLoader();
            }

        });
    }
    catch (e) {
        console.log("catch", e);
        ConLog(e);
        LogonHideLoader();
    }
}

$(document).keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        LogonUser();
    }
});

function hidemsg() {
    $('#spanDatasaved').text('');
}

function LogonShowLoader() {
    $('#divloader').show();
}

function LogonHideLoader() {
    $('#divloader').hide();
}