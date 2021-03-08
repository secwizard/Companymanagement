

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
//function GetRequiredDetails() {
//    debugger;
//    $.ajax({
//        url: baseURL + "OnBoard/GetRequiredDetails",
//        type: "POST",
//        dataType: "json",
//        data: {},
//        contentType: "application/json; charset=utf-8",
//        success: function (data) {
//            debugger;
//            var d = data.data;
//            var selected = $("#ddlBusinessType").val();
//            var html = '';
//            for (var i = 0; i < data.data.length; i++) {
//                html += '<option value="' + data.data[i].lookUpValue + '">' + data.data[i].lookUpText + '</option>';
//            }
//            $("#ddlBusinessType").html(html);
//            GetSuggestedCompanyId();
//            HideLoader();
//        },
//        error: function (data) {
//            console.log("error");
//            console.log(data);
//            HideLoader();
//        }
//    });
//}