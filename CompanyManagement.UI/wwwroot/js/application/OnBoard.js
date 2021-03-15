

function numberValidation(e) {
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
        (e.keyCode == 65 && e.ctrlKey === true) ||
        (e.keyCode >= 35 && e.keyCode <= 40)) {
        return;
    }
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
}
function numberValidationWithoutDot(e) {
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 190]) !== -1 ||
        (e.keyCode == 65 && e.ctrlKey === true) ||
        (e.keyCode >= 35 && e.keyCode <= 40)) {
        return;
    }
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
}
function focusstep1() {
    $("#step-1").show();
    $("#step-2").hide();
    $("#step-3").hide();
    $("#step-4").hide();
    $("#step-5").hide();
}
function focusstep2() {
    $("#step-1").hide();
    $("#step-2").show();
    $("#step-3").hide();
    $("#step-4").hide();
    $("#step-5").hide();
}
function focusstep4() {
    $("#step-1").hide();
    $("#step-2").hide();
    $("#step-3").hide();
    $("#step-4").show();
    $("#step-5").hide();
}
function FocusSubscription() {
    $("#OnBoardSubscriptionError").text("");
}
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
        alert("no");
    }

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
    $("#txtNameTemplate").val('');
    $("#txtTitle").val('');
    //$("#txtHTMLData").val('');
    $("#txtHTMLData").val("").trigger('change');
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
function ConfigurationOnFocus() {
    $("#OnBoardConfigurationError").text("");
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
    var count = $('#hdnRowCntTheme').val();
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
        var IsDefault = $('#IsDefault').is(':checked');
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
        if (IsDefault) {
            html += '<input type="hidden" id="isDefault_' + count + '" value="1" /></td ></tr >';
        }
        else {
            html += '<input type="hidden" id="isDefault_' + count + '" value="0" /></td ></tr >';
        }
        if (state > 0) {
            $('#themetr_' + state).remove();
            $("#tbl_OnBoard_Theme_body").prepend(html);
        }
        else {
            $("#tbl_OnBoard_Theme_body").prepend(html);
        }
        $('#hdnRowCntTheme').val(parseInt($('#hdnRowCntTheme').val()) + 1);
        $("#hdnThemeId").val('0');
        CancelTheme();
        ThemesOnFocus();
    }
}
function EditTheme(e) {
    $("#addTheme").css("display", "none");
    $("#newTheme").css("display", "block");
    $("#txtThemeName").val($("#txtThemeName_" + e).text());
    $("#txtExtThemeName").val($("#txtExtThemeName_" + e).text());
    $("#txtImageRatio").val($("#txtImageRatio_" + e).text());
    $("#txtColour").val($("#txtColour_" + e).text());
    $("#txtNoOfHomePanels").val($("#txtNoOfHomePanels_" + e).text());
    $("#txtMobileHeight").val($("#txtMobileHeight_" + e).text());
    $("#txtDesktopHeight").val($("#txtDesktopHeight_" + e).text());
    var isDefault = parseInt($("#isDefault_" + e).val());
    //if (i == "True") {
    //    $("#IsActiveTheme").prop("checked", true);
    //} else {
    //    $("#IsActiveTheme").prop("checked", false);
    //}
    if (isDefault == 1) {
        $("#IsDefault").prop("checked", true);
    } else {
        $("#IsDefault").prop("checked", false);
    }
    $("#hdnThemeId").val(e);
}
function DeleteTheme(e) {
    $('#themetr_' + e).remove();
}

