var common = {
    init: function () {
        common.numberFM();
        common.load();
        common.loadTk();
        common.LoadIncome();
        common.changeST();
        common.registerEvent();
        common.convert();
        common.loadAlert();
        common.detail();
        common.viewdetail();
    },
    registerEvent: function () {
        $('#btnLogout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#frmLogout').submit();
        });
        $('#xacNhan').off('change').on('change', function (e) {
            e.preventDefault();
            var id = $(this).data("id");
            alert(id);
            //common.loadTk();
        });

        $('.thongke').off('change').on('change', function (e) {
            e.preventDefault();
            common.loadTk();
        });

        $(".congviec").off('change').on('change', function (e) {
            e.preventDefault();
            common.load();
        });
        $('#alert_toggle').off('click').on('click', function (e) {
            e.preventDefault();
            $("#loadding").hide();

            common.alertToggle('#alert_toggle_run');
            common.loadAlert();
        });

        $('#close').off('click').on('click', function (e) {
            e.preventDefault();
            common.alertToggle('#alert_toggle_run');
        });
        $('.back_list_alert').off('click').on('click', function (e) {
            e.preventDefault();
            common.loadAlert();
        });

      
    },

    convert: function (date) {
        var MyDate_String_Value = date;
        var value = new Date( parseInt(MyDate_String_Value.replace(/(^.*\()|([+-].*$)/g, '')));
        return value.getMonth() + 1 +"-" +value.getDate() + "-" + value.getFullYear();

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
                common.LoadIncome($("#ID").val());
                if (response.status) {
                    var data = JSON.parse(response.data);
                    var html = "";
                    var stt = 1;
                    
                    html += '<table class=" table table-striped table-bordered table-hover"><tbody><tr><th style="width: 10px">#</th><th>Tên công việc</th><th>Ngày bắt đầu</th> <th>Ngày y/c hoàn thành</th><th>xác nhận hoàn thành</th></tr>';
                    for (var i = 0; i < data.length; i++) {
                        html += '<tr><td>' + stt++ + '</td> <td id="c">' + data[i].TenHangMuc + '</td><td>' + common.convert(data[i].NgayBatDau) + '</td><td>' + common.convert(data[i].NgayHoanThanh) + '</td><td>';

                        html += '<select class="xacNhan" onchange = "common.changeST(' + data[i].ID + ')">';
                        if (data[i].TrangThai == 0) {
                            html += '<option value="false" selected="selected">chưa hoàn thành</option><option value="true">hoàn thành</option>';
                        } else {
                            html += '<option value="false">chưa hoàn thành</option><option value="true" selected="selected">hoàn thành</option>';
                        }
                        html += ' </select></td></tr>';
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
                common.LoadIncome($(".thongke").val());
                if (response.status) {
                    var dataDirect = JSON.parse(response.dataDirect);
                    console.log(dataDirect);
                    var dataInDirect = JSON.parse(response.dataInDirect);
                    var html = "";
                    html += '<table class="table table-striped table-bordered table-hover"><tbody><tr><th style="width: 10px">#</th><th>Tên công việc</th><th style="width: 100px">Điểm</th> <th  style="width: 200px">Thu nhập (vnd)</th>  <th  style="width: 15px">Hoàn thành</th></tr>';
                    var totalMoneyDriect = 0;
                    for (var i = 0; i < dataDirect.length; i++) {
                        html += '<tr>';
                        html += '<td>' + (i + 1) + '</td><td>' + dataDirect[i].HangMuc.TenHangMuc + '</td>';
                        html += '<td>' + dataDirect[i].DiemThanhVien + '</td>';
                        html += '<td>' + common.numberFM(dataDirect[i].ThuNhap) + '</td>';
                        html += '<td><i class="fa fa-check text-success" aria-hidden="true"></i></td>';
                      
                        html += '</tr>';
                        totalMoneyDriect = totalMoneyDriect + dataDirect[i].ThuNhap;
                    }      
                    html += '</tbody>';
                    html += '<tfoot><tr><td colspan = "5"> Tổng tiền ' + common.numberFM(totalMoneyDriect) + '</td></tr></tfoot>';
                    html += '</table>';
                    $('#table-thongKe-direct').html(html);

                    var htmlIn = "";
                    htmlIn += '<table class="table table-striped table-bordered table-hover"><tbody><tr><th style="width: 10px">#</th><th>Tên công việc</th><th style="width: 100px">Điểm</th> <th  style="width: 200px">Thu nhập (vnd)</th>  <th  style="width: 15px">Hoàn thành</th></tr>';
                    var totalMoneyInDirect = 0;
                    for (var i = 0; i < dataInDirect.length; i++) {
                        htmlIn += '<tr>';
                        htmlIn += '<td>' + (i + 1) + '</td><td>' + dataInDirect[i].HangMuc.TenHangMuc + '</td>';
                        htmlIn += '<td>' + dataInDirect[i].DiemThanhVien + '</td>';
                        htmlIn += '<td>' + common.numberFM(dataInDirect[i].ThuNhap) + '</td>';
                        htmlIn += '<td><i class="fa fa-check text-success" aria-hidden="true"></i></td>';
                        htmlIn += '</tr>';
                        totalMoneyInDirect = totalMoneyInDirect + dataInDirect[i].ThuNhap;
                    }
                    htmlIn += '</tbody>';
                    htmlIn += '<tfoot><tr><td colspan = "5"> Tổng tiền ' + common.numberFM(totalMoneyInDirect) + '</td></tr></tfoot>';
                    htmlIn += '</table>';
                    $('#table-thongKe-indirect').html(htmlIn);
                    common.registerEvent();

                } else {
                    console.log(response);
                }
            }
        });
    },

    alertToggle: function (selector) {
        var effect = 'slide';
        var options = { direction: 'right' };
        var duration = 200;
        $(selector).toggle(effect, options, duration);
    },

    loadAlert: function () {
        $("#loadding").show();
        $.ajax({
            url: 'Home/GetAllAlert',
            data: {
                keyword: ''
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = JSON.parse(response.data);
                    var html = '';
                    html += '<ol>';
                    for (var i = 0; i < data.length; i++) {
                        html += '<li><a class="detail_class" onclick ="common.detail(' + data[i].Id + ')" data-id="' + data[i].Id + '">' + data[i].TieuDe + '</a></li>';
                    }
                    html += '</ol>';
                    $("#loadding").hide();
                    $(".panel-body-alert").html(html);
                }

            }
        });
         common.registerEvent();
    },

    detail: function (id) {
        $.ajax({
            url: 'Home/GetDetail',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = JSON.parse(response.data);
                    common.viewdetail(data);
                }

            }
        });
    },

    viewdetail: function (data) {
        var html = '';
        html = '<div class="detai_page">';
        html += '<h4><strong>' + data.TieuDe + '</strong></h3>';
        html += '<p> Nội dung - ' + data.NoiDung + '</p>';
        html += '<p">File đính kèm</p>';
        var moreFile = JSON.parse(data.MoreFile);
        for (var i = 0; i < moreFile.length; i++) {
            console.log(data.MoreFile[i].name);
            html += '<p> - <a href="' + moreFile[i].path + '"><i class="fa fa-file-text-o" aria-hidden="true"></i> ' + moreFile[i].name + '</a></p>';
        }
        html += '<div id="footer"><p> Người đăng : <i>' + data.NguoiTao + '</i>/' + common.convert(data.Created_at) + '</p></div>';
        html += '<hr>';
        html += '<a class="back_list_alert"><i class="fa fa-long-arrow-left" aria-hidden="true"></i> Danh sách thông báo</a>';
        html += '</div>';
        $(".panel-body-alert").html(html);
        common.registerEvent();
    },

    LoadIncome: function (id) {
        if(id != null){
            $.ajax({
                url: 'DuAn/XepHangDoanhThu',
                data: {
                    idDuAn:id
                },
                type: 'GET',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        var dataDirect = JSON.parse(response.dataDirect);
                        var dataInDirect = JSON.parse(response.dataInDirect);
                        var html = '';
                        var html2 = '';
                        for (var i = 0; i < dataDirect.length; i++) {
                            html += '<tr><td>' + dataDirect[i].ApplicationUser.FullName + '</td><td>' + common.numberFM(dataDirect[i].ThuNhap) + '</td> </tr>';
                        }

                        for (var i = 0; i < dataInDirect.length; i++) {
                            html2 += '<tr><td>' + dataInDirect[i].ApplicationUser.FullName + '</td><td>' + common.numberFM(dataInDirect[i].ThuNhap)+ '</td> </tr>';
                        }
                        $('#bxh_indirect #content').html(html2);
                        $('#bxh_direct #content').html(html);
                    }
                }
            });
        }
    },

    changeST: function (id) {
        $.ajax({
            url: "DuAn/updateStatus",
            data: {
                id: id,
                status: $(".xacNhan").val()
            },
            type: 'GET',
            dataType: 'json',
            success: function (result) {
                if (result.status == true) {
                    alert('Cập nhật trạng thái thành công ! ');
                }
            },
            error: function (error) {}
        });
     
    },
    numberFM : function(nStr)
    {
        nStr += '';
        x = nStr.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        return x1 + x2;
    }
}
common.init();
