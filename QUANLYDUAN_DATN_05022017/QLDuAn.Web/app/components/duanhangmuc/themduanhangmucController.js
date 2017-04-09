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
        $scope.HmDa = {
            IdDuAn: $stateParams.idDuAn,
            ThamGia: [],
            SoNguoiThucHien: 0, 
            TrangThai: false,
            LoaiHangMuc:$stateParams.LoaiHM,
            Created_at: new Date()
        };

        $scope.User = {};
        $scope.loading = false;

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
                $scope.HmDa.ThamGia.push(item);
            } else {
                notification.error('nhân viên đã tồn tại');
            }

            $scope.HmDa.SoNguoiThucHien = $scope.HmDa.ThamGia.length;
        }

        function removeUser(id) {
            for (var i = 0; i < $scope.HmDa.ThamGia.length; i++) {
                if ($scope.HmDa.ThamGia[i].IdNhanVien == id) {
                    $scope.HmDa.ThamGia.splice(i, 1);
                }
            }
        }

        function AddHangMucDuAn() {
            service.post('api/hm/created', $scope.HmDa, function (result) {
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