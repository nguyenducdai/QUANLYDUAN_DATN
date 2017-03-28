(function (app) {
    app.controller('chitietduanController', chitietduanController);
    chitietduanController.$inject = ['$scope', '$rootScope', 'service', 'notification', '$state', '$mdDialog', '$ngBootbox', '$stateParams'];

    function chitietduanController($scope, $rootScope, service, notification, $state, $mdDialog, $ngBootbox, $stateParams) {

        $scope.ThongTinDuAn = {};
        $scope.HangMucDuAnTT = {};
        $scope.HangMucDuAnGt = {};
        $scope.TotalPoint = 0;

        $scope.displayGT = displayGT;
        $scope.Delete = Delete;

        function displayGT(loaihangmuc) {
            LoaiHangMucDaTt(loaihangmuc);
        }

        function LoadById() {
            var config = {
                params: {
                    id: $stateParams.id,
                }
            }
            service.get('api/duan/getInfo', config, function (result) {
                $scope.ThongTinDuAn = result.data;
               
            }, function (error) {
            });
        }

        //loai hang muc tt=0
        function LoaiHangMucDaTt(loaihangmuc) {
            var TotalPoint = 0;
            var config = {
                params: {
                    id: $stateParams.id,
                    loaihangmuc: loaihangmuc
                }
            }

            service.get('api/duanhangmuc/gethangmucduan', config, function (result) {
                for (var i = 0; i < result.data.length; i++) {
                    for (var j = 0; j < result.data[i].HangMuc.ThamGia.length; j++) {
                        TotalPoint = TotalPoint + (result.data[i].HangMuc.ThamGia[j].DiemThanhVien);
                    }
                }
                if (loaihangmuc == 0) {
                    $scope.HangMucDuAnTT = result.data;
                } else {
                    $scope.HangMucDuAnGt = result.data;
                }
                $scope.TotalPoint = TotalPoint;
            }, function (error) {
            });
        }

        function Delete(IdHangMuc,IdDuAn, IdNhomCongViec, LoaiHangMuc) {
          
                $ngBootbox.confirm('bạn có chắc chắn muốn xóa không ? ').then(function (result) {
                    var config = {
                        params: {
                            IdHangMuc: IdHangMuc,
                            IdDuAn: IdDuAn,
                            IdNhomCongViec: IdNhomCongViec,
                            LoaiHangMuc: LoaiHangMuc
                        }
                    }
                    service.del('api/duanhangmuc/delete', config, function (result) {
                        LoaiHangMucDaTt(0);
                        LoaiHangMucDaTt(1);
                        notification.success('Xóa hạng mục công việc thành công ! ');
                    }, function (error) {
                    });
                })
        }

        LoadById();
        LoaiHangMucDaTt(0);
    }

})(angular.module('QLdaConfig'));