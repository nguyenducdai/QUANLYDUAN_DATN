﻿/// <reference path="../nhomnguoidung/dsnhomnguoidung.html" />
(function (app) {
    app.controller('thanhvienController', thanhvienController);

    thanhvienController.$inject = ['$scope', 'service', '$state', 'notification', '$mdDialog', '$rootScope', '$ngBootbox']
    function thanhvienController($scope, service, $state, notification, $mdDialog, $rootScope, $ngBootbox) {

        $scope.ThanhVien = {
            Created_at: new Date,
            Updatted_at:new Date,
            Groups: []
        }
        $scope.themThanhVien = themThanhVien;
        $scope.suaThanhVien = suaThanhVien;
        $scope.groups = {}
        $scope.ListNhanVien = {}
        $scope.Detail = {}
        $scope.templateUrl = '';
        $scope.UpdateThanhVien = {
            Created_at: new Date,
            Updatted_at: new Date,
            Groups: []
        }

        function themThanhVien(ev) {
            $mdDialog.show({
                controller:thanhvienController,
                templateUrl: '/app/components/thanhvien/themthanhvien.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: $scope.customFullscreen
            })
            .then(function (answer) {
                $scope.status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
        }

        function suaThanhVien(ev, id) {
            var config = {
                params: {
                    id: id
                }
            }
            service.get('api/appuser/detail', config, function (result) {
                $scope.UpdateThanhVien = result.data;
                $scope.UpdateThanhVien.Birthday = new Date(result.data.Birthday);
                $scope.UpdateThanhVien.Startdate = new Date(result.data.Startdate);
            }, function (error) {
                notification.error(error);
            });
            $mdDialog.show({
                locals:{
                    UpdateThanhVien: $scope.UpdateThanhVien
                },
                controller: thanhvienController,
                templateUrl: '/app/components/thanhvien/suaThanhVien.html',
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

        $scope.capnhatThanhVien = function(){
            service.put('api/appuser/updated', $scope.UpdateThanhVien, function (result) {
                notification.success('cập nhật thành viên thành công');
                $scope.cancel();
                loadApplicationUser();
            }, function (error) {
                notification.error('Có lỗi sảy ra');
            })
        }

        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.ThanhVien.Image = fileUrl;
                });
            }
            finder.popup();
        }

        $scope.cancel = function () {
            $mdDialog.cancel();
        }

        $scope.delete = function (id) {
            $ngBootbox.confirm('bạn có chắc chắn muốn xóa không').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                service.del('api/appuser/delete', config, function (result) {
                    loadApplicationUser();
                    notification.success('xóa thành viên ' + result.data.FullName + ' thành công');
                }, function (error) {
                    notification.error('xóa bản ghi không thành công');
                })
            });
           
        }

        function loadApplicationGroup() {
            service.get('api/applicationgroup/getall',null, function (result) {
                $scope.groups = result.data;
            }, function (error) {
                notification.error('không load được nhóm người dùng');
            })
        }

        function loadApplicationUser() {
            service.get('api/appuser/getall', null, function (result) {
                $scope.ListNhanVien = result.data;
            }, function (error) {
                notification.error('không load được nhân viên');
            })
        }

        $scope.AddApplicationUser = function () {
            service.post('api/appuser/created', $scope.ThanhVien, function (result) {
                loadApplicationUser();
                notification.success('Thêm thành viên thành công');
                $scope.cancel();
            }, function (error) {
                notification.error('Có lỗi sảy ra');
            })
        }

        $scope.viewDetail = function (id) {
            var config = {
                params: {
                    id:id
                }
            }
            service.get('api/appuser/detail', config, function (result) {
                $scope.Detail = result.data;
                $scope.templateUrl = '/app/components/thanhvien/chitietthanhvien.html';
            }, function (error) {
                notification.error(error);
            })
        }
        loadApplicationUser();
        loadApplicationGroup();
      
    }

})(angular.module('componentmodule'));