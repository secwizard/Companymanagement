

function FocusCompany() {
    $("#companyDetails").show();
    $("#branch").hide();
    $("#mailServerinfo").hide();
    $("#settings").hide();
    $("#templates").hide();
    $("#themes").hide();
}
function FocusBranch() {
    $("#companyDetails").hide();
    $("#branch").show();
    $("#mailServerinfo").hide();
    $("#settings").hide();
    $("#templates").hide();
    $("#themes").hide();
}
function FocusMail() {
    $("#companyDetails").hide();
    $("#branch").hide();
    $("#mailServerinfo").show();
    $("#settings").hide();
    $("#templates").hide();
    $("#themes").hide();
}
function FocusSetting() {
    $("#companyDetails").hide();
    $("#branch").hide();
    $("#mailServerinfo").hide();
    $("#settings").show();
    $("#templates").hide();
    $("#themes").hide();
}
function FocusTemplate() {
    $("#companyDetails").hide();
    $("#branch").hide();
    $("#mailServerinfo").hide();
    $("#settings").hide();
    $("#templates").show();
    $("#themes").hide();
}
function FocusTheme() {
    $("#companyDetails").hide();
    $("#branch").hide();
    $("#mailServerinfo").hide();
    $("#settings").hide();
    $("#templates").hide();
    $("#themes").show();
}
function GetCompanyDetails() {
    $.ajax({
        url: baseURL + "OnBoard/GetNewCompanydetails",
        type: "POST",
        dataType: "html",
        data: {},
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
function GetSuggestedCompanyId() {
    var postData = {
        CompanyId: 0,
        Type: $("#ddlBusinessType").val()
    }

    $.ajax({
        url: baseURL + "OnBoard/GetSuggestedCompanyId",
        type: "POST",
        dataType: "json",
        data: postData,
        success: function (data) {

            $("#txtCompanyId").val(data.companyId);
            HideLoader();
        },
        error: function (data) {

            console.log("error");
            console.log(data);
            HideLoader();
        }
    });
}
function AddCompany() {


    var flag = true;
    var err = "";
    if ($("#txtName").val() == '') {
        flag = false;
        err = err != "" ? err + ", Name " : " Name ";
    }
    if (!phonenumber($("#txtAdminPhone").val())) {
        flag = false;
        err = err != "" ? err + ", Admin Phone " : " Admin Phone ";
    }
    if (!IsEmail($("#txtAdminEmail").val())) {
        flag = false;
        err = err != "" ? err + ", Admin Email " : " Admin Email ";
    }
    if ($("#txtCurrencyCode").val() == '') {
        flag = false;
        err = err != "" ? err + ", Currency Code " : " Currency Code ";
    }
    if ($("#txtImageFilePath").val() == '') {
        flag = false;
        err = err != "" ? err + ", Image File Path " : " Image File Path ";
    }
    if ($("#txtShortname").val() == '') {
        flag = false;
        err = err != "" ? err + ", Short Name " : " Short Name ";
    }
    if ($("#txtGSTNumber").val() != '' && !IsGST($("#txtGSTNumber").val())) {
        flag = false;
        err = err != "" ? err + ", GST " : " GST ";
    }
    if (!flag) {
        //$("#OnompanyDetailsError").text("* " + err + "is Not Correct");
        alert("no");
    }
    //else {

    //}
    //else {
    //    var myJsVariable = newCompanyId;
    //    var companyId = parseInt($("#txtCompanyId").val());
    //    var name = $("#txtName").val();
    //    var shortName = $("#txtShortname").val();
    //    var address1 = $("#txtAddress1").val();
    //    var address2 = $("#txtAddress2").val();
    //    var pin = $("#txtPIN").val();
    //    var districtCode = $("#txtDistrictCode").val();
    //    var stateCode = $("#txtStateCode").val();
    //    var countryCode = $("#txtCountryCode").val();
    //    var adminPhone = $("#txtAdminPhone").val();
    //    var servicePhone = $("#txtServicePhone").val();
    //    var buisnessType = $("#ddlBusinessType").val();
    //    var adminEmail = $("#txtAdminEmail").val();
    //    var serviceEmail = $("#txtServiceEmail").val();
    //    var secondaryEmail = $("#txtSecondaryEmail").val();
    //    var gstNumber = $("#txtGSTNumber").val();
    //    var panNumber = $("#txtPanNumber").val();
    //    var currencyCode = $("#txtCurrencyCode").val();
    //    var imageFilePath = $("#txtImageFilePath").val();
    //    var website = $("#txtWebsite").val();
    //    var logoFileName = $("#txtLogoFileName").val();
    //    var fabiconFileName = $("#txtFabiconFileName").val();
    //    var loginImageFileName = $("#txtLoginImageFileName").val();
    //    var isActive = $('#IsActive').is(':checked');
    //    var pinRequired = $('#PinRequired').is(':checked');


    //    var companyInfo = {
    //        CompanyId: companyId,
    //        Name: name,
    //        ShortName: shortName,
    //        Address1: address1,
    //        Address2: address2,
    //        PIN: pin,
    //        DistrictCode: districtCode,
    //        StateCode: stateCode,
    //        CountryCode: countryCode,
    //        AdminPhone: adminPhone,
    //        ServicePhone: servicePhone,
    //        AdminEmail: adminEmail,
    //        ServiceEmail: serviceEmail,
    //        SecondaryEmail: secondaryEmail,
    //        GSTNumber: gstNumber,
    //        PanNumber: panNumber,
    //        BusinessType: buisnessType,
    //        CurrencyCode: currencyCode,
    //        ImageFilePath: imageFilePath,
    //        LogoFileName: logoFileName,
    //        FavIconFileName: fabiconFileName,
    //        LoginImageFileName: loginImageFileName,
    //        Website: website,
    //        PINRequired: pinRequired,
    //        IsActive: isActive
    //    }
    //    $.ajax({
    //        url: baseURL + "OnBoard/AddCompany",
    //        type: "GET",
    //        dataType: "json",
    //        data: companyInfo,
    //        contentType: "application/json; charset=utf-8",
    //        success: function (data) {
    //            HideLoader();
    //        },
    //        error: function (data) {
    //            console.log("error");
    //            console.log(data);
    //            HideLoader();
    //        }
    //    });
    //}
}
function phonenumber(inputtxt) {
    var phoneno = /^\d{10}$/;
    if ((inputtxt.match(phoneno))) {
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
    BranchOnFocus();
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
    TemplateOnFocus();
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
    ThemesOnFocus();

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
    SettingOnFocus();
}
function CompanyOnFocus() {
    $("#companyDetailsError").text("");
}
function BranchOnFocus() {
    $("#branchDetailsError").text("");
}
function MailOnFocus() {
    $("#companymailDetailsError").text("");
}
function SettingOnFocus() {
    $("#companySettingsDetailsError").text("");
}
function TemplateOnFocus() {
    $("#companyTemplateDetailsError").text("");
}
function ThemesOnFocus() {
    $("#companyThemesDetailsError").text("");
}

function AddEditBranch() {
    var count = $('#hdnRowCntBranch').val();
    var flag = true;
    var err = "";
    if ($("#txtCode").val() == '') {
        flag = false;
        err = err != "" ? err + ", Branch Code " : " Branch Code ";
    }
    if ($("#txtNameBranch").val() == '') {
        flag = false;
        err = err != "" ? err + ", Name " : " Name ";
    }
    if (!phonenumber($("#txtPhone").val())) {
        flag = false;
        err = err != "" ? err + ", Phone " : " Phone ";
    }
    if (!IsEmail($("#txtEmail").val())) {
        flag = false;
        err = err != "" ? err + ", Email " : " Email ";
    }
    if ($("#txtCountry").val() == '') {
        flag = false;
        err = err != "" ? err + ", Country " : " Country ";
    }
    if (!flag) {
        $("#branchDetailsError").text("* " + err + "is required.");
    }
    else {
        var state = parseInt($("#hdnBranchId").val());
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
        var html = '';
        html += '<tr id="branchtr_' + count + '"><td style="width:10%" id="txtCode_' + count + '">' + Code + '</td>';
        html += '<td style="width:20%" id="txtNameBranch_' + count + '">' + Name + '</td>';
        html += '<td style="width:10%" id="txtEmail_' + count + '">' + Email + '</td>';
        html += '<td style="width:10%" id="txtPhone_' + count + '">' + Phone + '</td>';
        html += '<td style="width:10%" id="txtPostalCode_' + count + '">' + PostalCode + '</td>';
        html += '<td style="width:10%" id="txtDistrict_' + count + '">' + District + '</td>';
        html += '<td style="width:10%" id="txtState_' + count + '">' + State + '</td>';
        html += '<td style="width:10%" class="text-center">';
        html += '<i class="fas fa-pencil-alt" style="cursor:pointer" onclick="EditBranch(' + count + ')"></i> | <i class="fas fa-trash-alt" style="cursor:pointer" onclick="DeleteBranch(' + count + ')"></i></td>';
        html += '<td style="width:0%" class="text-center">';
        html += '<input type="hidden" id="txtCountry_' + count + '" value="' + Country + '" />';
        html += '<input type="hidden" id="txtAddress1Branch_' + count + '" value="' + Address1 + '" />';
        html += ' <input type="hidden" id="txtAddress2Branch_' + count + '" value="' + Address2 + '"/>';
        html += '</td> </tr>';
        if (state > 0) {
            $('#branchtr_' + state).remove();
            $("#tbl_OnBoard_Branch_body").prepend(html);
        }
        else {
            $("#tbl_OnBoard_Branch_body").prepend(html);
        }
        $('#hdnRowCntBranch').val(parseInt($('#hdnRowCntBranch').val()) + 1);
        $("#hdnBranchId").val('0');
        CancelBranch();
        BranchOnFocus();
    }
}
function DeleteBranch(e) {
    $('#branchtr_' + e).remove();
}
function EditBranch(e) {
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
    $("#hdnBranchId").val(e);
}

function AddEditTheme() {
    var count = $('#hdnRowCntBranch').val();
    var flag = true;
    var err = "";
    if ($("#txtThemeName").val() == '') {
        flag = false;
        err = err != "" ? err + ", Theme Name " : " Theme Name ";
    }
    if (!flag) {
        $("#companyThemesDetailsError").text("* " + err + "is required.");
    }
    else {
        var state = parseInt($("#hdnThemeId").val());
        var ThemeName = $("#txtThemeName").val();
        var ExtThemeName = $("#txtExtThemeName").val();
        var ImageRatio = $("#txtImageRatio").val();
        var NoOfHomePanels = $("#txtNoOfHomePanels").val();
        var Colour = $("#txtColour").val();
        var MobileHeight = $("#txtMobileHeight").val();
        var DesktopHeight = $("#txtDesktopHeight").val();
        //var IsDefault = $('#IsDefault').is(':checked');
        //var IsActive = $('#IsActiveTheme').is(':checked');
        var html = '';

        html += '<tr id="themetr_' + count + '"><td style="width:20%" id="txtThemeName_' + count + '">' + ThemeName + '</td>';
        html += '<td style="width:10%" id="txtExtThemeName_' + count + '">' + ExtThemeName + '</td>';
        html += '<td style="width:10%" id="txtImageRatio_' + count + '">' + ImageRatio + '</td>';
        html += '<td style="width:10%" id="txtColour_' + count + '">' + Colour + '</td>';
        html += '<td style="width:10%" id="txtNoOfHomePanels_' + count + '">' + NoOfHomePanels + '</td>';
        html += '<td style="width:10%" id="txtMobileHeight_' + count + '">' + MobileHeight + '</td>';
        html += '<td style="width:10%" id="txtDesktopHeight_' + count + '">' + DesktopHeight + '</td>';
        html += '<td style="width:10%" class="text-center">';
        html += '<i class="fas fa-pencil-alt" style="cursor:pointer" onclick="EditTheme(' + count + ')"></i> | <i class="fas fa-trash-alt" style="cursor:pointer" onclick="DeleteTheme(' + count + ')"></i></td>';
        html += '<td style="width:0%" class="text-center">';
        if (state > 0) {
            $('#themetr_' + state).remove();
            $("#tbl_OnBoard_Theme_body").prepend(html);
        }
        else {
            $("#tbl_OnBoard_Theme_body").prepend(html);
        }
        $('#hdnRowCntTheme').val(parseInt($('#hdnRowCntBranch').val()) + 1);
        $("#hdnBranchId").val('0');
        CancelTheme();
        ThemesOnFocus();
    }
}
function EditTheme(e) {
    debugger;
    $("#addTheme").css("display", "none");
    $("#newTheme").css("display", "block");
    $("#txtThemeName").val($("#txtThemeName_" + e).text());
    $("#txtExtThemeName").val($("#txtExtThemeName_" + e).text());
    $("#txtImageRatio").val($("#txtImageRatio_" + e).text());
    $("#txtColour").val($("#txtColour_" + e).text());
    $("#txtNoOfHomePanels").val($("#txtNoOfHomePanels_" + e).text());
    $("#txtMobileHeight").val($("#txtMobileHeight_" + e).text());
    $("#txtDesktopHeight").val($("#txtDesktopHeight_" + e).text());
    //if (i == "True") {
    //    $("#IsActiveTheme").prop("checked", true);
    //} else {
    //    $("#IsActiveTheme").prop("checked", false);
    //}
    //if (j == "True") {
    //    $("#IsDefault").prop("checked", true);
    //} else {
    //    $("#IsDefault").prop("checked", false);
    //}
    $("#hdnThemeId").val(e);
}
function DeleteTheme(e) {
    $('#themetr_' + e).remove();
}