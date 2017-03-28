(function (app) {
    app.controller('duanhangmucController', duanhangmucController);

    duanhangmucController.$inject = ['$scope','$rootScope', 'service', 'notification', '$state', '$mdDialog', '$ngBootbox', '$stateParams'];


    function duanhangmucController($scope,$rootScope, service, notification, $state, $mdDialog, $ngBootbox, $stateParams) {

        //function
        $scope.changeSelect = changeSelect;
        $scope.loadThanhVien = loadThanhVien;
        $scope.addThanhVien = addThanhVien;
        $scope.removeUser = removeUser;
        $scope.changNhomCongViec = changNhomCongViec;
        $scope.autoHeSoNhanCongKcv = autoHeSoNhanCongKcv;
        $scope.changeSelectDkht = changeSelectDkht;
        $scope.changeMucDoTruyenThong = changeMucDoTruyenThong;
        $scope.AddHangMucDuAn = AddHangMucDuAn;

        //object
        $scope.dataUser = [];
        $scope.DisplayUser = [];
        $scope.select = '';
        $scope.loadHangMuc = {};
        $scope.ThanhVien = {};
        $scope.NhomCvs = {};
        $scope.HmDa = {
            IdDuAn: $stateParams.id,
            SoNguoiThucHien: $scope.dataUser.length,
            NgayHoanThanh: null,
            TrangThai:false,
            Created_at: new Date()
        };
        $scope.User = {};
        $scope.loading = false;

        // array Test
        $scope.HeSoKnc = {
            data: [
                {key: 0, value :0.00}, { key: 1, value :1.00},{ key: 2,value : 1.40},{ key: 3,value : 2.01},{ key: 4,value : 2.64},{ key: 3,value : 2.01},
                { key: 5,value :3.25},  { key: 6,value :3.84},  { key: 7,value :4.41},  { key: 8,value : 4.96},  { key: 9,value :5.49},  { key: 10,value :6.00}
            ]
        };
        $scope.NgayDkHoanThanh = {
            data: [{ key: '1-4', value: 1.00 }, { key: '5-9', value: 1.50 }, { key: '10-14', value: 2.00 }, { key: '15-19', value: 2.40 }, 
                { key: '20-24', value: 2.80 },{key: '25-29', value: 3.20 },{key: '30-34', value: 3.50 },{key: '>35', value:3.80 }]
        };
        $scope.MucDoTruyenThong = {
            data: [{ key: '0', value: 1 }, { key:'1', value: 1.00 }, { key:'2', value: 0.7 }, { key: '3', value: 0.60 }, 
                { key: '>3', value: 0.50 }]
        } 

        function autoHeSoNhanCongKcv() {
            for (var i = 0; i < $scope.HeSoKnc.data.length; i++) {
                if ($scope.HmDa.SoNguoiThucHien == $scope.HeSoKnc.data[i].key) {
                    $scope.HmDa.HeSoNhanCong = $scope.HeSoKnc.data[i].value;
                    break;
                }
            }
        }
      
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

        function addThanhVien() {
            var status = false;
            var Item = {
                IdDuAn: $stateParams.id,
                IdNhanVien: $scope.User.Id,
                HeSoThamGia: $scope.User.TiLe
            };
            if ($scope.dataUser.length == 0) {
                $scope.dataUser.push(Item);
                $scope.DisplayUser.push($scope.User);
                $scope.HmDa.SoNguoiThucHien= $scope.dataUser.length;
            } 
            else {
                for (var i = 0; i < $scope.dataUser.length; i++) {
                    if ($scope.dataUser[i].IdNhanVien == $scope.User.Id) {
                            status = true;
                    } 
                }

                if (!status) {
                    $scope.dataUser.push(Item);
                    $scope.DisplayUser.push($scope.User);
                } else {
                    notification.error('thành viên đã tồn tại');
                }
            }
            $scope.HmDa.SoNguoiThucHien = $scope.dataUser.length;
            autoHeSoNhanCongKcv();
        }

        function removeUser(id) {
            for (var i = 0; i < $scope.DisplayUser.length; i++) {
                var obj = $scope.DisplayUser[i];
                if (obj.Id == id) {
                    $scope.DisplayUser.splice(i, 1);
                    $scope.dataUser.splice(i, 1);
                }
            }
            $scope.HmDa.SoNguoiThucHien = $scope.dataUser.length;
            autoHeSoNhanCongKcv();
        }

        function changNhomCongViec(id) {
            for (var i = 0; i < $scope.NhomCvs.length; i++) {
                if ($scope.NhomCvs[i].ID == id) {
                    $scope.HmDa.HeSoCongViec = $scope.NhomCvs[i].HeSoCV;
                };

            }
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

        function AddHangMucDuAn() {
            service.post('api/duanhangmuc/created', JSON.stringify({ HangMucDa: $scope.HmDa, ThamGia: $scope.dataUser}), function (result) {
                notification.success('Thêm mục công việc thành công');
            }, function (errors) {
                notification.error('có lỗi sảy ra !');
            });
        }

        loadThanhVien();
        loadNhomCongViec();
        autoHeSoNhanCongKcv();
    }

})(angular.module('QLdaConfig'));