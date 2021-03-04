$(document).ready(function () {
    GetCompanyDetails();
});
function hideall() {
    $("#companyDetails").hide();
    $("#branch").hide();
    $("#mailServerinfo").hide();
    $("#settings").hide();
    $("#templates").hide();
    $("#themes").hide();
}
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
            hideall();
            $("#companyDetails").show();
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
            hideall();
            $("#branch").show();

            HideLoader();
        },
        error: function (data) {
            console.log("error");
            console.log(data);
            HideLoader();
        }
    });
}
function AddBranch() {
    $("#addBranch").css("display", "none");
    $("#newBranch").css("display", "block");
    $("#IsActiveBranch").prop("checked", false);
}
function CancelBranch() {
    $("#txtNameBranch").val('');
    $("#txtCode").val('');
    $("#txtPostalCode").val('');
    $("#txtCountry").val('');
    $("#txtAddress1Branch").val('');
    $("#txtAddress2Branch").val('');
    $("#txtDistrict").val('');
    $("#txtState").val('');
    $("#txtPhone").val('');
    $("#txtEmail").val('');
    $("#hdnBranchId").val(0);
    $("#addBranch").css("display", "block");
    $("#newBranch").css("display", "none");
}
function EditBranch(e, i) {
    $("#addBranch").css("display", "none");
    $("#newBranch").css("display", "block");
    $("#txtNameBranch").val($("#txtNameBranch_" + e).text());
    $("#txtCode").val($("#txtCode_" + e).text());
    $("#txtPostalCode").val($("#txtPostalCode_" + e).text());
    $("#txtCountry").val($("#txtCountry_" + e).val());
    $("#txtAddress1Branch").val($("#txtAddress1Branch_" + e).val());
    $("#txtAddress2Branch").val($("#txtAddress2Branch_" + e).val());
    $("#txtDistrict").val($("#txtDistrict_" + e).text());
    $("#txtState").val($("#txtState_" + e).text());
    $("#txtPhone").val($("#txtPhone_" + e).text());
    $("#txtEmail").val($("#txtEmail_" + e).text());
    if (i == "True") {
        $("#IsActiveBranch").prop("checked", true);
    } else {
        $("#IsActiveBranch").prop("checked", false);
    }
    $("#hdnBranchId").val($("#hdnBranchId_" + e).val());
}
function AddEditBranch() {
    if ($("#txtNameBranch").val() == '') {
        MessageShow('', 'Name is Not Correct', 'error');
    }
    else if ($("#txtCode").val() == '') {
        MessageShow('', 'Code is Not Correct', 'error');
    }
    else if (!phonenumber($("#txtPhone").val())) {
        MessageShow('', 'Phone is Not Correct', 'error');
    }
    else if (!IsEmail($("#txtEmail").val())) {
        MessageShow('', 'Email is Not Correct', 'error');
    }
    else {
        var BranchId = parseInt($("#hdnBranchId").val());

        
        var Name = $("#txtNameBranch").val();
        var Code = $("#txtCode").val();
        var Address1 = $("#txtAddress1Branch").val();
        var Address2 = $("#txtAddress2Branch").val();
        var PostalCode = $("#txtPostalCode").val();
        var District = $("#txtDistrict").val();
        var State = $("#txtState").val();
        var Country = $("#txtCountry").val();
        var Phone = $("#txtPhone").val();
        var Email = $("#txtEmail").val();

        var isActive = $('#IsActiveBranch').is(':checked');
        var branchInfo = {
            CompanyId: 0,
            BranchId: BranchId,
            Name: Name,
            Code: Code,
            Address1: Address1,
            Address2: Address2,
            PostalCode: PostalCode,
            District: District,
            State: State,
            IsActive: isActive,
            Country: Country,
            Phone: Phone,
            Email: Email,
            CreatedBy: null,
            CreatedDate: null,
            ModifiedDate: null,
            ModifiedBy :null
        }
        $.ajax({
            url: baseURL + "Company/EditBranch",
            type: "POST",
            dataType: "html",
            data: branchInfo,
            success: function (data) {

                if (data !== "NO") {
                    CancelBranch()
                    $("#Branch").html(data);
                    MessageShow('', 'Branch saved', 'success');
                }
                else {
                    MessageShow('', 'Branch is not saved', 'error');
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
function DeleteBranch(e) {

    var TemplateId = $("#hdnBranchId_" + e).val();
    deleteBranch = {
        BranchId: TemplateId,
        CompanyId: 0,
        UserId: null
    }
    $.ajax({
        url: baseURL + "Company/DeleteBranch",
        type: "POST",
        dataType: "html",
        data: deleteBranch,
        success: function (data) {

            if (data !== "NO") {
                CancelBranch()
                $("#Branch").html(data);
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
function EditCompany() {
    if ($("#txtName").val() == '') {
        MessageShow('', 'Name is Not Correct', 'error');
    }
    else if (!phonenumber($("#txtAdminPhone").val())) {
        MessageShow('', 'Admin Phone is Not Correct', 'error');
    }
    else if (!IsEmail($("#txtAdminEmail").val())) {
        MessageShow('', 'Admin Email is Not Correct', 'error');
    }
    else if ($("#txtCurrencyCode").val() == '') {
        MessageShow('', 'Currency Code is Not Correct', 'error');
    }
    else if ($("#txtImageFilePath").val() == '') {
        MessageShow('', 'Image File Path is Not Correct', 'error');
    }
    else if ($("#txtShortname").val() == '') {
        MessageShow('', 'Short Name is Not Correct', 'error');
    }
    else if ($("#txtGSTNumber").val() != '' && !IsGST($("#txtGSTNumber").val())) {

        MessageShow('', 'GST Number is Not Correct', 'error');
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
            hideall();
            $("#mailServerinfo").show();

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
                if (data == "NO") {
                    MessageShow('', 'Not Saved', 'error');
                }
                else {
                    MessageShow('', 'SMTP Saved', 'success');
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
            hideall();
            $("#settings").show();

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
    $("#addCompanySetting").css("display", "none");
    $("#newCompanySetting").css("display", "block");
    $("#IsActiveCompanySetting").prop("checked", false);
}
function CancelCompanySetting() {
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
function EditCompanySetting(e,i) {
    $("#addCompanySetting").css("display", "none");
    $("#newCompanySetting").css("display", "block");
    $("#txtSettingType").val($("#txtSettingType_" + e).text());
    $("#txtDataText").val($("#txtDataText_" + e).text());
    $("#txtDataValue").val($("#txtDataValue_" + e).text());
    $("#txtOption1").val($("#txtOption1_" + e).text());
    $("#txtOption2").val($("#txtOption2_" + e).text());
    $("#txtOption3").val($("#txtOption3_" + e).text());
    if (i == "True") {
        $("#IsActiveCompanySetting").prop("checked", true);
    } else {
        $("#IsActiveCompanySetting").prop("checked", false);
    }
    $("#hdnCompanySettingId").val($("#hdnCompanySettingId_" + e).val());
}
function AddEditCompanySetting() {
    if ($("#txtSettingType").val() == '') {
        MessageShow('', 'Setting Type is Not Correct', 'error');
    }
    else if ($("#txtDataText").val() == '') {
        MessageShow('', 'Data Text is Not Correct', 'error');
    }
    else if ($("#txtDataValue").val() == '') {
        MessageShow('', 'Data Value is Not Correct', 'error');
    }
    else {
        var companySettingId = parseInt($("#hdnCompanySettingId").val());
        var settingType = $("#txtSettingType").val();
        var dataText = $("#txtDataText").val();
        var dataValue = $("#txtDataValue").val();
        var option1 = $("#txtOption1").val();
        var option2 = $("#txtOption2").val();
        var option3 = $("#txtOption3").val();
        var isActive = $('#IsActiveCompanySetting').is(':checked');
        var companySettingInfo = {
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
        $.ajax({
            url: baseURL + "Company/EditCompanySetting",
            type: "POST",
            dataType: "html",
            data: companySettingInfo,
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
function DeleteCompanysetting(e) {
    var CompanySettingsId = $("#hdnCompanySettingId_" + e).val();
    deleteCompanySetting = {
        CompanySettingsId: CompanySettingsId,
        CompanyId: 0,
        UserId: null
    }
    $.ajax({
        url: baseURL + "Company/DeleteCompanySetting",
        type: "POST",
        dataType: "html",
        data: deleteCompanySetting,
        success: function (data) {
            if (data !== "NO") {
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
            hideall();
            $("#templates").show();

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
    $("#addTemplate").css("display", "none");
    $("#newTemplate").css("display", "block");
    $("#IsActiveTemplate").prop("checked", false);
}
function CancelTemplate() {
    $("#txtTemplateType").val('');
    $("#txtName").val('');
    $("#txtTitle").val('');
    $("#txtHTMLData").val('');
    $("#hdnTemplateId").val(0);
    $("#addTemplate").css("display", "block");
    $("#newTemplate").css("display", "none");

}
function EditTemplate(e,i) {
    $("#addTemplate").css("display", "none");
    $("#newTemplate").css("display", "block");
    $("#txtTemplateType").val($("#txtTemplateType_" + e).text());
    var dd = $("#txtName_" + e).text();
    $("#txtNameTemplate").val($("#txtName_" + e).text());
    $("#txtTitle").val($("#txtTitle_" + e).text());
    $("#txtHTMLData").val($("#txtHTMLData_" + e).val());
    var d = $("#IsActive_" + e).val();
    if (i == "True") {
        $("#IsActiveTemplate").prop("checked", true);
    } else {
        $("#IsActiveTemplate").prop("checked", false);
    }
    $("#hdnTemplateId").val($("#hdnTemplateId_" + e).val());
}
function AddEditTemplate() {
    if ($("#txtTemplateType").val() == '') {
        MessageShow('', 'Template Type is Not Correct', 'error');
    }
    else if ($("#txtNameTemplate").val() == '') {
        MessageShow('', 'Name is Not Correct', 'error');
    }
    else if ($("#txtTitle").val() == '') {
        MessageShow('', 'Title is Not Correct', 'error');
    }

    else {
        var TemplateId = parseInt($("#hdnTemplateId").val());
        var TemplateType = $("#txtTemplateType").val();
        var Name = $("#txtNameTemplate").val();
        var Title = $("#txtTitle").val();
        var HTMLData = $("#txtHTMLData").val();
        var isActive = $('#IsActiveTemplate').is(':checked');
        var templateInfo = {
            CompanyId: 0,
            TemplateId: TemplateId,
            TemplateType: TemplateType,
            Name: Name,
            Title: Title,
            HTMLData: HTMLData,
            IsActive: isActive,
            CreatedBy: null
        }
        $.ajax({
            url: baseURL + "Company/EditTemplate",
            type: "POST",
            dataType: "html",
            data: templateInfo,
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
function DeleteTemplate(e) {

    var TemplateId = $("#hdnTemplateId_" + e).val();
    deleteTemplate = {
        TemplateId: TemplateId,
        CompanyId: 0,
        UserId: null
    }
    $.ajax({
        url: baseURL + "Company/DeleteTemplate",
        type: "POST",
        dataType: "html",
        data: deleteTemplate,
        success: function (data) {

            if (data !== "NO") {
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
function GetThemeDetails() {
    $.ajax({
        url: baseURL + "Company/GetThemeDetails",
        type: "GET",
        dataType: "html",
        data: "",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#Theme").html(data);
            hideall();
            $("#themes").show();

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
    $("#addTheme").css("display", "none");
    $("#newTheme").css("display", "block");
    $("#IsActiveTheme").prop("checked", false);
    $("#IsDefault").prop("checked", false);
}
function CancelTheme() {
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
function EditTheme(e, i, j) {
    $("#addTheme").css("display", "none");
    $("#newTheme").css("display", "block");
    $("#txtThemeName").val($("#txtThemeName_" + e).text());
    $("#txtExtThemeName").val($("#txtExtThemeName_" + e).text());
    $("#txtImageRatio").val($("#txtImageRatio_" + e).text());
    $("#txtColour").val($("#txtColour_" + e).text());
    $("#txtNoOfHomePanels").val($("#txtNoOfHomePanels_" + e).text());
    $("#txtMobileHeight").val($("#txtMobileHeight_" + e).text());
    $("#txtDesktopHeight").val($("#txtDesktopHeight_" + e).text());
    if (i == "True") {
        $("#IsActiveTheme").prop("checked", true);
    } else {
        $("#IsActiveTheme").prop("checked", false);
    }
    if (j == "True") {
        $("#IsDefault").prop("checked", true);
    } else {
        $("#IsDefault").prop("checked", false);
    }
    $("#hdnThemeId").val($("#hdnThemeId_" + e).val());
}
function AddEditTheme() {
    if ($("#txtThemeName").val() == '') {
        MessageShow('', 'ThemeName is blank', 'error');
    }
    else if ($("#txtImageRatio").val() == '') {
        MessageShow('', 'ImageRatio is blank', 'error');
    }
    else if ($("#txtColour").val() == '') {
        MessageShow('', 'Colour is blank', 'error');
    }
    else {
        var ThemeId = parseInt($("#hdnThemeId").val());
        var ThemeName = $("#txtThemeName").val();
        var ExtThemeName = $("#txtExtThemeName").val();
        var ImageRatio = $("#txtImageRatio").val();
        var NoOfHomePanels = $("#txtNoOfHomePanels").val();
        var Colour = $("#txtColour").val();
        var MobileHeight = $("#txtMobileHeight").val();
        var DesktopHeight = $("#txtDesktopHeight").val();
        var IsDefault = $('#IsDefault').is(':checked');
        var IsActive = $('#IsActiveTheme').is(':checked');
        var themeInfo = {
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
        $.ajax({
            url: baseURL + "Company/EditTheme",
            type: "POST",
            dataType: "html",
            data: themeInfo,
            success: function (data) {
                if (data !== "NO") {
                    CancelTheme();
                    $("#Theme").html(data);
                    MessageShow('', 'Theme saved', 'success');
                }
                else {
                    MessageShow('', 'Theme is not saved', 'error');
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
function DeleteTheme(e) {
    var ThemeId = $("#hdnThemeId_" + e).val();
    deleteTheme = {
        ThemeId: ThemeId,
        CompanyId: 0,
        UserId: null
    }
    $.ajax({
        url: baseURL + "Company/DeleteTheme",
        type: "POST",
        dataType: "html",
        data: deleteTheme,
        success: function (data) {
            if (data !== "NO") {
                $("#Theme").html(data);
                MessageShow('', 'Theme Deleted', 'success');
            }
            else {
                MessageShow('', 'Theme is not Deleted', 'error');
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

function phonenumber(inputtxt) {
    var phoneno = /^\d{10}$/;
    if ((inputtxt.match(phoneno)))
        {
        return true;
    }
    else {
        return false;
    }
}
function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}
function IsGST(GSTNo) {
    var reggst = /^([0-9]){2}([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}([0-9]){1}([a-zA-Z]){1}([0-9]){1}?$/;
    if (!reggst.test(GSTNo)) {
        return false;
    } else {
        return true;
    }
}

