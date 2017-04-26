(function (app) {
    app.controller('danhsachkhachhangController', danhsachkhachhangController);
    danhsachkhachhangController.$inject = ['$scope', 'service', 'notification', '$mdDialog', '$rootScope', '$ngBootbox'];


    function danhsachkhachhangController($scope, service, notification, $mdDialog, $rootScope ,$ngBootbox) {

        $scope.KhachHang = {};
        $scope.KhachHangEdit = {}
        $scope.AddKhachHang = { GioiTinh: true };

        $scope.showFrmAddKh = showFrmAddKh;
        $scope.xoaKhachHang = xoaKhachHang;
        $scope.showFromEdit = showFromEdit;
        $scope.suaKhachHang = suaKhachHang;
        $scope.LoadKhachHang = LoadKhachHang;
        $scope.cancel = cancel;
        $scope.themKhachHang = themKhachHang;
        $scope.refresh = refresh;
        $scope.loadding = false;

        //pagination
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';


        function showFrmAddKh(ev) {
            $mdDialog.show({
                controller: 'danhsachkhachhangController',
                templateUrl: '/app/components/khachHang/themkhachhang.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: $scope.customFullscreen, 
                scope: $scope,
                preserveScope: true
            })
          .then(function (answer) {
              $scope.status = 'You said the information was "' + answer + '".';
          }, function () {
              $scope.status = 'You cancelled the dialog.';
          });
        }

        function xoaKhachHang(id) {
            $ngBootbox.confirm('bạn có chắc chắn muốn xóa khách hàng này không').then(function (result) {
                var config = {
                    params: {
                        id: id
                    }
                }
                service.del('api/kh/delete', config, function (result) {
                    $scope.LoadKhachHang();
                    notification.success('xóa khách hàng thành công');
                }, function (error) {
                    notification.error('load dữ liệu thất bại');
                });
            });
        }

        function showFromEdit(ev, id) {

            var config = {
                params: {
                    id: id
                }
            }
            service.get('api/kh/getbyid', config, function (result) {
                $scope.KhachHangEdit = result.data;
            }, function (error) {
                notification.error('load dữ liệu thất bại');
            });

            $mdDialog.show({
                locals: {
                    KhachHangEdit: $scope.KhachHangEdit
                },
                controller: danhsachkhachhangController,
                templateUrl: '/app/components/khachHang/suakhachang.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: $scope.customFullscreen,
                scope: $scope,
                preserveScope: true
            })
         .then(function (answer) {
             $scope.status = 'You said the information was "' + answer + '".';
         }, function () {
             $scope.status = 'You cancelled the dialog.';
         });
        }

        function suaKhachHang() {
            $scope.loadding = true;
            service.put('api/kh/update', $scope.KhachHangEdit, function (result) {
                $scope.LoadKhachHang();
                notification.success('cập nhật khách hàng thành công');
                cancel();
            }, function (error) {
                $scope.loadding = false;
                notification.error('có lỗi sảy ra ~');
            })
        }

        function LoadKhachHang(page) {
            $scope.loadding = true;
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    keyword: $scope.keyword
                }
            }
            service.get('api/kh/getall', config, function (result) {
                $scope.loadding = false;
                $scope.KhachHang = result.data.items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPage;
                $scope.totalCount = result.data.TotalCount;
            }, function (error) {
                notification.error('load dữ liệu thất bại');
            });
        }

        function cancel() {
            $mdDialog.cancel();
        }

        function refresh() {
            $scope.LoadKhachHang();
        }

        function themKhachHang() {
            $scope.loadding = true;
            service.post('api/kh/create', $scope.AddKhachHang, function (result) {
                cancel();
                $scope.LoadKhachHang();
                notification.success('thêm khách hàng thành công');
            }, function (error) {
                $scope.loadding = false;
                notification.error('có lỗi sảy ra ~');
            })
        }


        $scope.LoadKhachHang();
    }



})(angular.module('componentmodule'))