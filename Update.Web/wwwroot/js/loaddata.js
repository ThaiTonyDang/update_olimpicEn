
let cityName = "";
$(document).ready(function () {
    let schoolType = $("#bangthi").val()
    let areaId = 0;
    let cityId = 0;
    let districtId = 0;
    let user = GetUserObject();
    SaveUserToStorage(user);
    let city_id_default = user.CityId; 
    let district_id_default = user.DistrictId;
    let school_id_default = user.SchoolId;
    let currentArea = $("#area").val();
    GetCityById(city_id_default);
    GetCitiesByAreaId(currentArea);
    GetSchoolById(school_id_default);
    if (user.Class == "SV") {
        $("#district").prop('disabled', true);
    }
    else {
        GetDistrictById(district_id_default);
    }

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
        let valueClass = $("#bangthi").val();
        if (valueClass == "SV") {
            $("#district").prop('disabled', true);
            var defaultValue = '';
            $("#district").html(defaultValue);
        }
        else {
            $("#district").prop('disabled', false);
            cityId = $('#city').val();
            GetDistrictsByCityId(cityId);
        }
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

            var s = '<option value="-1">' + cityName + '</option>';
            if (!cityName || cityName == "") {
                s = '<option value="-1">--- Chọn Tỉnh/Thành Phố/Khu Vực ---</option>';
            }

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

function GetCityById(cityId) {
    $.ajax({
        type: "GET",
        url: "/city/" + cityId,
        data: "{}",
        success: function (response) {
            cityName = response.Name;
            var s = '<option value="' + response.Id + '">' + cityName + '</option>';
            $("#city").html(s);
        },
        error: function (response, status, error) {
            var defaultValue = '<option value="-1">--- Chọn Tỉnh/Thành Phố/Khu Vực ---</option>';
            $("#city").html(defaultValue);
        }
    })
}

function GetDistrictById(districtId) {
    $.ajax({
        type: "GET",
        url: "/district/" + districtId,
        data: "{}",
        success: function (response) {
            var s = '<option value="' + response.Id + '">' + response.Name + '</option>';
            $("#district").html(s);
        },
        error: function (response, status, error) {
            var defaultValue = '<option value="-1">--- Chọn Quận/Huyện ---</option>';
            $("#district").html(defaultValue);
        }
    })
}

function GetSchoolById(schoolId) {
    $.ajax({
        type: "GET",
        url: "/school/" + schoolId,
        data: "{}",
        success: function (response) {
            var s = '<option value="' + response.Id + '">' + response.Name + '</option>';
            $("#school").html(s);
        },
        error: function (response, status, error) {
            var defaultValue = '<option value="-1">--- Chọn Trường ---</option>';
            $("#school").html(defaultValue);
        }
    })
}

function GetUserObject() {
    var user = {
        Id : $(".userId").val(),
        FullName : $(".fullname").val(),
        Email : $(".email").val(),
        UserName : $(".userName").val(),
        Class : $(".class").val(),
        AreaId : $(".areaId").val(),
        CityId : $(".cityId").val(),
        DistrictId : $(".districtId").val(),
        SchoolId : $(".schoolId").val(),
    }

    return user;
}

function SaveUserToStorage(user) {
    localStorage.setItem("userData", JSON.stringify(user));
}