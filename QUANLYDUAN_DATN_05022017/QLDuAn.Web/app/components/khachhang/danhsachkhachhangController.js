﻿(function (app) {
    app.controller('danhsachkhachhangController',danhsachkhachhangController);
    danhsachkhachhangController.$inject = ['$scope', 'service', 'notification', '$mdDialog', '$rootScope'];


    function danhsachkhachhangController($scope, service, notification, $mdDialog, $rootScope) {

        $scope.KhachHang = {
            GioiTinh:true
        };
        $scope.KhachHangEdit= {}

        $scope.showFrmAddKh = showFrmAddKh;
        $scope.xoaKhachHang = xoaKhachHang;
        $scope.showFromEdit = showFromEdit;
        $scope.suaKhachHang = suaKhachHang;
        $rootScope.cancel = cancel;

        function showFrmAddKh(ev) {
            $mdDialog.show({
                controller:themkhachhangController,
                templateUrl: '/app/components/khachHang/themkhachhang.html',
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

        function xoaKhachHang(id) {
            var config = {
                params: {
                    id:id
                }
            }
            service.del('api/kh/delete', config, function (result) {
                LoadKhachHang();
                notification.success('xóa khách hàng thành công');
            }, function (error) {
                notification.error('load dữ liệu thất bại');
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
                locals:{
                    KhachHangEdit :$scope.KhachHangEdit
                },
                controller:danhsachkhachhangController,
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
            service.put('api/kh/update', $scope.KhachHangEdit, function (result) {
                LoadKhachHang();
                notification.success('cập nhật khách hàng thành công');
                cancel();
            }, function (error) {
                notification.error('có lỗi sảy ra ~');
            })
        }
 
        function LoadKhachHang() {
            service.get('api/kh/getall', null, function (result) {
                $scope.KhachHang = result.data;
            }, function (error) {
                notification.error('load dữ liệu thất bại');
            });
        }

        function cancel() {
            $mdDialog.cancel();
        }
      
        function themkhachhangController($scope, service, notification, $mdDialog) {

            $scope.themKhachHang = themKhachHang;

            function themKhachHang() {
                service.post('api/kh/create', $scope.KhachHang, function (result) {
                    LoadKhachHang();
                    notification.success('thêm khách hàng thành công');
                    cancel();
                }, function (error) {
                    notification.error('có lỗi sảy ra ~');
                })
            }

        }    

        LoadKhachHang();
    }
    
 
 
})(angular.module('componentmodule'))