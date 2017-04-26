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
        $scope.LimitPersent = 100;
        $scope.Total = 0;

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
                var tmp = $scope.Total + $scope.User.Tile;
                if (tmp <= $scope.LimitPersent) {
                    $scope.HmDa.ThamGia.push(item);
                    $scope.Total = $scope.Total + $scope.User.Tile;
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
                    $scope.Total -= $scope.HmDa.ThamGia[i].HeSoThamGia;
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
                for (var i = 0; i < $scope.HmDa.ThamGia.length; i++) {
                    $scope.Total = $scope.Total + $scope.HmDa.ThamGia[i].HeSoThamGia;
                }
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