function AddEditTemplate() {
    var count = $('#hdnRowCntTemplate').val();
    var flag = true;
    var err = "";
    if ($("#txtTemplateType").val() == '') {
        flag = false;
        err = err != "" ? err + ", Template Type " : "Template Type ";
    }
    if ($("#txtNameTemplate").val() == '') {
        flag = false;
        err = err != "" ? err + ", Name " : " Name ";
    }
    if ($("#txtTitle").val() == '') {
        flag = false;
        err = err != "" ? err + ", Title " : " Title ";
    }
    if (!flag) {
        $("#companyTemplateDetailsError").text("* " + err + "is required.");
    }
    else {
        var state = parseInt($("#hdnTemplateId").val());
        var TemplateType = $("#txtTemplateType").val();
        var Name = $("#txtNameTemplate").val();
        var Title = $("#txtTitle").val();
        var HTMLData = $("#txtHTMLData").val();
        var html = '';
        html += '<tr id="templatetr_' + count + '"><td style="width:20%" id="txtTemplateType_' + count + '">' + TemplateType + '</td>';
        html += '<td style="width:20%" id="txtNameTemplate_' + count + '">' + Name + '</td>';
        html += '<td style="width:30%" id="txtTitleTemplate_' + count + '">' + Title + '</td>';
        html += '<td style="width:10%" class="text-center">';
        html += '<i class="fas fa-pencil-alt" style="cursor:pointer" onclick="EditTemplate(' + count + ')"></i> | <i class="fas fa-trash-alt" style="cursor:pointer" onclick="DeleteTemplate(' + count + ')"></i></td>';
        html += '<td style="width:0%" class="text-center">';
        html += '<input type="hidden" id="txtHTMLData_' + count + '" value="' + HTMLData + '" /></td></tr>';
        if (state > 0) {
            $('#templatetr_' + state).remove();
            $("#tbl_OnBoard_Template_body").prepend(html);
        }
        else {
            $("#tbl_OnBoard_Template_body").prepend(html);
        }
        $('#hdnRowCntTemplate').val(parseInt($('#hdnRowCntTemplate').val()) + 1);
        $("#hdnTemplateId").val('0');
        CancelTemplate();
        TemplateOnFocus();

    }
}
function EditTemplate(e) {
    $("#addTemplate").css("display", "none");
    $("#newTemplate").css("display", "block");
    $("#txtTemplateType").val($("#txtTemplateType_" + e).text());
    $("#txtNameTemplate").val($("#txtName_" + e).text());
    $("#txtTitle").val($("#txtTitle_" + e).text());
    var dt = $("#txtHTMLData_" + e).val();
    $("#txtHTMLData").val($("#txtHTMLData_" + e).val()).trigger('change');
    $("#hdnTemplateId").val(e);
}
function DeleteTemplate(e) {
    $('#templatetr_' + e).remove();
}

function AddEditCompanySetting() {
    var count = $('#hdnRowCntSetting').val();
    var flag = true;
    var err = "";
    if ($("#txtSettingType").val() == '') {
        flag = false;
        err = err != "" ? err + ", Type " : " Type ";
    }
    if ($("#txtDataText").val() == '') {
        flag = false;
        err = err != "" ? err + ", Data Text " : " Data Text ";
    }
    if ($("#txtDataValue").val() == '') {
        flag = false;
        err = err != "" ? err + ", Data Value " : " Data Value ";
    }
    if (!flag) {
        $("#companySettingsDetailsError").text("* " + err + "is required.");
    }
    else {
        var state = parseInt($("#hdnCompanySettingId").val());
        var settingType = $("#txtSettingType").val();
        var dataText = $("#txtDataText").val();
        var dataValue = $("#txtDataValue").val();
        var option1 = $("#txtOption1").val();
        var option2 = $("#txtOption2").val();
        var option3 = $("#txtOption3").val();
        var html = '';
        html += '<tr id="settingtr_' + count + '"><td style="width:10%" id="txtSettingType_' + count + '">' + settingType + '</td>';
        html += '<td style="width:20%" id="txtDataText_' + count + '">' + dataText + '</td>';
        html += '<td style="width:20%" id="txtDataValue_' + count + '">' + dataValue + '</td>';
        html += '<td style="width:10%" id="txtOption1_' + count + '">' + option1 + '</td>';
        html += '<td style="width:10%" id="txtOption2_' + count + '">' + option2 + '</td>';
        html += '<td style="width:10%" id="txtOption3_' + count + '">' + option3 + '</td>';
        html += '<td style="width:10%" class="text-center">';
        html += '<i class="fas fa-pencil-alt" style="cursor:pointer" onclick="EditCompanySetting(' + count + ')"></i> | <i class="fas fa-trash-alt" style="cursor:pointer" onclick="DeleteCompanysetting(' + count + ')"></i></td></tr>';
        if (state > 0) {
            $('#settingtr_' + state).remove();
            $("#tbl_OnBoard_Setting_body").prepend(html);
        }
        else {
            $("#tbl_OnBoard_Setting_body").prepend(html);
        }
        $('#hdnRowCntSetting').val(parseInt($('#hdnRowCntSetting').val()) + 1);
        $("#hdnCompanySettingId").val('0');
        CancelCompanySetting();
        SettingOnFocus();
    }
}
function EditCompanySetting(e) {
    $("#addCompanySetting").css("display", "none");
    $("#newCompanySetting").css("display", "block");
    $("#txtSettingType").val($("#txtSettingType_" + e).text());
    $("#txtDataText").val($("#txtDataText_" + e).text());
    $("#txtDataValue").val($("#txtDataValue_" + e).text());
    $("#txtOption1").val($("#txtOption1_" + e).text());
    $("#txtOption2").val($("#txtOption2_" + e).text());
    $("#txtOption3").val($("#txtOption3_" + e).text());
    $("#hdnCompanySettingId").val(e);
}
function DeleteCompanysetting(e) {
    $('#settingtr_' + e).remove();
}

