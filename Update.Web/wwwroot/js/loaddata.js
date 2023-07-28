
$(document).ready(function () {
    function CheckTextError() {
        var textMes = $('.error').text();
        if (textMes) {
            $('.confirm-update').prop('disabled', true);
        }
        else {
            $('.confirm-update').prop('disabled', false);
        }

    }

    let dataUser = JSON.parse(localStorage.getItem("userData")) // lấy data user
    // Bắt đầu fill dữ liệu vào form detail
    let area_id_default = dataUser.Area;
    let city_id_default = dataUser.City; 
    let district_id_default = dataUser.District;
    let school_id_default = dataUser.School;
    let school_type_default = dataUser.Class;
    GetCityById(city_id_default); // hiển thị tên thành phố của users*/
    GetCitiesByAreaId(area_id_default); // đồng thời hiển thị list city theo area của user

    if (dataUser.Class == "SV") {
        // nếu là sinh viên thì ẩn Huyện
        $('.district_block').css('display', 'none');
    }
    else {
       GetDistrictById(district_id_default); // Hiển thị Huyện mặc định nếu là HS*/
        GetDistrictsByCityId(dataUser.City); //  Đồng thời hiện danh sách District với city hiện tại
    }

    GetSchoolById(school_id_default);
    GetSchoolsByCityIdSchoolType(city_id_default, district_id_default, school_type_default);
    // Sau khi lựa chọn thay đổi lại khu vực thành phố
    let currentSchoolType = ''; // giá trị mặc định các trư
    let currentAreaId = -1;
    let currentCityId = -1;
    let currentDistrictId = -1;
    let currentSchoolId = -1;

    $("#bangthi").on('change', function () {
        GetCurrentId();

         // khi bảng thi thay đổi thì District thay đổi (hiển thị hoặc không)
         // List
        // List School có sự thay đổi nên Id của nó reset vể -1  
        if (currentSchoolType != "SV") {
            $('.district_block').show();
        }
        else {
            $('.district_block').css('display', 'none');
            $('.district-error-message').html('');
        }

        if (currentSchoolType == dataUser.Class) {
            $('.district-error-message').html('');
            $('.school-error-message').html('');
            //GetDistrictsByCityId(dataUser.City);
            //GetSchoolsByCityIdSchoolType(dataUser.City, dataUser.District, dataUser.Class);
        }
        else {
            if (currentSchoolType == "SV") {
                $('.district-error-message').html('');
            }
            else {
                GetDistrictsByCityId(currentCityId);
                ValidateDistrict(-1);
            }

            GetSchoolsByCityIdSchoolType(currentCityId, currentDistrictId, currentSchoolType);
            ValidateSchool(-1);
        }
        // Get list School theo Id hiện tại (current)
        CheckTextError();
    });

    $("#area").on('change', function () { 
        // thay đổi area a thì City thay đổi , city thay đổi => List School thay đổi
        currentAreaId = $('#area').val(); // set Id hien tai cua area
        currentSchoolType = $('#bangthi').val(); // set Id school hiện tại
        currentCityId = $('#city').val(); 
        if (currentAreaId != dataUser.Area) {
            // thay đổi area thì district về mặc đinh

            var defaultValue = '';
            
            GetCitiesByAreaId(currentAreaId); // Get list city theo id area hiện tại
            ValidateCity(-1);

            GetDistrictsByCityId(currentCityId);
            ValidateDistrict(-1); 

            GetSchoolsByCityIdSchoolType(currentCityId, currentDistrictId, currentSchoolId);
            ValidateSchool(-1);
        }
        else {
            // nếu Area không đổi
            $('.city-error-message').html('');
            $('.school-error-message').html('');
            $('.district-error-message').html('');
            GetCitiesByAreaId(dataUser.Area); // Get list city theo id area hiện tại
            GetDistrictsByCityId(dataUser.City);
            GetSchoolsByCityIdSchoolType(dataUser.City, dataUser.District, currentSchoolType);
        }
        CheckTextError();

    });

    $("#city").on('change', function () {
        GetCurrentId();
        ValidateCity(currentCityId);
        currentCityId = $('#city').val();
        currentCityId = $('#city').val();
/*        let chooseCityIdNull = $('#city').val();*/
        if (currentCityId != dataUser.City) {
            GetDistrictsByCityId(currentCityId);
            ValidateDistrict(-1);

            GetSchoolsByCityIdSchoolType(currentCityId, currentDistrictId, currentSchoolType);
            ValidateSchool(-1)
        }
        else {
            $('.district-error-message').html('');
            $('.school-error-message').html('');
            GetDistrictsByCityId(dataUser.City);
            GetSchoolsByCityIdSchoolType(dataUser.City, dataUser.District, dataUser.Class);
        }

        CheckTextError();
    });


    $("#district").on('change', function () {
        currentDistrictId = $('#district').val();
        currentSchoolType = $('#bangtin').val();
        ValidateDistrict(currentDistrictId);
        if (currentDistrictId != dataUser.District) {
            if (currentCityId == -1) {
                currentCityId = dataUser.City;
            }
            GetSchoolsByCityIdSchoolType(currentCityId, currentDistrictId, currentSchoolType);
            ValidateSchool(-1)
        }
            $('.school-error-message').html('');
            GetSchoolsByCityIdSchoolType(dataUser.C
        else {ity, dataUser.District, dataUser.Class);
        }
    });

    $("#school").on('change', function () {
        currentSchoolId = $('#school').val();
        ValidateSchool(currentSchoolId);
        CheckTextError();
    });

    

    function GetCitiesByAreaId(areaId) {
        $.ajax({
            type: "GET",
            url: "/update/cities/" + areaId,
            data: "{}",
            success: function (response) {
                var dataCity = localStorage.getItem("City");
                var html_element = '';
                if (dataCity) {
                    html_element = '<option value="' + JSON.parse(dataCity).Id + '">' + JSON.parse(dataCity).Name + '</option>';
                }
                let currentAreaId = $("#area").val();
                if (currentAreaId != JSON.parse(dataCity).AreaId || response.length == 0) {
                    html_element = '<option value="-1">---Chọn Tỉnh/Thành Phố/Khu Vực---</option>';
                }
                $("#city").html(html_element);

                $.each(response.sort(function (a, b)
                {
                    return a.Name.localeCompare(b.Name);
                }), function (index, city) {
                    $("#city").append($('<option></option>').val(city.Id).text(city.Name));
                });
            },
            error: function (response, status, error) {
                var defaultValue = '<option value="-1">---Chọn Tỉnh/Thành Phố ---</option>';
                $("#city").htmls(defaultValue);
            }
        });
    }

    function GetDistrictsByCityId(cityId) {
        $.ajax({
            type: "GET",
            url: "/update/districts/" + cityId,
            data: "{}",
            success: function (response) {
                var dataDistrict = localStorage.getItem("District");
                var html_element = '';
                if (dataUser.Class == "HS") {
                    if (JSON.parse(dataDistrict).Id != 0) {
                        html_element = '<option value="' + JSON.parse(dataDistrict).Id + '">' + JSON.parse(dataDistrict).Name + '</option>';
                        if (currentAreaId == -1) {
                            currentAreaId = dataUser.Area;
                        }
                        if (cityId != dataUser.City || currentAreaId != dataUser.Area)
                        {
                            html_element = '<option value="-1">---Chọn Quận/Huyện---</option>';
                        }
                    }
                }
               
                else {
                    html_element = '<option value="-1"></option>';
                }
                $("#district").html(html_element);
                $.each(response.sort(function (a, b) {
                    return a.Name.localeCompare(b.Name);
                }), function (index, district) {
                    $("#district").append($('<option></option>').val(district.Id).text(district.Name));
                });
            },
            error: function (response, status, error) {
                var defaultValue = '<option value="-1">---Quận/Huyện ---</option>';
                $("#district").append(defaultValue);
            }
        });
    }
    function GetSchoolsByCityIdSchoolType(cityId, districtId, schoolType) {

        $.ajax({
            type: "GET",
            url: "schools" + "?schoolType=" + schoolType + "&cityId=" + cityId + "&districtId=" + districtId,
            data: "{}",
            success: function (response) {
                var dataSchool = localStorage.getItem("School");

                var html_element = '';
                if (JSON.parse(dataSchool)) {
                    html_element = '<option value="' + JSON.parse(dataSchool).Id + '">' + JSON.parse(dataSchool).Name + '</option>';
                }

                if (schoolType != dataUser.Class ||
                    cityId != dataUser.City || districtId != dataUser.District) {
                    html_element = '<option value="-1">--- Chọn Trường ---</option>';
                }
                $("#school").html(html_element);
                $.each(response.sort(function (a, b) {
                    return a.Name.localeCompare(b.Name);
                }), function (index, school) {
                    $("#school").append($('<option></option>').val(school.Id).text(school.Name));
                });
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
                var html_element = '';
                localStorage.setItem("City", JSON.stringify(response));
                html_element = '<option value="' + response.Id + '">' + response.Name + '</option>';
                $("#city").html(html_element);
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
                var html_element = '';
                if (dataUser.Class == "HS") {
                    localStorage.setItem("District", JSON.stringify(response));
                }
               
                html_element = '<option value="' + response.Id + '">' + response.Name + '</option>';
                $("#district").html(html_element);
            },
            error: function (response, status, error) {
                var defaultValue = '<option value="-1">--- Chọn Quận/Huyện (Chọn Thành Phố Trước) ---</option>';
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
                var html_element = '';
                localStorage.setItem("School", JSON.stringify(response));
                html_element = '<option value="' + response.Id + '">' + response.Name + '</option>';
                $("#school").html(html_element);
            },
            error: function (response, status, error) {
                var defaultValue = '<option value="-1">--- Chọn Trường ---</option>';
                $("#school").html(defaultValue);
            }
        })
    }

    function GetCurrentId() {
        currentAreaId = $('#area').val();
        currentSchoolType = $('#bangthi').val()
        currentCityId = $('#city').val();
        currentDistrictId = $('#district').val();
        currentSchoolId = $('#school').val();
    }

    function CAllUpdate() {
        var user = GetDataUser();
        $.ajax({   
            url: '/Profile/Update',
            type: 'PUT',
            data: user,
            success: function (response) {
                Swal.fire({
                    icon: 'success',
                    title: 'SUCCESS..!!',
                    text: response.Message,
                    footer: '< a href="/Search/Search" > Trở Về Tìm Kiếm</a>'
                })
            },
            error: function (response, status, error) {
                if (response.status === 400) {
                    Swal.fire({
                        icon: 'error',
                        title: error,
                        text: response.Message,
                        footer: '< a href="/Search/Search">Trở Về Tìm Kiếm</a>'
                    })
                }

                if (response.status === 404) {
                    Swal.fire({
                        icon: 'error',
                        title: error,
                        text: response.Message,
                        footer: '< a href="/Search/Search">Trở Về Tìm Kiếm</a>'
                    })
                }
            }

        });
    }

    function GetDataUser() { // Lấy thông tin từ form html theo id tạo User
        let userName = $('#inputPhone').val();
        let email = $('#inputEmail').val();
        let fullName = $('#inputName').val();
        let bangthi = $('#bangthi').val();
        let area = $('#area').val();
        let city = $('#city').val();
        let district = $('#district').val();
        if (bangthi == "SV") {  //Nếu district = 0 thì có nghĩa đang ở mặc định
            district = 0;
        }
        let school = $('#school').val();
        let khoa = $('#inputKhoa').val();
        let lop = $('#inputLop').val();
        let id = $('.userId').val();
        let user = {
            Email: email,
            FullName: fullName,          
            Area: area,
            PhoneNumber : userName,
            Class : bangthi,
            UserName : userName,
            City : city,
            District : district,
            School : school,
            Khoa : khoa,
            Lop : lop,
            EmailConfirmed: true,
            Id : id
        }

        localStorage.setItem("updateUser", JSON.stringify(user));
        return user;
    }

    $('.confirm-update').on("click", function () {
        CAllUpdate();
    });

    function ValidateCity(currentCityId) {
        if (currentCityId == -1) {
            var html_element = 'Chưa Chọn Thành Phố';
            $('.city-error-message').html(html_element);
        }
        else {
            $('.city-error-message').html('');
        }
    }

    function ValidateDistrict(currentDistrict) {
        if (currentDistrict == -1) {
            var html_element = 'Chưa Chọn Quận/ Huyện';
            $('.district-error-message').html(html_element);
        }
        else {
            $('.district-error-message').html('');
        }
    }

    function ValidateSchool(currentSchool)
    {
        if (currentSchool == -1)
        {
            var html_element = 'Chưa Chọn Trường';
            $('.school-error-message').html(html_element);
        }
        else {
            $('.school-error-message').html('');
        }
    }
});