///// <reference path="chitethd.html" />
///// <reference path="add.html" />
//(function (app) {
//    app.controller('danhsachHdController', danhsachHdController);

//    danhsachHdController.$inject = ['$scope', 'service', '$state', 'notification', '$mdDialog', '$rootScope', '$ngBootbox'];

//    function danhsachHdController($scope, service, $state, notification, $mdDialog, $rootScope ,$ngBootbox) {

//        $scope.HopDong = {}
//        $scope.items = {}

//        $scope.showFrmAdd = showFrmAdd;
//        $scope.refresh = refresh;
//        $scope.templateUrl = '';
//        $scope.DanhSachHd = DanhSachHd;
//        $scope.loadding = false;
//        $scope.ThemHopDong = ThemHopDong;
//        $scope.showFrmEdit = showFrmEdit;
//        $scope.CapNhatHopDong = CapNhatHopDong;
//        $scope.deleteHd = deleteHd;


//        //pagination
//        $scope.page = 0;
//        $scope.pagesCount = 0;
//        $scope.keyword = '';


//    function showFrmAdd(ev) {
//        $mdDialog.show({
//            controller: danhsachHdController,
//            templateUrl: '/app/components/hopdong/add.html',
//            parent: angular.element(document.body),
//            targetEvent: ev,
//            clickOutsideToClose: false,
//            fullscreen: $scope.customFullscreen, // Only for -xs, -sm breakpoints.
//            scope: $scope,
//            preserveScope: true
//        })
//        .then(function (answer) {
//            $scope.status = 'You said the information was "' + answer + '".';
//        }, function () {
//            $scope.status = 'You cancelled the dialog.';
//        });

//    }

//    function showFrmEdit(ev, id) {
//        $mdDialog.show({
//            locals: {
//                items: $scope.items
//            },
//            controller: 'danhsachHdController',
//            templateUrl: '/app/components/hopdong/edit.html',
//            parent: angular.element(document.body),
//            targetEvent: ev,
//            clickOutsideToClose: false,
//            fullscreen: $scope.customFullscreen, // Only for -xs, -sm breakpoints.
//            scope: $scope,
//            preserveScope: true
//        })
//        .then(function (answer) {
//            $scope.status = 'You said the information was "' + answer + '".';
//        }, function () {
//            $scope.status = 'You cancelled the dialog.';
//        });
//        var config = {
//            params: {
//                id: id
//            }
//        }
//        service.get('api/hd/getbyid', config, function (result) {
//            $scope.items = result.data;
//            console.log($scope.items);
//            $scope.items.NgayBatDau = new Date(result.data.NgayBatDau);
//            $scope.items.NgayKetThuc = new Date(result.data.NgayKetThuc);
//            $scope.items.NgayKy = new Date(result.data.NgayKy);
//        }, function (error) {
//        });
//    }

//    function deleteHd(id) {
//        $ngBootbox.confirm('Bạn có chắc chán muốn xóa hợp đồng này không ? ').then(function (result) {
//            var config = {
//                params: {
//                    id: id
//                }
//            }
//            $scope.loadding = true;

//            service.del('api/hd/deleted', config, function (result) {
//                $mdDialog.cancel();
//                $scope.DanhSachHd();
//                notification.success('xóa hợp đồng thành công');
//            }, function (error) {
//                notification.error('có lỗi dảy ra');
//            });
//        });
//    }

//    function refresh() {
//        DanhSachHd();
//    }

//    function DanhSachHd(page) {
//        $scope.loadding = true;
//        page = page || 0;
//        var config = {
//            params: {
//                page: page,
//                pageSize: 8,
//                keyword: $scope.keyword
//            }
//        }
//        service.get('api/hd/getall', config, function (result) {
//            $scope.loadding = false;
//            $scope.HopDong = result.data.items;
//            $scope.page = result.data.Page;
//            $scope.pagesCount = result.data.TotalPage;
//            $scope.totalCount = result.data.TotalCount;
//        }, function (error) {
//            notification.error('có lỗi dảy ra');
//        });
//    }
 
  
//    function ThemHopDong() {
//        service.post('api/hd/created', $scope.HopDong, function (result) {
//            $mdDialog.cancel();
//            $scope.DanhSachHd();
//            notification.success('Thêm hợp đồng thành công');
//        }, function (error) {
//            notification.error('có lỗi dảy ra');
//        });
//    }

//    function CapNhatHopDong() {
//        $scope.loadding = true;
//        service.put('api/hd/update', $scope.items, function (result) {
//            $mdDialog.cancel();
//            $scope.DanhSachHd();
//            notification.success('cập nhật hợp đồng thành công');
//        }, function (error) {
//            notification.error('có lỗi dảy ra');
//        });
//    }

//    function KhachHang() {
//        service.get('api/hd/getcustomer', null, function (result) {
//            $scope.ListKhachHang = result.data;
//        }, function (error) {
//            notification.error('có lỗi dảy ra');
//        });
//    }

//    $scope.cancel = function () {
//        $mdDialog.cancel();
//    }

//    KhachHang();
//    DanhSachHd();

//}
//})(angular.module('componentmodule'));