(function (app) {
    app.controller('chitietduanController', chitietduanController);
    chitietduanController.$inject = ['$scope', '$rootScope', 'service', 'notification', '$state', '$mdDialog', '$ngBootbox', '$stateParams', '$filter'];

    function chitietduanController($scope, $rootScope, service, notification, $state, $mdDialog, $ngBootbox, $stateParams, $filter) {

        $scope.ThongTinDuAn = {};
        $scope.HangMucDuAnTT = {};
        $scope.HangMucDuAnGt = {};
        $scope.LoaiHangMucDaTt = LoaiHangMucDaTt;
        $scope.TotalPointTT = 0;
        $scope.TotalPointGT = 0;


        //pagination
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

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
        function LoaiHangMucDaTt(loaihangmuc,page) {
            page = page || 0;
            var config = {
                params: {
                    idDuAn: $stateParams.id,
                    LoaiHm: loaihangmuc,
                    page: page,
                    pageSize: 5,
                    keyword: $scope.keyword,

                }
            }

            service.get('api/hm/gethangmucduan', config, function (result) {
                if (loaihangmuc == 0) {
                    $scope.HangMucDuAnTT = result.data.items;
                    for (var i = 0; i < $scope.HangMucDuAnTT.length; i++) {
                        $scope.HangMucDuAnTT[i].DiemHM = $scope.HangMucDuAnTT[i].DiemDanhGia * $scope.HangMucDuAnTT[i].NhomCongViec.HeSoCV * $scope.HangMucDuAnTT[i].HeSoTg.HeSoTgdk * $scope.HangMucDuAnTT[i].HeSoLap.Hesl * $scope.HangMucDuAnTT[i].HesoKcn;;
                    }
                } else {
                    $scope.HangMucDuAnGt = result.data.items;
                    for (var i = 0; i < $scope.HangMucDuAnGt.length; i++) {
                        $scope.HangMucDuAnGt[i].DiemHM = $scope.HangMucDuAnGt[i].DiemDanhGia * $scope.HangMucDuAnGt[i].NhomCongViec.HeSoCV * $scope.HangMucDuAnGt[i].HeSoTg.HeSoTgdk * $scope.HangMucDuAnGt[i].HeSoLap.Hesl * $scope.HangMucDuAnGt[i].HesoKcn;
                    }
                }
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPage;
                $scope.totalCount = result.data.TotalCount;
            }, function (error) {
            });
        }

        function Delete(id) {
          
                $ngBootbox.confirm('bạn có chắc chắn muốn xóa không ? ').then(function (result) {
                    var config = {
                        params: {
                            id: id
                        }
                    }
                    service.del('api/hm/delete', config, function (result) {
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