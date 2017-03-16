(function (app) {
    app.controller('chitietduanController', chitietduanController);
    chitietduanController.$inject = ['$scope', '$rootScope', 'service', 'notification', '$state', '$mdDialog', '$ngBootbox', '$stateParams'];

    function chitietduanController($scope, $rootScope, service, notification, $state, $mdDialog, $ngBootbox, $stateParams) {

        $scope.ThongTinDuAn = {};
        $scope.HangMucDuAnTT = {};
        $scope.TotalPoint = 0;
       
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

        function LoaiHangMucDaTt() {
            var TotalPoint = 0;
            var config = {
                params: {
                    id: $stateParams.id,
                    loaihangmuc:0
                }
            }

            service.get('api/duanhangmuc/gethangmucduan', config, function (result) {
                for (var i = 0; i <  result.data.length; i++) {
                    for (var j = 0; j < result.data[i].HangMuc.ThamGia.length; j++) {
                        TotalPoint = TotalPoint + (result.data[i].HangMuc.ThamGia[j].DiemThanhVien);
                    }
                }
                $scope.HangMucDuAnTT = result.data;
                $scope.TotalPoint = TotalPoint;
            }, function (error) {
            });
        }

        
        LoadById();
        LoaiHangMucDaTt();

    }

})(angular.module('QLdaConfig'));