
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
function EditCompanySetting() {
    var postData = [];
    var rowCount = parseInt($("#hdnRowcount").val());
    for (var i = 1; i <= rowCount; i++) {
        var companyId = parseInt($("#hdnCompanyId_" + i).val());
        var companySettingId = parseInt($("#hdnStmpServerId_" + i).val());
        var settingType = $("#txtSettingType_" + i).val();
        var dataText = $("#txtDataText_" + i).val();
        var dataValue = $("#txtDataValue_" + i).val();
        var option1 = $("#txtOption1_" + i).val();
        var option2 = $("#txtOption2_" + i).val();
        var option3 = $("#txtOption3_" + i).val();
        var isActive = $('#IsActive_' + i).is(':checked');
        var stmpInfo = {
            CompanyId: companyId,
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
        postData.push(stmpInfo);

    }
    var comSetting = { ListData: postData }
    $.ajax({
        url: baseURL + "Company/EditCompanySetting",
        type: "POST",
        dataType: "json",
        data: comSetting,
        success: function (data) {
            HideLoader();
        },
        error: function (data) {
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


