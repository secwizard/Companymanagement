
function GetCompanyDetails() {
    debugger;
    var dt = baseURL + "Company/GetCompanydetails";
    $.ajax({
        url: baseURL + "Company/GetCompanydetails",
        type: "GET",
        dataType: "html",
        data: "",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger;
            $("#Company").html(data);
            //$("#summerydata").html(data);
            HideLoader();
        },
        error: function (data) {
            debugger;
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
            //$("#summerydata").html(data);
            HideLoader();
        },
        error: function (data) {
            console.log("error");
            console.log(data);
            HideLoader();
        }
    });
}
function SaveCompany() {
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
    var buisnessType = $("#txtBusinessType").val();
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
        Website : website,
        PINRequired : pinRequired,
        IsActive : isActive
    }
    debugger;
    $.ajax({
        url: baseURL + "Company/AddEditCompany",
        type: "GET",
        dataType: "json",
        data: companyInfo,
        contentType: "application/json; charset=utf-8",
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
