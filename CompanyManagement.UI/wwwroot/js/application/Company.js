
function GetCompanyDetails() {
    var dt = baseURL + "Company/GetCompanydetails";
    $.ajax({
        url: baseURL + "Company/GetCompanydetails",
        type: "GET",
        dataType: "html",
        data: "",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#Company").html(data);
            HideLoader();
        },
        error: function (data) {
            console.log("error");
            console.log(data);
            HideLoader();
        }
    });
}
function GetBranchDetails() {
    $.ajax({
        url: baseURL + "Company/GetBranchdetails",
        type: "GET",
        dataType: "html",
        data: "",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#Branch").html(data);
            HideLoader();
        },
        error: function (data) {
            console.log("error");
            console.log(data);
            HideLoader();
        }
    });
}

function EditCompany() {
    debugger;
    if ($("#txtName").val() == '') {
        MessageShow('', 'Name is blank', 'error');
    }
    else if ($("#txtAdminPhone").val() == '') {
        MessageShow('', 'Admin Phone is blank', 'error');
    }
    else if ($("#txtAdminEmail").val() == '') {
        MessageShow('', 'Admin Email is blank', 'error');
    }
    else if ($("#txtCurrencyCode").val() == '') {
        MessageShow('', 'Currency Code is blank', 'error');
    }
    else if ($("#txtShortname").val() == '') {
        MessageShow('', 'Short Name is Blank', 'error');
    }
    else {
        var companyId = parseInt($("#txtCompanyId").val());
        var name = $("#txtName").val();
        var shortName = $("#txtShortname").val();
        var address1 = $("#txtAddress1").val();
        var address2 = $("#txtAddress2").val();
        var pin = $("#txtPIN").val();
        var districtCode = $("#txtDistrictCode").val();
        var stateCode = $("#txtStateCode").val();
        var countryCode = $("#txtCountryCode").val();
        var adminPhone = $("#txtAdminPhone").val();
        var servicePhone = $("#txtServicePhone").val();
        var buisnessType = $("#ddlBusinessType").val();
        var adminEmail = $("#txtAdminEmail").val();
        var serviceEmail = $("#txtServiceEmail").val();
        var secondaryEmail = $("#txtSecondaryEmail").val();
        var gstNumber = $("#txtGSTNumber").val();
        var panNumber = $("#txtPanNumber").val();
        var currencyCode = $("#txtCurrencyCode").val();
        var imageFilePath = $("#txtImageFilePath").val();
        var website = $("#txtWebsite").val();
        var logoFileName = $("#txtLogoFileName").val();
        var fabiconFileName = $("#txtFabiconFileName").val();
        var loginImageFileName = $("#txtLoginImageFileName").val();
        var isActive = $('#IsActive').is(':checked');
        var pinRequired = $('#PinRequired').is(':checked');

        debugger;
        var companyInfo = {
            CompanyId: companyId,
            Name: name,
            ShortName: shortName,
            Address1: address1,
            Address2: address2,
            PIN: pin,
            DistrictCode: districtCode,
            StateCode: stateCode,
            CountryCode: countryCode,
            AdminPhone: adminPhone,
            ServicePhone: servicePhone,
            AdminEmail: adminEmail,
            ServiceEmail: serviceEmail,
            SecondaryEmail: secondaryEmail,
            GSTNumber: gstNumber,
            PanNumber: panNumber,
            BusinessType: buisnessType,
            CurrencyCode: currencyCode,
            ImageFilePath: imageFilePath,
            LogoFileName: logoFileName,
            FavIconFileName: fabiconFileName,
            LoginImageFileName: loginImageFileName,
            Website: website,
            PINRequired: pinRequired,
            IsActive: isActive
        }
        $.ajax({
            url: baseURL + "Company/EditCompany",
            type: "POST",
            dataType: "html",
            data: companyInfo,
            success: function (data) {
                debugger;
                if (data == "NO") {
                    MessageShow('', 'Not Saved', 'error');
                }
                else {
                    MessageShow('', 'Company Saved', 'success');
                }
                HideLoader();
            },
            error: function (data) {
                MessageShow('', 'Something Went Wrong', 'error');
                console.log("error");
                console.log(data);
                HideLoader();
            }
        });
    }
}
function GetMailDetails() {
    var dt = baseURL + "Company/GetMailDetails";
    $.ajax({
        url: baseURL + "Company/GetMailDetails",
        type: "GET",
        dataType: "html",
        data: "",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#MailServer").html(data);
            HideLoader();
        },
        error: function (data) {
            console.log("error");
            console.log(data);
            HideLoader();
        }
    });
}
function EditMailServer() {
    if ($("#txtSTMPServer").val() == '') {
        MessageShow('', 'STMPServer is blank', 'error');
    }
    else if ($("#txtSTMPPort").val() == '') {
        MessageShow('', 'STMPPort is blank', 'error');
    }
    else if ($("#fromEmailDisplayName").val() == '') {
        MessageShow('', 'fromEmailDisplayName is blank', 'error');
    }
    else {
        var companyId = parseInt($("#hdnCompanyId").val());
        var stmpServerId = parseInt($("#hdnStmpServerId").val());
        var stmpServer = $("#txtSTMPServer").val();
        var stmpPort = $("#txtSTMPPort").val();
        var fromEmailDisplayName = $("#txtFromEmailDisplayName").val();
        var fromEmailId = $("#txtFromEmailId").val();
        var fromEmailIdPwd = $("#txtFromEmailIdPwd").val();
        var isActive = $('#IsActive').is(':checked');
        var enableSSL = $('#EnableSSL').is(':checked');


        var stmpInfo = {
            CompanyId: companyId,
            MailServerId: stmpServerId,
            SMTPServer: stmpServer,
            SMTPPort: stmpPort,
            FromEmailDisplayName: fromEmailDisplayName,
            FromEmailId: fromEmailId,
            FromEmailPwd: fromEmailIdPwd,
            EnableSSL: enableSSL,
            IsActive: isActive
        }
        $.ajax({
            url: baseURL + "Company/EditSTMPServer",
            type: "POST",
            dataType: "html",
            data: stmpInfo,
            success: function (data) {
                debugger;
                if (data == "NO") {
                    MessageShow('', 'Not Saved', 'error');
                }
                else {
                    MessageShow('', 'SMTP Saved', 'success');
                }
                HideLoader();
            },
            error: function (data) {
                debugger;
                MessageShow('', 'Something Went Wrong', 'error');
                console.log("error");
                console.log(data);
                HideLoader();
            }
        });
    }
}
function GetCompanySettingsDetails() {
    var dt = baseURL + "Company/GetCompanySettingsDetails";
    $.ajax({
        url: baseURL + "Company/GetCompanySettingsDetails",
        type: "GET",
        dataType: "html",
        data: "",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#CompanySettings").html(data);
            HideLoader();
        },
        error: function (data) {
            console.log("error");
            console.log(data);
            HideLoader();
        }
    });
}
function AddCompanySetting() {
    debugger;
    $("#addCompanySetting").css("display", "none");
    $("#newCompanySetting").css("display", "block");

}
function CancelCompanySetting() {
    debugger;
    $("#txtSettingType").val('');
    $("#txtDataText").val('');
    $("#txtDataValue").val('');
    $("#txtOption1").val('');
    $("#txtOption2").val('');
    $("#txtOption3").val('');
    $("#hdnCompanySettingId").val(0);
    $("#addCompanySetting").css("display", "block");
    $("#newCompanySetting").css("display", "none");

}
function EditCompanySetting(e) {
    $("#addCompanySetting").css("display", "none");
    $("#newCompanySetting").css("display", "block");
    $("#txtSettingType").val($("#txtSettingType_" + e).val());
    $("#txtDataText").val($("#txtDataText_" + e).val());
    $("#txtDataValue").val($("#txtDataValue_" + e).val());
    $("#txtOption1").val($("#txtOption1_" + e).val());
    $("#txtOption2").val($("#txtOption2_" + e).val());
    $("#txtOption3").val($("#txtOption3_" + e).val());
    if ($("#IsActive_" + e).prop("checked")) {
        $("#IsActive").prop("checked", true);
    } else {
        $("#IsActive").prop("checked", false);
    }
    $("#hdnCompanySettingId").val($("#hdnCompanySettingId_" + e).val());
}
function AddEditCompanySetting() {
    var companySettingId = parseInt($("#hdnCompanySettingId").val());
    var settingType = $("#txtSettingType").val();
    var dataText = $("#txtDataText").val();
    var dataValue = $("#txtDataValue").val();
    var option1 = $("#txtOption1").val();
    var option2 = $("#txtOption2").val();
    var option3 = $("#txtOption3").val();
    var isActive = $('#IsActive').is(':checked');
        var stmpInfo = {
        CompanyId: 0,
        CompanySettingId: companySettingId,
        SettingType: settingType,
        DataText: dataText,
        DataValue: dataValue,
        Option1: option1,
        Option2: option2,
        Option3: option3,
        IsActive: isActive,
        CreatedBy: null
    }

    debugger;
    $.ajax({
        url: baseURL + "Company/EditCompanySetting",
        type: "POST",
        dataType: "html",
        data: stmpInfo,
        success: function (data) {
            if (data !== "NO") {
                CancelCompanySetting()
                $("#CompanySettings").html(data);
                MessageShow('', 'Company Setting saved', 'success');
            }
            else {
                MessageShow('', 'Company Setting is not saved', 'error');
            }
            HideLoader();
        },
        error: function (data) {
            MessageShow('', 'Something Went Wrong', 'error');
            console.log("error");
            console.log(data);
            HideLoader();
        }
    });

}
function GetTemplateDetails() {
    $.ajax({
        url: baseURL + "Company/GetTemplateDetails",
        type: "GET",
        dataType: "html",
        data: "",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#Template").html(data);
            HideLoader();
        },
        error: function (data) {
            console.log("error");
            console.log(data);
            HideLoader();
        }
    });
}
function GetThemeDetails() {
    $.ajax({
        url: baseURL + "Company/GetThemeDetails",
        type: "GET",
        dataType: "html",
        data: "",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#Theme").html(data);
            HideLoader();
        },
        error: function (data) {
            console.log("error");
            console.log(data);
            HideLoader();
        }
    });
}



