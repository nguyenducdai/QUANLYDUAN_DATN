(function (app) {
    app.controller('suaduanhmController', suaduanhmController);

    suaduanhmController.inject = ['$scope', '$rootScope', 'service', 'notification', '$state', '$mdDialog', '$ngBootbox', '$stateParams'];

    function suaduanhmController($scope, $rootScope, service, notification, $state, $mdDialog, $ngBootbox, $stateParams) {

        $scope.HmDa = {}
        $scope.ThanhVien = {}
        $scope.DisplayUser = [];
        $scope.User = [];

        $scope.getSingle = getSingle;
        $scope.loadHangMuc = loadHangMuc;
        $scope.changeSelectDkht = changeSelectDkht;
        $scope.changeMucDoTruyenThong = changeMucDoTruyenThong; 
        $scope.changNhomCongViec = changNhomCongViec;
        $scope.addThanhVien = addThanhVien;
        $scope.removeUser = removeUser;
        $scope.UpdateHangMucDuAn = UpdateHangMucDuAn;


        // array Test
        $scope.HeSoKnc = {
            data: [
                { key: 0, value: 0.00 }, { key: 1, value: 1.00 }, { key: 2, value: 1.40 }, { key: 3, value: 2.01 }, { key: 4, value: 2.64 }, { key: 3, value: 2.01 },
                { key: 5, value: 3.25 }, { key: 6, value: 3.84 }, { key: 7, value: 4.41 }, { key: 8, value: 4.96 }, { key: 9, value: 5.49 }, { key: 10, value: 6.00 }
            ]
        };
        $scope.NgayDkHoanThanh = {
            data: [{ key: '1-4', value: 1.00 }, { key: '5-9', value: 1.50 }, { key: '10-14', value: 2.00 }, { key: '15-19', value: 2.40 },
                { key: '20-24', value: 2.80 }, { key: '25-29', value: 3.20 }, { key: '30-34', value: 3.50 }, { key: '>35', value: 3.80 }]
        };
        $scope.MucDoTruyenThong = {
            data: [{ key: '0', value: 1 }, { key: '1', value: 1.00 }, { key: '2', value: 0.7 }, { key: '3', value: 0.60 },
                { key: '>3', value: 0.50 }]
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
        function autoHeSoNhanCongKcv() {
            for (var i = 0; i < $scope.HeSoKnc.data.length; i++) {
                if ($scope.HmDa.SoNguoiThucHien == $scope.HeSoKnc.data[i].key) {
                    $scope.HmDa.HeSoNhanCong = $scope.HeSoKnc.data[i].value;
                    break;
                }
            }
        }
        function loadHangMuc() {
            var config = {
                params: {
                    option: $scope.HmDa.LoaiHangMuc
                }
            }
            service.get('api/hm/getbylhm', config, function (result) {
                $scope.loadHangMuc = result.data;
            }, function () {

            });
        }
        function changeSelectDkht(id) {
            for (var i = 0; i < $scope.NgayDkHoanThanh.data.length; i++) {
                if ($scope.NgayDkHoanThanh.data[i].value == id) {
                    $scope.HmDa.HeSoThoiGian = $scope.NgayDkHoanThanh.data[i].value;
                    break;
                }
            }
        }
        function changeMucDoTruyenThong(id) {
            for (var i = 0; i < $scope.MucDoTruyenThong.data.length; i++) {
                if ($scope.MucDoTruyenThong.data[i].value == id) {
                    $scope.HmDa.HeSoLapLai = $scope.MucDoTruyenThong.data[i].value;
                    break;
                }
            }
        }
        function changNhomCongViec(id) {
            for (var i = 0; i < $scope.NhomCvs.length; i++) {
                if ($scope.NhomCvs[i].ID == id) {
                    $scope.HmDa.HeSoCongViec = $scope.NhomCvs[i].HeSoCV;
                };

            }
        }
        function addThanhVien() {
            var status = false;
            var Item = {
                IdDuAn: $stateParams.id,
                IdHangMuc: $stateParams.IdHangMuc,
                IdNhanVien: $scope.User.Id,
                HeSoThamGia: $scope.User.TiLe,
                LoaiHangMuc: $stateParams.LoaiHangMuc

            }
            var ApplicationUser = {
                    ApplicationUser: $scope.User,
                    HeSoThamGia: $scope.User.TiLe,
                    IdDuAn: $stateParams.IdDuAn,
                    IdHangMuc: $stateParams.IdHangMuc,
                    IdNhanVien: $scope.User.Id,
                    HeSoThamGia: $scope.User.TiLe,
                    LoaiHangMuc: $stateParams.LoaiHangMuc
            }
            if ($scope.DisplayUser.length == 0) {
                $scope.DisplayUser.push(ApplicationUser);
            }
            else {
                for (var i = 0; i < $scope.DisplayUser.length; i++) {
                    if ($scope.DisplayUser[i].IdNhanVien == $scope.User.Id) {
                        status = true;
                    }
                }
                if (!status) {
                    $scope.DisplayUser.push(ApplicationUser);
                } else {
                    notification.error('thành viên đã tồn tại');
                }
            }
            $scope.HmDa.SoNguoiThucHien = $scope.DisplayUser.length;
            autoHeSoNhanCongKcv();
        }
        function removeUser(id) {
            for (var i = 0; i < $scope.DisplayUser.length; i++) {
                var obj = $scope.DisplayUser[i];
                if (obj.ApplicationUser.Id == id) {
                    $scope.DisplayUser.splice(i, 1);
                }
            }
            $scope.HmDa.SoNguoiThucHien = $scope.DisplayUser.length;
            autoHeSoNhanCongKcv();
        }
        

        function getSingle() {
            var config = {
                params: {
                    IdHangMuc: $stateParams.IdHangMuc,
                    IdDuAn: $stateParams.IdDuAn,
                    IdNhomCongViec: $stateParams.IdNhomCongViec,
                    LoaiHangMuc: $stateParams.LoaiHangMuc
                }
            }

            service.get('api/duanhangmuc/getSingle', config, function (result) {
                $scope.HmDa = result.data;
                $scope.HmDa.NgayBatDau = new Date(result.data.NgayBatDau);
                $scope.HmDa.NgayDuKienKetThuc = new Date(result.data.NgayDuKienKetThuc);
                $scope.HmDa.LoaiHangMuc = result.data.LoaiHangMuc;
                $scope.HmDa.ThoiGianThucHienDuKien = result.data.HeSoThoiGian;
                $scope.DisplayUser = result.data.HangMuc.ThamGia;
                console.log($scope.DisplayUser);
                loadHangMuc();
            },
            function (error) {

            });
        }


        function UpdateHangMucDuAn() {
            service.put('api/duanhangmuc/update', JSON.stringify({ HangMucDa: $scope.HmDa, ThamGia: $scope.DisplayUser }), function (result) {
                notification.success('Cập nhật hạng mục thành công');
                //$state.go('chitietduan({id: $scope.HmDa.IdDuAn})');
            }, function (error) {

            })
        }

        getSingle();
        loadThanhVien();
        loadNhomCongViec();
        autoHeSoNhanCongKcv();
    }
})(angular.module('QLdaConfig'));