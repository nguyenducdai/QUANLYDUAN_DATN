(function (app) {
    app.controller('suaduanhmController', suaduanhmController);

    suaduanhmController.inject = ['$scope', '$rootScope', 'service', 'notification', '$state', '$mdDialog', '$ngBootbox', '$stateParams'];

    function suaduanhmController($scope, $rootScope, service, notification, $state, $mdDialog, $ngBootbox, $stateParams) {

        //function
        $scope.loadThanhVien = loadThanhVien;
        $scope.removeUser = removeUser;

        $scope.select = '';
        $scope.ThanhVien = {}
        $scope.NhomCvs = {}
        $scope.HeSoThoiGian = {}


        $scope.HmDa = {
            IdDuAn: $stateParams.id,
            NgayHoanThanh: null,
            TrangThai: false,
            Updated_at: new Date(),
            LoaiHangMuc: $stateParams.LoaiHM,
            SoNguoiThucHien: 0,
              NgayBatDau: (this.NgayBatDau != null) ? new Date(this.NgayBatDau).toString() : '',
            NgayHoanThanh: (this.NgayHoanThanh != null) ? new Date(this.NgayHoanThanh).toString():'',

        };
        $scope.User = {};
        $scope.LimitPersent = 100;
        $scope.Total = 0;


        function datechoose() {
            $("#dt1").datepicker({
                dateFormat: "mm/dd/yy",
                minDate: 0,
                onSelect: function (date) {
                    var dt2 = $("#dt2");
                    var startDate = $(this).datepicker('getDate');
                    var minDate = $(this).datepicker('getDate');
                    dt2.datepicker('setDate', minDate);
                    dt2.datepicker('option', 'minDate', minDate);
                    $(this).datepicker('option', 'minDate', minDate);
                    $scope.HmDa.NgayBatDau = $(this).val();
                }
            });
            $("#dt2").datepicker({
                dateFormat: "mm/dd/yy"
            });
        }
        datechoose();

        function loadThanhVien() {
            service.get('api/appuser/getalluser', null, function (result) {
                $scope.ThanhVien = result.data;
            }, function (error) {

            });
        }

        function loadNhomCongViec() {
            service.get('api/nhomcv/getall', null, function (result) {
                $scope.NhomCvs = result.data;
            }, function (error) {

            });
        }

        function loadMucDoTruyenThong() {
            service.get('api/hsl/getall', null, function (result) {
                $scope.MucDoTruyenThong = result.data;
            }, function (error) {
            });
        }

        function loadHeSoTg() {
            service.get('api/hstg/getall', null, function (result) {
                $scope.HeSoThoiGian = result.data;
            }, function (error) {
            });
        }

        $scope.addThanhVien = function () {
            var status = true;
            var item = {
                ApplicationUser: {
                    FullName: $scope.User.FullName,
                },
                IdDuAn: $scope.HmDa.IdDuAn,
                IdHangMuc: $scope.HmDa.IdHangMuc,
                IdNhanVien: $scope.User.Id,
                HeSoThamGia: $scope.User.Tile,
                LoaiHangMuc: $stateParams.LoaiHM
            }
            for (var i = 0; i < $scope.HmDa.ThamGia.length; i++) {
                if ($scope.HmDa.ThamGia[i].IdNhanVien == $scope.User.Id) {
                    status = false;
                    break;
                } else {
                    status = true;

                }
            }
            if (status) {
                var tmp = Number($scope.Total) + Number($scope.User.Tile);
                if (tmp <= Number($scope.LimitPersent)) {
                    $scope.HmDa.ThamGia.push(item);
                    $scope.Total = Number($scope.Total) + Number($scope.User.Tile);
                } else {
                    notification.error('phần trăm tham gia vượt quá 100 %');
                }
            } else {
                notification.error('nhân viên đã tồn tại');
            }

            $scope.HmDa.SoNguoiThucHien = $scope.HmDa.ThamGia.length;
        }

        function removeUser(id) {
            for (var i = 0; i < $scope.HmDa.ThamGia.length; i++) {
                if ($scope.HmDa.ThamGia[i].IdNhanVien == id) {
                    $scope.Total = Number($scope.Total) - Number($scope.HmDa.ThamGia[i].HeSoThamGia);
                    $scope.HmDa.ThamGia.splice(i, 1);
                    $scope.HmDa.SoNguoiThucHien = $scope.HmDa.ThamGia.length;
                }
            }
        }




        function getSingle() {
            var config = {
                params: {
                    IdHangMuc: $stateParams.IdHangMuc
                }
            }


            service.get('api/hm/getHangMucById', config, function (result) {
                $scope.HmDa = result.data;
                $scope.HmDa.NgayBatDau =  $.datepicker.formatDate('mm/dd/yy',new Date(result.data.NgayBatDau));
                $scope.HmDa.NgayHoanThanh = $.datepicker.formatDate('mm/dd/yy', new Date(result.data.NgayHoanThanh));
                $scope.HmDa.LoaiHangMuc = result.data.LoaiHangMuc;
                $scope.HmDa.ThamGia = result.data.ThamGia;
                console.log($.datepicker.formatDate('mm/dd/yy', result.data.NgayHoanThanh));
                for (var i = 0; i < $scope.HmDa.ThamGia.length; i++) {
                    $scope.Total = $scope.Total + $scope.HmDa.ThamGia[i].HeSoThamGia;
                }
            },
            function (error) {

            });
        }


        $scope.UpdateHangMucDuAn = function () {
            service.put('api/hm/updated', $scope.HmDa, function (result) {
                notification.success('Cập nhật hạng mục thành công');
            }, function (error) {

            })
        }

        getSingle();
        loadThanhVien();
        loadNhomCongViec();
        loadMucDoTruyenThong();
        loadHeSoTg();
    }
})(angular.module('QLdaConfig'));