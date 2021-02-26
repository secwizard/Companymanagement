var ImageLibraryList = Array();
$(document).ready(function () {
    try {
        GetBrandList();
    } catch (e) {
        console.log("catch", e);
        ConLog(e);
        HideLoader();
    }
});
function GetBrandList() {
    try {
        ShowLoader();
        $.ajax({
            url: baseURL + "Allocate/GetBrandList",
            type: "GET",
            dataType: "json",
            data: "",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != "NO") {
                    console.log(data);
                    var html = "";
                    html += "<option value='0'>- Select -</option>";
                    for (var i = 0; i < data.length; i++) {
                        html += "<option value='" + data[i].brandId + "' >" + data[i].brandName + "</option>";
                    }
                    $("#ddlBrand").html(html);
                }
                HideLoader();
            },
            error: function (data) {
                console.log("error");
                console.log(data);
                HideLoader();
            }
        });

    } catch (e) {
        console.log("catch", e);
        ConLog(e);
        HideLoader();
    }
}
function GetCategoryList() {
    try {
        ShowLoader();
        var brand = $("#ddlBrand").val();
        $("#ddlCategory").html("<option value='0'>- Select -</option>");
        $("#ddlSubCategory").html("<option value='0'>- Select -</option>");
        $("#ddlItem").html("<option value='0'>- Select -</option>");
        ClearData();
        if (brand === 0) {
            HideLoader();
            return false;
        }
        $.ajax({
            url: baseURL + "Allocate/GetCategoryList?BrandId=" + brand,
            type: "GET",
            dataType: "json",
            data: "",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != "NO") {
                    console.log(data);
                    var html = "";
                    html += "<option value='0'>- Select -</option>";
                    for (var i = 0; i < data.length; i++) {
                        html += "<option value='" + data[i].categoryId + "' >" + data[i].categoryName + "</option>";
                    }
                    $("#ddlCategory").html(html);
                }
                HideLoader();
            },
            error: function (data) {
                console.log("error");
                console.log(data);
                HideLoader();
            }
        });

    } catch (e) {
        console.log("catch", e);
        ConLog(e);
        HideLoader();
    }
}
function GetSubCategoryList() {
    try {
        ShowLoader();
        var brand = $("#ddlBrand").val();
        var category = $("#ddlCategory").val();
        $("#ddlSubCategory").html("<option value='0'>- Select -</option>");
        $("#ddlItem").html("<option value='0'>- Select -</option>");
        ClearData();
        if (category == 0) {
            HideLoader();
            return false;
        }
        $.ajax({
            url: baseURL + "Allocate/GetSubCategoryList?BrandId=" + brand + "&CategoryId=" + category,
            type: "GET",
            dataType: "json",
            data: "",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != "NO") {
                    console.log(data);
                    var html = "";
                    html += "<option value='0'>- Select -</option>";
                    for (var i = 0; i < data.length; i++) {
                        html += "<option value='" + data[i].subCategoryId + "' >" + data[i].subCategoryName + "</option>";
                    }
                    $("#ddlSubCategory").html(html);
                }
                GetItemList();
                HideLoader();
            },
            error: function (data) {
                console.log("error");
                console.log(data);
                HideLoader();
            }
        });

    } catch (e) {
        console.log("catch", e);
        ConLog(e);
        HideLoader();
    }
}
function GetItemList() {
    try {
        ShowLoader();
        var brand = $("#ddlBrand").val();
        var category = $("#ddlCategory").val();
        var subCategory = $("#ddlSubCategory").val();
        $("#ddlItem").html("<option value='0'>- Select -</option>");
        ClearData();
        //if (subCategory == 0) {
        //    HideLoader();
        //    return false;
        //}
        $.ajax({
            url: baseURL + "Allocate/GetItemList?BrandId=" + brand + "&CategoryId=" + category + "&SubCategoryId=" + subCategory,
            type: "GET",
            dataType: "json",
            data: "",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != "NO") {
                    console.log(data);
                    var html = "";
                    html += "<option value='0'>- Select -</option>";
                    for (var i = 0; i < data.length; i++) {
                        html += "<option value='" + data[i].itemId + "' >" + data[i].itemName + "</option>";
                    }
                    $("#ddlItem").html(html);
                }
                HideLoader();
            },
            error: function (data) {
                console.log("error");
                console.log(data);
                HideLoader();
            }
        });

    } catch (e) {
        console.log("catch", e);
        ConLog(e);
        HideLoader();
    }
}
function GetItemWiseList() {
    try {
        var ItemId = $("#ddlItem").val();
        var ItemName = $("#ddlItem :selected").text();
        if (ItemId == 0) {
            HideLoader();
            ClearData();
            return false;
        }
        $("#txtImageName").val('');
        GetImageLibrary();
        $.ajax({
            url: baseURL + "Allocate/GetItemWiseList?ItemId=" + ItemId,
            type: "GET",
            dataType: "json",
            data: "",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != "NO") {
                    console.log(data);
                    CreateImageList(data);
                    $("#spnItemName").text(ItemName);
                }
                HideLoader();
            },
            error: function (data) {
                console.log("error");
                console.log(data);
                HideLoader();
            }
        });

        $("#divtablepannel").css("display", "block");

    } catch (e) {
        console.log("catch", e);
        ConLog(e);
        HideLoader();
    }
}
function CreateImageList(data) {
    try {
        var html = '';
        if (data != null && data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                var imagepath = data[i].filePath + "/" + data[i].name;
                var IsPrimary = '';
                if (data[i].isPrimary == true) {
                    IsPrimary = 'checked';
                }
                html += '<tr id="row_' + (i + 1) + '">';
                html += '<td>';
                html += '<div class="search-img-small-imgdiv" data-toggle="modal" data-target="#lightboxmodal">';
                html += '<img onclick="ViewFullImage(\'' + imagepath + '\')" src="' + imagepath + '" />';
                html += '</div>';
                html += '</td>';
                html += '<td style="word-break: break-all"><span>' + data[i].name + '</span><input type="hidden" name="ImageId" value="' + data[i].imageId + '"></td>';

                html += '<td>';
                html += '<label class="radio-inline cus-radio">';
                html += '<input type="radio" name="optradio" ' + IsPrimary + '>';
                html += '<span class="checkmark-radio"></span>';
                html += '</label>';
                html += '</td>';

                html += '<td><input type="text" class="textboxclass h23input-table" style="width: 50px;" name="sortOrder" value="' + data[i].sortOrder + '"></td>';

                html += '<td>';
                html += '<label class="cus-chk-containerall ml-15">';
                html += '<input type="checkbox" name="record">';
                html += '<span class="checkmarkall"></span>';
                html += '</label>';
                html += '</td>';
                html += '</tr>';
            }
            $("#btlBody").html(html);
        }
        else {
            $("#btlBody").html('');
        }

    } catch (e) {
        console.log("catch", e);
        ConLog(e);
        HideLoader();
    }
}
function GetImageLibrary() {
    try {
        $.ajax({
            url: baseURL + "Allocate/GetImageLibrary",
            type: "GET",
            dataType: "json",
            data: "",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != "NO") {
                    console.log(data);
                    CreateLibraryHtml(data);
                    ImageLibraryList = data;
                }
                HideLoader();
            },
            error: function (data) {
                console.log("error");
                console.log(data);
                HideLoader();
            }
        });

    } catch (e) {
        console.log("catch", e);
        ConLog(e);
        HideLoader();
    }
}
function CreateLibraryHtml(data) {
    try {
        var html = '';
        if (data != null && data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                var imagepath = data[i].filePath + "/" + data[i].name;
                html += '<div class="col-md-4 mt-10" id="divLibrary_' + (i + 1) + '">';
                html += '<div class="bordccc">';
                html += '<img src="' + imagepath + '" class="img-responsive" />';
                html += '<p class="field-label-text imgname-p">' + data[i].name + '</p>';
                html += '<div class="text-center mt-10 mb-10">';
                html += '<button class="secondary-type-button" onclick="AddItemList(' + data[i].imageId + ',\'' + imagepath + '\',\'' + data[i].name + '\',' + (i + 1) + ')" > Add</button >';
                html += '</div > ';
                html += '</div>';
                html += '</div>';
            }
            $("#divImageLibrary").html(html);
        }
        else {
            $("#divImageLibrary").html(html);
        }

    } catch (e) {
        console.log("catch", e);
        ConLog(e);
        HideLoader();
    }
}
function AddItemList(imageId, imagepath, name, index) {
    try {
        var sortOrder = 0;
        var SortOrderlist = [];
        if ($("#btlBody tr").length > 0) {
            $("#btlBody tr").each(function () {
                var SortOrder = $(this).find('input[name="sortOrder"]').val();
                if (SortOrder == "") {
                    SortOrder = 0;
                }
                SortOrderlist.push(SortOrder);
            });
            sortOrder = Math.max.apply(Math, SortOrderlist);
        }




        var html = '';
        html += '<tr id="NewRow">';
        html += '<td>';
        html += '<div class="search-img-small-imgdiv" data-toggle="modal" data-target="#lightboxmodal">';
        html += '<img onclick="ViewFullImage(\'' + imagepath + '\')" src="' + imagepath + '" />';
        html += '</div>';
        html += '</td>';
        html += '<td style="word-break: break-all"><span>' + name + '</span><input type="hidden" name="ImageId" value="' + imageId + '"></td>';

        html += '<td>';
        html += '<label class="radio-inline cus-radio">';
        html += '<input type="radio" name="optradio">';
        html += '<span class="checkmark-radio"></span>';
        html += '</label>';
        html += '</td>';

        html += '<td><input type="text" class="textboxclass h23input-table" style="width: 50px;" name="sortOrder" value="' + (sortOrder + 1) + '"></td>';

        html += '<td>';
        html += '<label class="cus-chk-containerall ml-15">';
        html += '<input type="checkbox" name="record">';
        html += '<span class="checkmarkall"></span>';
        html += '</label>';
        html += '</td>';
        html += '</tr>';
        if ($('#btlBody > tr').length > 0) {
            $('#btlBody > tr').eq(0).before(html);
        } else {
            $('#btlBody').html(html);
        }
        $('#divLibrary_' + index).css("display", "none");

        console.log(ImageLibraryList);
        for (var i = 0; i < ImageLibraryList.length; i++) {
            if (ImageLibraryList[i].imageId == imageId) {
                ImageLibraryList.splice(i, 1);
            }
        }
        console.log(ImageLibraryList);

    } catch (e) {
        console.log("catch", e);
        ConLog(e);
        HideLoader();
    }
}
function ClearData() {
    try {
        $("#divImageLibrary").html('');
        $("#btlBody").html('');
        $("#divtablepannel").css("display", "none");
        $("#txtImageName").val('');

    } catch (e) {
        console.log("catch", e);
        ConLog(e);
        HideLoader();
    }
}
function SaveItemImage() {
    try {
        ShowLoader();
        var deleteList = [];
        var editList = [];
        var NewList = [];
        $("#btlBody tr").each(function () {
            var Id = this.id;
            var ImageId = $(this).find('input[name="ImageId"]').val();
            var IsPrimary = $(this).find('input[name="optradio"]').is(":checked");
            var IsDelete = $(this).find('input[name="record"]').is(":checked");
            var SortOrder = $(this).find('input[name="sortOrder"]').val();

            if (IsDelete == true && !Id.includes("NewRow")) { // Delete
                deleteList.push({ ImageId: ImageId });
            }
            else if (!Id.includes("NewRow")) { // Edit
                editList.push({ ImageId: ImageId, IsPrimary: IsPrimary, SortOrder: SortOrder });
            }
            else if (Id.includes("NewRow") && IsDelete == false) { // New Add
                NewList.push({ ImageId: ImageId, IsPrimary: IsPrimary, SortOrder: SortOrder });
            }
        });
        console.log(deleteList);
        console.log(editList);
        console.log(NewList);
        var data = {
            ItemId: $("#ddlItem").val(),
            DeleteList: deleteList,
            EditList: editList,
            NewList: NewList
        }
        $.ajax({
            url: baseURL + "Allocate/SaveItemImage",
            type: "POST",
            dataType: "json",
            data: data,
            success: function (data) {
                if (data != "NO" && data != null) {
                    console.log(data);
                    MessageShow('GetItemWiseList()', '' + data + ' Successfully!', 'success');
                } else {
                    MessageShow('', 'Error', 'error');
                }
                HideLoader();
            },
            error: function (data) {
                console.log("error");
                console.log(data);
                HideLoader();
            }
        });

    } catch (e) {
        console.log("catch", e);
        ConLog(e);
        HideLoader();
    }
}
function SearchImage() {
    try {
        var data = Array();
        var imageName = $("#txtImageName").val();
        if (imageName !== "") {
            for (var i = 0; i < ImageLibraryList.length; i++) {
                var name = ImageLibraryList[i].name;
                name = name.toLowerCase();
                if (name.indexOf(imageName.toLowerCase()) != -1) {
                    data.push(ImageLibraryList[i]);
                }
            }
        } else {
            data = ImageLibraryList;
        }
        CreateLibraryHtml(data);

    } catch (e) {
        console.log("catch", e);
        ConLog(e);
        HideLoader();
    }
}
function ViewFullImage(path) {
    try {
        $("#fullImage").attr("src", '');
        $("#fullImage").attr("src", path);

    } catch (e) {
        console.log("catch", e);
        ConLog(e);
        HideLoader();
    }
}


