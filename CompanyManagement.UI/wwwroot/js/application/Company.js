
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
    debugger;
    if ($("#txtSettingType").val() == '') {
        MessageShow('', 'SettingType is blank', 'error');
    }
    else if ($("#txtDataText").val() == '') {
        MessageShow('', 'DataText is blank', 'error');
    }
    else if ($("#txtDataValue").val() == '') {
        MessageShow('', 'DataValue is blank', 'error');
    }
    else {
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
function AddTemplate() {
    debugger;
    $("#addTemplate").css("display", "none");
    $("#newTemplate").css("display", "block");

}
function CancelTemplate() {
    debugger;
    $("#txtTemplateType").val('');
    $("#txtName").val('');
    $("#txtTitle").val('');
    $("#txtHTMLData").val('');
    $("#hdnTemplateId").val(0);
    $("#addTemplate").css("display", "block");
    $("#newTemplate").css("display", "none");

}
function EditTemplate(e) {
    debugger;
    $("#addTemplate").css("display", "none");
    $("#newTemplate").css("display", "block");
    $("#txtTemplateType").val($("#txtTemplateType_" + e).val());
    $("#txtName").val($("#txtName_" + e).val());
    $("#txtTitle").val($("#txtTitle_" + e).val());
    $("#txtHTMLData").val($("#txtHTMLData_" + e).val());
    if ($("#IsActive_" + e).prop("checked")) {
        $("#IsActive").prop("checked", true);
    } else {
        $("#IsActive").prop("checked", false);
    }
    $("#hdnTemplateId").val($("#hdnTemplateId_" + e).val());
}
function AddEditTemplate() {
    if ($("#txtTemplateType").val() == '') {
        MessageShow('', 'TemplateType is blank', 'error');
    }
    else if ($("#txtName").val() == '') {
        MessageShow('', 'Name is blank', 'error');
    }
    else if ($("#txtTitle").val() == '') {
        MessageShow('', 'Title is blank', 'error');
    }
    else if ($("#txtHTMLData").val() == '') {
        MessageShow('', 'HTMLData is blank', 'error');
    }
    else {
        var TemplateId = parseInt($("#hdnTemplateId").val());
        var TemplateType = $("#txtTemplateType").val();
        var Name = $("#txtName").val();
        var Title = $("#txtTitle").val();
        var HTMLData = $("#txtHTMLData").val();
        var isActive = $('#IsActive').is(':checked');
        var stmpInfo = {
            CompanyId: 0,
            TemplateId: TemplateId,
            TemplateType: TemplateType,
            Name: Name,
            Title: Title,
            HTMLData: HTMLData,
            IsActive: isActive,
            CreatedBy: null
        }

        debugger;
        $.ajax({
            url: baseURL + "Company/EditTemplate",
            type: "POST",
            dataType: "html",
            data: stmpInfo,
            success: function (data) {
                if (data !== "NO") {
                    CancelCompanySetting();
                    $("#Template").html(data);
                    MessageShow('', 'Template saved', 'success');
                }
                else {
                    MessageShow('', 'Template is not saved', 'error');
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
function AddTheme() {
    debugger;
    $("#addTheme").css("display", "none");
    $("#newTheme").css("display", "block");

}
function CancelTheme() {
    debugger;
    $("#txtThemeName").val('');
    $("#txtExtThemeName").val('');
    $("#txtImageRatio").val('');
    $("#txtColour").val('');
    $("#txtNoOfHomePanels").val('');
    $("#txtMobileHeight").val('');
    $("#txtDesktopHeight").val('');
    $("#hdnThemeId").val(0);
    $("#addTheme").css("display", "block");
    $("#newTheme").css("display", "none");

}
function EditTheme(e) {
    debugger;
    $("#addTheme").css("display", "none");
    $("#newTheme").css("display", "block");
    $("#txtThemeName").val($("#txtThemeName_" + e).val());
    $("#txtExtThemeName").val($("#txtExtThemeName_" + e).val());
    $("#txtImageRatio").val($("#txtImageRatio_" + e).val());
    $("#txtColour").val($("#txtColour_" + e).val());
    $("#txtNoOfHomePanels").val($("#txtNoOfHomePanels_" + e).val());
    $("#txtMobileHeight").val($("#txtMobileHeight_" + e).val());
    $("#txtDesktopHeight").val($("#txtDesktopHeight_" + e).val());
    if ($("#IsActive_" + e).prop("checked")) {
        $("#IsActive").prop("checked", true);
    } else {
        $("#IsActive").prop("checked", false);
    }
    if ($("#IsDefault_" + e).prop("checked")) {
        $("#IsDefault").prop("checked", true);
    } else {
        $("#IsDefault").prop("checked", false);
    }
    $("#hdnThemeId").val($("#hdnThemeId" + e).val());
}
function AddEditTheme() {
    if ($("#txtThemeName").val() == '') {
        MessageShow('', 'ThemeName is blank', 'error');
    }
    else if ($("#txtExtThemeName").val() == '') {
        MessageShow('', 'ExtThemeName is blank', 'error');
    }
    else if ($("#txtImageRatio").val() == '') {
        MessageShow('', 'ImageRatio is blank', 'error');
    }
    else if ($("#txtColour").val() == '') {
        MessageShow('', 'Colour is blank', 'error');
    }
    else {
        var ThemeId = parseInt($("#txtThemeName").val());
        var ThemeName = $("#txtThemeName").val();
        var ExtThemeName = $("#txtExtThemeName").val();
        var ImageRatio = $("#txtImageRatio").val();
        var NoOfHomePanels = $("#txtColour").val();
        var Colour = $("#txtNoOfHomePanels").val();
        var MobileHeight = $("#txtMobileHeight").val();
        var DesktopHeight = $("#txtDesktopHeight").val();
        var IsDefault = $('#IsDefault').is(':checked');
        var IsActive = $('#IsActive').is(':checked');
        var stmpInfo = {
            CompanyId: 0,
            ThemeId: ThemeId,
            ThemeName: ThemeName,
            ExtThemeName: ExtThemeName,
            ImageRatio: ImageRatio,
            NoOfHomePanels: NoOfHomePanels,
            Colour: Colour,
            MobileHeight: MobileHeight,
            DesktopHeight: DesktopHeight,
            IsActive: IsActive,
            IsDefault: IsDefault,
            CreatedBy: null
        }

        debugger;
        $.ajax({
            url: baseURL + "Company/EditTheme",
            type: "POST",
            dataType: "html",
            data: stmpInfo,
            success: function (data) {
                if (data !== "NO") {
                    CancelTheme();
                    $("#Template").html(data);
                    MessageShow('', 'Template saved', 'success');
                }
                else {
                    MessageShow('', 'Template is not saved', 'error');
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



