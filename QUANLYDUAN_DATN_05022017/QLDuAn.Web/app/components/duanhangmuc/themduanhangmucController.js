(function (app) {
    app.controller('themduanhangmucController', themduanhangmucController);

    themduanhangmucController.$inject = ['$scope', '$rootScope', 'service', 'notification', '$state', '$mdDialog', '$ngBootbox', '$stateParams'];


    function themduanhangmucController($scope, $rootScope, service, notification, $state, $mdDialog, $ngBootbox, $stateParams) {

        //function
        $scope.loadThanhVien = loadThanhVien;
        $scope.AddHangMucDuAn = AddHangMucDuAn;
        $scope.removeUser = removeUser;


        $scope.select = '';
        $scope.loadHangMuc = {}
        $scope.ThanhVien = {}
        $scope.NhomCvs = {}
        $scope.HeSoThoiGian = {}
        $scope.LimitPersent = 100;
        $scope.Total = 0;


        $scope.HmDa = {
            IdDuAn: $stateParams.idDuAn,
            ThamGia: [],
            SoNguoiThucHien: 0,
            TrangThai: false,
            LoaiHangMuc: $stateParams.LoaiHM,
            NgayBatDau: (this.NgayBatDau != null) ? new Date(this.NgayBatDau).toString() : '',
            NgayHoanThanh: (this.NgayHoanThanh != null) ? new Date(this.NgayHoanThanh).toString():'',
            Created_at: new Date()
        };

        $scope.User = {};
        $scope.loadding = false;

        function datechoose() {
            $("#dt1").datepicker({
                dateFormat:"mm/dd/yy",
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
                FullName: $scope.User.FullName,
                IdDuAn: $stateParams.id,
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
                }
            }
        }
        $scope.abc = '';
        function AddHangMucDuAn() {
             
            $scope.loadding = true;
            service.post('api/hm/created', $scope.HmDa, function (result) {
                $scope.loadding = false;
                notification.success('Thêm mục công việc thành công');
            }, function (errors) {
                notification.error(errors.data);
            });
        }

        loadThanhVien();
        loadNhomCongViec();
        loadMucDoTruyenThong();
        loadHeSoTg();
    }

})(angular.module('QLdaConfig'));