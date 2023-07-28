$(document).ready(function () {
    $('#input_phone').keyup(function () {
        var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
        var phone = $('#input_phone').val();

        if (!vnf_regex.test(phone)) {
            var s = `<div class="alert alert-danger" role="alert">
                     Số Điện Thoại Sai Hoặc Thừa Số
                     </div>`;
            $('.user-information').html(s);
            $('.search_button').prop('disabled', true);
            $('.detail-view').css('visibility', 'hidden');
        }
        else {
            $('.search_button').prop('disabled', false)
            $('.user-information').html('');
        }

        if (!phone) {
            $('.user-information').html('');
            $('.search_button').prop('disabled', false);
        }
    })
})

function searchUserByPhone() {
    localStorage.clear(); // xóa dữ liệu cũ
    var phoneNumber = $('.phone-number').val();
    $.ajax({
        type: "GET",
        url: "/Search/SearchUserInformation?phoneNumber=" + phoneNumber,
        data: "{}",
        success: function (response) {
            var user = response;
            SaveUserToStorage(user);
            var render = ``;
            render = GetRenderInformation(user);

            $(".user-information").html(render);
            $('.detail-view').css('visibility', 'visible');
        },
        error: function (response, status, error) {
            var render = ``;
            if (response.status == 404 || response.status == 400) {
                render = `<div class="alert alert-danger" role="alert">
                          ${response.responseJSON.Message}
                          </div>`;
            }
            $(".user-information").html(render);
            $('.detail-view').css('visibility', 'hidden');
        }
    })
}

function GetRenderInformation(user) {
    let render = `
    <h2 class="bg-home-top text-white a-text-20 text-uppercase p-3 w-100" style="background-color:#02a1e3">Thông tin cá nhân</h2>
                <div class="col-md-6 col-12">
                    <table class="table a-text-16 table-no-border">
                        <tbody>
                            <tr>
                                <td class="col-min"><i class="fa fa-user" aria-hidden="true"></i> <span class="hidden-xs">Họ tên</span></td>
                                <td><span class="hidden-xs">:&nbsp;&nbsp;</span><strong>${user.FullName}</strong></td>
                            </tr>
                            <tr>
                                <td class="col-min"><i class="fa fa-envelope" aria-hidden="true"></i> <span class="hidden-xs">Email</span></td>
                                <td><span class="hidden-xs">:&nbsp;&nbsp;</span><strong>${user.Email}</strong></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="col-md-6 col-12">
                    <table class="table a-text-16 table-no-border">
                        <tbody>
                            <tr>
                                <td class="col-min"><i class="fa-solid fa-phone"></i> <span class="hidden-xs">SĐT</span></td>
                                <td><span class="hidden-xs">:&nbsp;&nbsp;</span><strong>${user.UserName}</strong></td>
                            </tr>
                            <tr>
                                <td class="col-min"><i class="fa fa-users" aria-hidden="true"></i> <span class="hidden-xs">Bảng Thi</span></td>
                                <td>
                                    <span class="hidden-xs">:&nbsp;&nbsp;</span><strong>
                                        <span>${user.Class}</span>
                                    </strong>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>`;

    return render;
}

function SaveUserToStorage(user) {
    GetCityById(user.City);
    if (user.Class == "HS") {
        GetDistrictById(user.District);
    }
    GetSchoolById(user.School);
    localStorage.setItem("userData", JSON.stringify(user));
}


function GetCityById(cityId) {
    $.ajax({
        type: "GET",
        url: "/city/" + cityId,
        data: "{}",
        success: function (response) {
            var html_element = '';
            localStorage.setItem("City", JSON.stringify(response));
            //html_element = '<option value="' + response.Id + '">' + response.Name + '</option>';
            //$("#city").html(html_element);
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
            localStorage.setItem("District", JSON.stringify(response));
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

