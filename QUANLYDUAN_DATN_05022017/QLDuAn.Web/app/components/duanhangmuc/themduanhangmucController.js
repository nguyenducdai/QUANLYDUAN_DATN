(function (app) {
    app.controller('themduanhangmucController', themduanhangmucController);

    themduanhangmucController.$inject = ['$scope', '$rootScope', 'service', 'notification', '$state', '$mdDialog', '$ngBootbox', '$stateParams'];


    function themduanhangmucController($scope, $rootScope, service, notification, $state, $mdDialog, $ngBootbox, $stateParams) {

        //function
        $scope.changeSelect = changeSelect;
        $scope.loadThanhVien = loadThanhVien;
        $scope.AddHangMucDuAn = AddHangMucDuAn;


        $scope.select = '';
        $scope.loadHangMuc = {}
        $scope.ThanhVien = {}
        $scope.NhomCvs = {}
        $scope.HeSoThoiGian={}
        $scope.HmDa = {
            IdDuAn: $stateParams.id,
            NgayHoanThanh: null,
            TrangThai: false,
            Created_at: new Date(),
            ThamGia: [],
            SoNguoiThucHien:0
        };
        $scope.User = {};
        $scope.loading = false;


      
        function changeSelect() {
            $scope.loading = true;
            var config = {
                params: {
                    option: $scope.HmDa.LoaiHangMuc
                }
            }
            service.get('api/hm/getbylhm', config, function (result) {
                $scope.loadHangMuc = result.data;
                $scope.loading = false;
            }, function () {

            });
        }

        function loadThanhVien() {
            service.get('api/appuser/getall', null, function (result) {
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

        function loadMucDoTruyenThong(){
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
                HeSoThamGia: $scope.User.Tile
            }
            for (var i = 0; i < $scope.HmDa.ThamGia.length; i++) {
                if ($scope.HmDa.ThamGia[i].IdNhanVien == $scope.User.Id) {
                     status = false;
                } else {
                     status = true;
                }
            }
            if (status) {
                $scope.HmDa.ThamGia.push(item);
            } else {
                notification.error('nhân viên đã tồn tại');
            }

            $scope.HmDa.SoNguoiThucHien = $scope.HmDa.ThamGia.length;
        }

        $scope.removeUser = function (id) {
            for (var i = 0; i < $scope.HmDa.ThamGia.length; i++) {
                if ($scope.HmDa.ThamGia[i] == id) {
                    $scope.HmDa.ThamGia.splice(i, 1);
                }
            }
        }

        function AddHangMucDuAn() {
            service.post('api/duanhangmuc/created', $scope.HmDa, function (result) {
                notification.success('Thêm mục công việc thành công');
            }, function (errors) {
                notification.error('có lỗi sảy ra !');
            });
        }

        loadThanhVien();
        loadNhomCongViec();
        loadMucDoTruyenThong();
        loadHeSoTg();
    }

})(angular.module('QLdaConfig'));