function SaveOnBoard() {
    var flag = true;
    var stmpInfo = {};
    var branchRows = [];
    var settingRows = [];
    var templateRows = [];
    var themeRows = [];
    var subRows = [];
    var addRows = [];
    var configuration = {};


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
    if (flag) {
        var flag2 = true;
        var err2 = "";
        if (($("#txtSTMPServer").val() == '') && ($("#txtSTMPPort").val() == '') && ($("#txtFromEmailDisplayName").val() == '') && ($("#txtFromEmailId").val() == '') && ($("#txtFromEmailIdPwd").val() == '')) {
            flag2 = true;
        }
        else {
            if ($("#txtSTMPServer").val() == '') {
                flag2 = false;
                err2 = err2 != "" ? err2 + ", SMTP Server " : " SMTP Server ";
            }
            if ($("#txtSTMPPort").val() == '') {
                flag2 = false;
                err2 = err2 != "" ? err2 + ", SMTP Port " : " SMTP Port ";
            }
            if ($("#txtFromEmailDisplayName").val() == '') {
                flag2 = false;
                err2 = err2 != "" ? err2 + ", Display Email Name " : " Display Email Name ";
            }
            if ($("#txtFromEmailId").val() == '') {
                flag2 = false;
                err2 = err2 != "" ? err2 + ", Email " : " Email ";
            }
            if ($("#txtFromEmailIdPwd").val() == '') {
                flag2 = false;
                err2 = err2 != "" ? err2 + ", Password " : " Password ";
            }
            if (flag2) {
                //Mail
                var stmpServer = $("#txtSTMPServer").val();
                var stmpPort = $("#txtSTMPPort").val();
                var fromEmailDisplayName = $("#txtFromEmailDisplayName").val();
                var fromEmailId = $("#txtFromEmailId").val();
                var fromEmailIdPwd = $("#txtFromEmailIdPwd").val();
                var enableSSL = $('#EnableSSL').is(':checked');


                stmpInfo = {
                    CompanyId: 0,
                    MailServerId: 0,
                    SMTPServer: stmpServer,
                    SMTPPort: stmpPort,
                    FromEmailDisplayName: fromEmailDisplayName,
                    FromEmailId: fromEmailId,
                    FromEmailPwd: fromEmailIdPwd,
                    EnableSSL: enableSSL,
                    IsActive: true
                }
            }
        }

        if (flag2) {
            //--company--
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
            var pinRequired = $('#PinRequired').is(':checked');
            var suggCompanyId = $('#txtCompanyId').val();
            var companyInfo = {
                CompanyId: 0,
                Name: name,
                SuggestedCompanyId: suggCompanyId,
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
                IsActive: true
            }
            //Branch

            $('#tbl_OnBoard_Branch_body tr').each(function (e) {
                var id = $(this).index();
                var trID = $(this).attr("id");
                var count = trID.split('_');
                branchRows.push({
                    BranchId: 0,
                    CompanyId: 0,
                    Name: $('#txtNameBranch_' + count[1]).text(),
                    Code: $('#txtCode_' + count[1]).text(),
                    Address1: $('#txtAddress1Branch_' + count[1]).val(),
                    Address2: $('#txtAddress2Branch_' + count[1]).val(),
                    PostalCode: $('#txtPostalCode_' + count[1]).text(),
                    District: $('#txtDistrict_' + count[1]).text(),
                    State: $('#txtState_' + count[1]).text(),
                    Country: $('#txtCountry_' + count[1]).val(),
                    Phone: $('#txtPhone_' + count[1]).text(),
                    Email: $('#txtEmail_' + count[1]).text(),
                    IsActive: true

                });
            });

            //Setting
            $('#tbl_OnBoard_Setting_body tr').each(function (e) {
                var trID = $(this).attr("id");
                var count = trID.split('_');
                settingRows.push({
                    CompanySettingId: 0,
                    CompanyId: 0,
                    SettingType: $('#txtSettingType_' + count[1]).text(),
                    DataText: $('#txtDataText_' + count[1]).text(),
                    DataValue: $('#txtDataValue_' + count[1]).text(),
                    Option1: $('#txtOption1_' + count[1]).text(),
                    Option2: $('#txtOption2_' + count[1]).text(),
                    Option3: $('#txtOption3_' + count[1]).text(),
                    IsActive: true

                });
            });

            //Template
            $('#tbl_OnBoard_Template_body tr').each(function (e) {
                var trID = $(this).attr("id");
                var count = trID.split('_');
                templateRows.push({
                    TemplateId: 0,
                    CompanyId: 0,
                    TemplateType: $('#txtTemplateType_' + count[1]).text(),
                    Name: $('#txtNameTemplate_' + count[1]).text(),
                    Title: $('#txtTitleTemplate_' + count[1]).text(),
                    HTMLData: $('#txtHTMLData_' + count[1]).val(),
                    IsActive: true

                });
            });

            //Theme
            $('#tbl_OnBoard_Theme_body tr').each(function (e) {
                var trID = $(this).attr("id");
                var count = trID.split('_');
                var flag = $("#isDefault_" + count[1]).val();
                var isDefault = false;
                if (flag == 1)
                    isDefault = true;
                themeRows.push({
                    ThemeId: 0,
                    CompanyId: 0,
                    ThemeName: $('#txtThemeName_' + count[1]).text(),
                    ExtThemeName: $('#txtExtThemeName_' + count[1]).text(),
                    ImageRatio: $('#txtImageRatio_' + count[1]).text(),
                    NoOfHomePanels: $('#txtNoOfHomePanels_' + count[1]).text(),
                    Colour: $('#txtColour_' + count[1]).text(),
                    MobileHeight: $('#txtMobileHeight_' + count[1]).text(),
                    DesktopHeight: $('#txtDesktopHeight_' + count[1]).text(),
                    IsDefault: isDefault,
                    IsActive: true

                });
            });

            //Subscription
            $('#tbl_OnBoard_Subscription_body tr').each(function (e) {
                var trID = $(this).attr("id");
                var count = trID.split('_');
                var dt = $('#subscriptions_' + count[1]).is(':checked');
                if ($('#subscriptions_' + count[1]).is(':checked')) {
                    subRows.push({
                        SubscriptionId: $('#subscriptions_' + count[1]).val()

                    });
                }
            });
            //AddOns
            $('#tbl_OnBoard_AddOn_body tr').each(function (e) {
                var trID = $(this).attr("id");
                var count = trID.split('_');
                var dt = $('#addons_' + count[1]).is(':checked');
                if ($('#addons_' + count[1]).is(':checked')) {
                    addRows.push({
                        AddOnId: $('#addons_' + count[1]).val()

                    });
                }
            });

            //Configuration
            var Key = $('#txtConfigurationKey').val();
            var secret = $('#txtConfigurationSecret').val();
            if (subRows.length > 0) {
                var flag3 = true;
                var err3 = "";
                if (Key == '' && secret == '')
                    flag3 = true;
                else {
                    if (Key == '') {
                        flag3 = false;
                        err3 += "Key";
                    }
                    if (secret == '') {
                        flag3 = false;
                        err3 += "Secret";
                    }
                }
                if (flag3) {
                    configuration = {
                        GatewayCompanyMappingId: 0,
                        PaymentGatewayId: $('#ddlPaymentGateway').val(),
                        CompanyId: 0,
                        IsActive: true,
                        CreatedAt: null,
                        CreatedBy: 0,
                        RazoyPaymentDetailId: 0,
                        Key: Key,
                        Secret: secret
                    }
                    var OnBoardCompanyInfo = {
                        CompanyInfo: companyInfo,
                        BranchInfo: branchRows,
                        MailServerInfo: stmpInfo,
                        CompanySettingInfo: settingRows,
                        CompanyTemplate: templateRows,
                        CompanyTheme: themeRows
                    }
                    var OnBoardSubscriptionInfo = {};
                    var OnBoardAddOn = {};
                    var OnBoardConfiguration = {};
                    var OnBoardProcessinfo = {
                        OnBoardCompanyInfo: OnBoardCompanyInfo,
                        OnBoardSubscriptionInfo: OnBoardSubscriptionInfo,
                        OnBoardAddOn: OnBoardAddOn,
                        OnBoardConfiguration: OnBoardConfiguration,
                        Subscriptions: subRows,
                        AddOns: addRows,
                        Configuration: configuration
                    }
                        = console.log(OnBoardProcessinfo);
                    $.ajax({
                        url: baseURL + "OnBoard/SaveOnBoardProcess",
                        type: "POST",
                        dataType: "json",
                        data: OnBoardProcessinfo,
                        success: function (data) {
                            if (data == "login") {
                                window.location.href = baseURL + 'Login/Index'
                            }
                            else if (data.status == true) {
                                MessageShow('', 'Company Saved', 'success');
                            }
                            else {
                                MessageShow('', 'Company not saved', 'error');
                            }
                            HideLoader();
                        },
                        error: function (data) {
                            MessageShow('', 'working in process', 'success');
                            console.log("error");
                            console.log(data);
                            HideLoader();
                        }
                    });
                }
                else {
                    $("#OnBoardConfigurationError").text("* " + err3 + " is required.");
                    focusstep4();
                    focusstep4();
                }

            }
            else {
                $("#OnBoardSubscriptionError").text("* one subscription is required.");
                focusstep2();
            }
        }
        else {
            $("#companymailDetailsError").text("* " + err2 + "is required.");
            FocusMail();
            focusstep1();
        }
    }
    else {
        $("#companyDetailsError").text("* " + err + "is Not Correct");
        FocusCompany();
        focusstep1();
    }

}
function SearchSubscription() {
    var search = $("#srchSub").val();
    $('#tbl_OnBoard_Subscription_body tr').hide();
    var len = $('#tbl_OnBoard_Subscription_body tr:not(.notfound) td:nth-child(2):contains("' + search + '")').length;

    if (len > 0) {
        $('#tbl_OnBoard_Subscription_body tr td:contains("' + search + '")').each(function () {
            $(this).closest('tr').show();
        });
    }
    $.expr[":"].contains = $.expr.createPseudo(function (arg) {
        return function (elem) {
            return $(elem).text().toUpperCase().indexOf(arg.toUpperCase()) >= 0;
        };
    });
}
function SearchAddOns() {
    var search = $("#srchadd").val();
    $('#tbl_OnBoard_AddOn_body tr').hide();
    var len = $('#tbl_OnBoard_AddOn_body tr:not(.notfound) td:nth-child(2):contains("' + search + '")').length;

    if (len > 0) {
        $('#tbl_OnBoard_AddOn_body tr td:contains("' + search + '")').each(function () {
            $(this).closest('tr').show();
        });
    }
    $.expr[":"].contains = $.expr.createPseudo(function (arg) {
        return function (elem) {
            return $(elem).text().toUpperCase().indexOf(arg.toUpperCase()) >= 0;
        };
    });
}

