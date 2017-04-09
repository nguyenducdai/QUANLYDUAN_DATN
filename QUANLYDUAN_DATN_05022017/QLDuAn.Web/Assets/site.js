var common = {
    init: function () {
        common.load();
        common.loadTk();
        common.registerEvent();
        common.convert();

    },
    registerEvent: function () {
        $('#btnLogout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#frmLogout').submit();
        });
        $('#xacNhan').off('change').on('change', function (e) {
            e.preventDefault();
            common.loadTk();
        });

        $('.thongke').off('change').on('change', function (e) {
            e.preventDefault();
            alert("ok");
        });

        $(".congviec").off('change').on('change', function (e) {
            e.preventDefault();
            common.load();
        });
    },

    convert: function (date) {
        var value = new Date(parseInt(date.replace("/Date(", "").replace(")/", ""), 10));
        var dat = value.getMonth() + 1 + "/" + value.getDate() + "/" + value.getFullYear();
        return dat;
    },
    load: function () {
        $.ajax({
            url: 'DuAn/GetHangMucCV',
            data: {
                idDuAn: $("#ID").val()
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = JSON.parse(response.data);
                    var html = "";
                    var stt = 1;
                    html += '<table class="table"><tbody><tr><th style="width: 10px">#</th><th>Tên công việc</th><th>Ngày bắt đầu</th> <th>Ngày y/c hoàn thành</th><th>xác nhận hoàn thành</th></tr>';
                    for (var i = 0; i < data.length; i++) {
                        html += '<tr><td>' + stt++ + '</td> <td id="c">' + data[i].TenHangMuc + '</td><td>' + common.convert(data[i].NgayBatDau) + '</td><td>' + common.convert(data[i].NgayHoanThanh) + '</td><td><select id="xacNhan">';
                        if (data[i].TrangThai == 0) {
                            html += '<option value="0" selected="selected">chưa hoàn thành</option><option value="1">hoàn thành</option> </select></td></tr>';
                        } else {
                            html += '<option value="0">chưa hoàn thành</option><option value="1" selected="selected">hoàn thành</option> </select></td></tr>';
                        }
                    }
                    html += '</tbody> </table>';
                    $("#table-hangmucda").html(html);
                    common.registerEvent();

                } else {
                    console.log(response);
                }
            }
        });
    },

    loadTk: function () {
        $.ajax({
            url: 'DuAn/ThongKeHangMc',
            data: {
                idDuAn: $(".thongke").val()
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = JSON.parse(response.data);
                    console.log(data);
                    var html = "";
                    html += '<table class="table"><tbody><tr><th style="width: 10px">#</th><th>Tên công việc</th><th>điểm thành viên</th> <th>thu nhập (vnd)</th></tr>';
                    for (var i = 0; i < data.length; i++) {
                        html += '<tr>';
                        html += '<td>' + i + '</td><td>' + data[i].TenHangMuc + '</td><td>' + data[i].ThamGia[i].DiemThanhVien + '(điểm)</td><td>vnd</td>';
                        html +='</tr>';
                    }      
                    html += '</tbody></table>';
                    $('#table-thongKe').html(html);
                    common.registerEvent();

                } else {
                    console.log(response);
                }
            }
        });
    },
}
common.init();
