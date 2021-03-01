function GetCompanyDetails() {
    $.ajax({
        url: baseURL + "OnBoard/GetCompanydetails",
        type: "GET",
        dataType: "html",
        data: "",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#OnBoardCompany").html(data);
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
function GetBranchDetails() {
    $.ajax({
        url: baseURL + "OnBoard/GetBranchdetails",
        type: "GET",
        dataType: "html",
        data: "",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#OnBoardBranch").html(data);
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