$(document).ready(function () {
    var navListItems = $('div.setup-panel div a'),
        allWells = $('.setup-content'),
        allNextBtn = $('.nextBtn');
    allPrevBtn = $('.prevBtn');

    allWells.hide();

    navListItems.click(function (e) {
        e.preventDefault();
        var $target = $($(this).attr('href')),
            $item = $(this);

        if (!$item.hasClass('disabled')) {
            navListItems.removeClass('btn-successa').addClass('btn-default');
            $item.addClass('btn-successa');
            allWells.hide();
            $target.show();
            $target.find('input:eq(0)').focus();
        }
    });

    allNextBtn.click(function () {
        var goToNext = true;
        var curStep = $(this).closest(".setup-content"),
            curStepBtn = curStep.attr("id"),
            nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().next().children("a"),
            curInputs = curStep.find("input[type='text'],input[type='url']"),
            isValid = true;

        $(".form-group").removeClass("has-error");
        for (var i = 0; i < curInputs.length; i++) {
            if (!curInputs[i].validity.valid) {
                isValid = false;
                $(curInputs[i]).closest(".form-group").addClass("has-error");
            }
        }
        if (curStep.attr("id") == 'step-1') {
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
            if (flag) {
                var flag2 = true;
                var err2 = "";
                if (($("#txtSTMPServer").val() == '') && ($("#txtSTMPPort").val() == '') && ($("#txtFromEmailDisplayName").val() == '') && ($("#txtFromEmailId").val() == '') && ($("#txtFromEmailIdPwd").val() == '')) {
                    flag2 = true;
                }
                else {
                    if ($("#txtSTMPServer").val() == '') {
                        flag2 = false;
                        err2 = err2 != "" ? err2 + ", SMTP Server " : " SMTP Server ";
                    }
                    if ($("#txtSTMPPort").val() == '') {
                        flag2 = false;
                        err2 = err2 != "" ? err2 + ", SMTP Port " : " SMTP Port ";
                    }
                    if ($("#txtFromEmailDisplayName").val() == '') {
                        flag2 = false;
                        err2 = err2 != "" ? err2 + ", Display Email Name " : " Display Email Name ";
                    }
                    if ($("#txtFromEmailId").val() == '') {
                        flag2 = false;
                        err2 = err2 != "" ? err2 + ", Email " : " Email ";
                    }
                    if ($("#txtFromEmailIdPwd").val() == '') {
                        flag2 = false;
                        err2 = err2 != "" ? err2 + ", Password " : " Password ";
                    }
                }
                if (flag2) {
                    goToNext = true;
                }
                else {
                    goToNext = false;
                    $("#companymailDetailsError").text("* " + err2 + "is required.");
                    FocusMail();
                    focusstep1();
                }


            }
            else {
                goToNext = false;
                $("#companyDetailsError").text("* " + err + "is Not Correct");
                FocusCompany();
                focusstep1();
            }
        }
        if (curStep.attr("id") == 'step-2') {
            var subRows = [];
            $('#tbl_OnBoard_Subscription_body tr').each(function (e) {
                var trID = $(this).attr("id");
                var count = trID.split('_');
                var dt = $('#subscriptions_' + count[1]).is(':checked');
                if ($('#subscriptions_' + count[1]).is(':checked')) {
                    subRows.push({
                        SubscriptionId: $('#subscriptions_' + count[1]).val()

                    });
                }
            });
            if (subRows.length > 0) {
                goToNext = true;

            }
            else {
                goToNext = false;
                $("#OnBoardSubscriptionError").text("* one subscription is required.");
                focusstep2();
            }
        }
        if (curStep.attr("id") == 'step-3') {
            var addRows = [];
            $('#tbl_OnBoard_AddOn_body tr').each(function (e) {
                var trID = $(this).attr("id");
                var count = trID.split('_');
                var dt = $('#addons_' + count[1]).is(':checked');
                if ($('#addons_' + count[1]).is(':checked')) {
                    addRows.push({
                        AddOnId: $('#addons_' + count[1]).val(),
                        AddOnText: $('#partNo_' + count[1]).text()

                    });
                }
            });
            if (addRows.length > 0) {
                for (var i = 0; i < addRows.length; i++) {
                    if (addRows[i].AddOnText == 'ADDON-1003') {
                        $('#hdnConfguration').val(1);
                        $('#configurations').show();
                    }
                    else {
                        $('#hdnConfguration').val(0);
                        $('#configurations').hide();
                    }
                }
            }
            else {
                $('#hdnConfguration').val(0);
                $('#configurations').hide();
            }
        }

        if (isValid && goToNext) nextStepWizard.removeAttr('disabled').trigger('click');
    });
    allPrevBtn.click(function () {

        var curStep = $(this).closest(".setup-content"),
            curStepBtn = curStep.attr("id"),
            nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().prev().children("a"),
            curInputs = curStep.find("input[type='text'],input[type='url']"),
            isValid = true;

        $(".form-group").removeClass("has-error");
        for (var i = 0; i < curInputs.length; i++) {
            if (!curInputs[i].validity.valid) {
                isValid = false;
                $(curInputs[i]).closest(".form-group").addClass("has-error");
            }
        }

        if (isValid) nextStepWizard.removeAttr('disabled').trigger('click');
    });
    $('div.setup-panel div a.btn-successa').trigger('click');
});