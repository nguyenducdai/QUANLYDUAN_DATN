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
            SoNguoiThucHien: 0
        };
        $scope.User = {};

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
                ApplicationUser:{
                    FullName: $scope.User.FullName,
                },
                IdDuAn: $scope.HmDa.IdDuAn,
                IdHangMuc: $scope.HmDa.IdHangMuc,
                IdNhanVien: $scope.User.Id,
                HeSoThamGia: $scope.User.Tile,
                LoaiHangMuc: $stateParams.LoaiHangMuc
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

        function getSingle() {
            var config = {
                params: {
                    IdHangMuc: $stateParams.IdHangMuc
                }
            }

            service.get('api/hm/getHangMucById', config, function (result) {
                $scope.HmDa = result.data;
                $scope.HmDa.NgayBatDau = new Date(result.data.NgayBatDau);
                $scope.HmDa.NgayHoanThanh = new Date(result.data.NgayHoanThanh);
                $scope.HmDa.LoaiHangMuc = result.data.LoaiHangMuc;
                $scope.HmDa.ThamGia = result.data.ThamGia;
            },
            function (error) {

            });
        }


        $scope.UpdateHangMucDuAn= function(){
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