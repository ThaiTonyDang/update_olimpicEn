

$(document).ready(function () {
    let defaultValue = 0;
    let schoolType = $("#bangthi").val()
    let areaId = 0;
    let cityId = 0;
    let districtId = 0;
    GetCitiesByAreaId(defaultValue);
    GetDistrictsByCityId(defaultValue);
    GetSchoolsByCityIdSchoolType(defaultValue, defaultValue, schoolType);
    $("#area").on('change', function () {
        areaId = $(this).val();
        GetCitiesByAreaId(areaId);
    });

    $("#city").on('change', function () {
        cityId = $(this).val();
        GetDistrictsByCityId(cityId);
       
    });

    $("#district").on('change', function () {
        districtId = $(this).val();
        GetSchoolsByCityIdSchoolType(cityId, districtId, schoolType);
    });
    $("#bangthi").on('change', function () {
        schoolType = $("#bangthi").val()
        GetSchoolsByCityIdSchoolType(cityId, districtId, schoolType);
    });
});

function GetCitiesByAreaId(areaId) {
    $.ajax({
        type: "GET",
        url: "/update/cities/" + areaId,
        data: "{}",
        success: function (response) {
            var s = '<option value="-1">---Chọn Tỉnh/Thành Phố ---</option>';
            for (var i = 0; i < response.length; i++) {
                s += '<option value="' + response[i].Id + '">' + response[i].Name + '</option>';
            }
            $("#city").html(s);
        },
        error: function (response, status, error) {
            var defaultValue = '<option value="-1">---Chọn Tỉnh/Thành Phố ---</option>';
            $("#city").html(defaultValue);
        }
    });
}

function GetDistrictsByCityId(cityId) {
    $.ajax({
        type: "GET",
        url: "/update/districts/" + cityId,
        data: "{}",
        success: function (response) {
            var s = '<option value="-1">---Chọn Quận/Huyện---</option>';
            for (var i = 0; i < response.length; i++) {
                s += '<option value="' + response[i].Id + '">' + response[i].Name + '</option>';
            }
            $("#district").html(s);
        },
        error: function (response, status, error) {
            var defaultValue = '<option value="-1">---Quận/Huyện ---</option>';
            $("#district").html(defaultValue);
        }
    });
}

function GetSchoolsByCityIdSchoolType(cityId, districtId, schoolType) {
    $.ajax({
        type: "GET",
        url: "schools" + "?schoolType=" + schoolType + "&cityId=" + cityId + "&districtId=" + districtId  ,
        data: "{}",
        success: function (response) {
            var s = '<option value="-1">--- Chọn Trường ---</option>';
            for (var i = 0; i < response.length; i++) {
                s += '<option value="' + response[i].Id + '">' + response[i].Name + '</option>';
            }
            $("#school").html(s);
        },
        error: function (response, status, error) {
            var defaultValue = '<option value="-1">--- Chọn Trường ---</option>';
            $("#school").html(defaultValue);
        }
    });
}