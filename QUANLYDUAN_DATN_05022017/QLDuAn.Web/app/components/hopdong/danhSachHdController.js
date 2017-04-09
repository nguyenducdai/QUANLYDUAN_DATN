/// <reference path="chitethd.html" />
/// <reference path="add.html" />
(function (app) {
    app.controller('danhsachHdController', danhsachHdController);
    app.controller('ThemHdController', ThemHdController);

    danhsachHdController.$inject = ['$scope', 'service', '$state', 'notification', '$mdDialog','$rootScope'];
    ThemHdController.$inject = ['$scope', 'service', '$state', 'notification', '$mdDialog'];
    

    function danhsachHdController($scope, service, $state, notification, $mdDialog) {

        $scope.HopDong = {}
        $scope.ChiTietHopDong = {}
        $scope.alert = 'click vào dự án để xem chi tiết';

        $scope.showFrmAdd = showFrmAdd;
        $scope.viewDetail = viewDetail;
        $scope.refresh = refresh;
        $scope.templateUrl = '';
        $scope.DanhSachHd = DanhSachHd;

        //pagination
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';


        function showFrmAdd(ev) {
            $mdDialog.show({
                controller: ThemHdController,
                templateUrl: '/app/components/hopdong/add.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: $scope.customFullscreen // Only for -xs, -sm breakpoints.
            })
            .then(function (answer) {
                $scope.status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });

        }

        function viewDetail(id) {
            $scope.alert = '';
            var config = {
                params: {
                    id:id
                }
            }
            service.get('api/hd/getbyid', config, function (result) {
                $scope.ChiTietHopDong = result.data;
                console.log(result.data);
                $scope.templateUrl = '/app/components/hopdong/chitiethd.html';
            }, function (error) {
            });

        }

        function refresh() {
               DanhSachHd();
        }

        function DanhSachHd(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 8,
                    keyword: $scope.keyword
                }
            }
            service.get('api/hd/getall', config, function (result) {
                $scope.HopDong = result.data.items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPage;
                $scope.totalCount = result.data.TotalCount;
            }, function (error) {
                notification.error('có lỗi dảy ra');
            });
        }
        DanhSachHd();
    }


    function ThemHdController($scope, service, $state, notification, $mdDialog) {
        $scope.HopDong = {
            NgayBatDau: new Date(),
            NgayKetThuc: new Date(),
            NgayKy: new Date(),
            Created_at: new Date(),
            Updated_at: new Date()
        }

        $scope.ThemHopDong = ThemHopDong;
        function ThemHopDong() {
            service.post('api/hd/create', $scope.HopDong, function (result) {
                $mdDialog.cancel();
                notification.success('Thêm hợp đồng thành công');
            }, function (error) {
                notification.error('có lỗi dảy ra');
            });
        }

        function KhachHang() {
            service.get('api/hd/getcustomer', null, function (result) {
                $scope.ListKhachHang = result.data;
            }, function (error) {
                notification.error('có lỗi dảy ra');
            });
        }

        $scope.cancel = function () {
            $mdDialog.cancel();
        }
        KhachHang();
    }

})(angular.module('componentmodule'));