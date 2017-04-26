(function (app) {
    app.controller('HeSoController', ['$scope', 'service', 'notification', '$state', '$mdDialog', '$ngBootbox', function ($scope, service, notification, $state ,$mdDialog, $ngBootbox) {

        $scope.ListNhomCongViec = ListNhomCongViec;
        $scope.ListHeSoNhanCongKnc = ListHeSoNhanCongKnc;
        $scope.ListHeSoThoiGian = ListHeSoThoiGian;
        $scope.ListHeSoLap = ListHeSoLap;
        $scope.cancel = cancel;

        // NHÓM CÔNG VIỆC
        $scope.NhomCV = {};
        $scope.ItemEditNcvObj = {};
        $scope.ThemNcv = ThemNcv;
        $scope.showFrmAddNCV = showFrmAddNCV;
        $scope.showFrmEditNCV = showFrmEditNCV;
        $scope.CapNhatNcv = CapNhatNcv;
        $scope.deleteNcv = deleteNcv;

        function showFrmAddNCV(ev) {
            $mdDialog.show({
                controller: 'HeSoController',
                templateUrl: '/app/components/heso/themNcv.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: $scope.customFullscreen,// Only for -xs, -sm breakpoints.
                scope: $scope,
                preserveScope: true
            })
            .then(function (answer) {
                $scope.status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
        }

        function ThemNcv() {
            service.post('api/nhomcv/created', $scope.NhomCV, function (result) {
                $scope.cancel();
                ListNhomCongViec();
                notification.success('Thêm nhóm công việc thành công');
            }, function (error) { });
        }

        function showFrmEditNCV(ev, id) {

            var config = {
                params: {
                    id: id
                }
            }
            service.get('api/nhomcv/getbyid', config, function (result) {
                $scope.ItemEditNcvObj = result.data;
            }, function (error) { });

            $mdDialog.show({
                locals: {
                    ItemEditNcvObj: $scope.ItemEditNcvObj,
                },
                controller: 'HeSoController',
                templateUrl: '/app/components/heso/suaNcv.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: $scope.customFullscreen,// Only for -xs, -sm breakpoints.
                scope: $scope,
                preserveScope: true
            })
            .then(function (answer) {
                $scope.status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
        }

        function CapNhatNcv() {
            service.put('api/nhomcv/updated', $scope.ItemEditNcvObj, function (result) {
                $scope.cancel();
                ListNhomCongViec();
                notification.success('cập nhật công việc thành công');
            }, function (error) { });
        }

        function deleteNcv(id) {
            $ngBootbox.confirm('bạn có chắc chắn muôn xóa nhóm công việc này không ? ').then(function (result) {
                var config = {
                    params: {
                        id: id
                    }
                }
                service.del('api/nhomcv/delete', config, function (result) {
                    ListNhomCongViec();
                    notification.success("Xóa Nhóm Công việc thành công ~");
                }, function (error) {
                    notification.error("lỗi ~");
                });
            });
        }


        // HỆ SỐ LĂP
        $scope.Hesl = {};
        $scope.HeslUpdate = {};
        $scope.ThemHsl = ThemHsl;
        $scope.showFrmAddHSL = showFrmAddHSL;
        $scope.showFrmEditHSL = showFrmEditHSL;
        $scope.CapNhatHsl = CapNhatHsl;
        $scope.deleteHsl = deleteHsl;

        function showFrmAddHSL(ev) {
            $mdDialog.show({
                controller: 'HeSoController',
                templateUrl: '/app/components/heso/themHsl.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: $scope.customFullscreen,// Only for -xs, -sm breakpoints.
                scope: $scope,
                preserveScope: true
            })
            .then(function (answer) {
                $scope.status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
        }

        function ThemHsl() {
            service.post('api/hsl/created', $scope.Hesl, function (result) {
                $scope.cancel();
                ListHeSoLap();
                notification.success('Thêm nhóm hệ số lặp thành công');
            }, function (error) {
                notification.error(error.data);
            });
        }

        function showFrmEditHSL(ev, id) {

            var config = {
                params: {
                    id: id
                }
            }
            service.get('api/hsl/getbyid', config, function (result) {
                $scope.HeslUpdate = result.data;
            }, function (error) { });

            $mdDialog.show({
                locals: {
                    HeslUpdate: $scope.HeslUpdate,
                },
                controller: 'HeSoController',
                templateUrl: '/app/components/heso/suaHsl.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: $scope.customFullscreen,// Only for -xs, -sm breakpoints.
                scope: $scope,
                preserveScope: true
            })
            .then(function (answer) {
                $scope.status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
        }

        function CapNhatHsl() {
            service.put('api/hsl/updated', $scope.HeslUpdate, function (result) {
                $scope.cancel();
                ListHeSoLap();
                notification.success('cập nhật hệ số lặp thành công');
            }, function (error) { });
        }

        function deleteHsl(id) {
            $ngBootbox.confirm('bạn có chắc chắn muôn xóa Hệ số này không ? ').then(function (result) {
                var config = {
                    params: {
                        id: id
                    }
                }
                service.del('api/hsl/delete', config, function (result) {
                    ListHeSoLap();
                    notification.success("Xóa hệ số thành công ~");
                }, function (error) {
                    notification.error("lỗi ~");
                });
            });
        }



        // HỆ SỐ THỜI GIAN
        $scope.Hestg = {};
        $scope.HesltgUpdate = {};
        $scope.ThemHstg = ThemHstg;
        $scope.showFrmAddHSTG = showFrmAddHSTG;
        $scope.showFrmEditHSTG = showFrmEditHSTG;
        $scope.CapNhatHstg = CapNhatHstg;
        $scope.deleteHstg = deleteHstg;

        function showFrmAddHSTG(ev) {
            $mdDialog.show({
                controller: 'HeSoController',
                templateUrl: '/app/components/heso/themHstg.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: $scope.customFullscreen,// Only for -xs, -sm breakpoints.
                scope: $scope,
                preserveScope: true
            })
            .then(function (answer) {
                $scope.status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
        }

        function ThemHstg() {
            service.post('api/hstg/created', $scope.Hestg, function (result) {
                $scope.cancel();
                ListHeSoThoiGian();
                notification.success('Thêm nhóm hệ số thời gian thành công');
            }, function (error) {
                notification.error(error.data);
            });
        }

        function showFrmEditHSTG(ev, id) {

            var config = {
                params: {
                    id: id
                }
            }
            service.get('api/hstg/getbyid', config, function (result) {
                $scope.HesltgUpdate = result.data;
                console.log($scope.HesltgUpdate);
            }, function (error) {

            });

            $mdDialog.show({
                locals: {
                    HesltgUpdate: $scope.HesltgUpdate,
                },
                controller: 'HeSoController',
                templateUrl: '/app/components/heso/suaHstg.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: $scope.customFullscreen,// Only for -xs, -sm breakpoints.
                scope: $scope,
                preserveScope: true
            })
            .then(function (answer) {
                $scope.status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
        }

        function CapNhatHstg() {
            service.put('api/hstg/updated', $scope.HesltgUpdate, function (result) {
                $scope.cancel();
                ListHeSoThoiGian();
                notification.success('cập nhật hệ số thời gian thành công');
            }, function (error) { });
        }

        function deleteHstg(id) {
            $ngBootbox.confirm('bạn có chắc chắn muôn xóa Hệ số này không ? ').then(function (result) {
                var config = {
                    params: {
                        id: id
                    }
                }
                service.del('api/hstg/delete', config, function (result) {
                    ListHeSoThoiGian();
                    notification.success("Xóa hệ số thành công ~");
                }, function (error) {
                    notification.error("lỗi ~");
                });
            });
        }

        // HỆ SỐ NHÂN CÔNG
        $scope.Hesonc = {};
        $scope.HesoncUpdate = {};
        $scope.ThemHsnc = ThemHsnc;
        $scope.showFrmAddHSNC = showFrmAddHSNC;
        $scope.showFrmEditHSNC = showFrmEditHSNC;
        $scope.capNhatHnc = capNhatHnc;
        $scope.deleteHsnc = deleteHsnc;

        function showFrmAddHSNC(ev) {
            $mdDialog.show({
                controller: 'HeSoController',
                templateUrl: '/app/components/heso/themHsnc.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: $scope.customFullscreen,// Only for -xs, -sm breakpoints.
                scope: $scope,
                preserveScope: true
            })
            .then(function (answer) {
                $scope.status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
        }

        function ThemHsnc() {
            service.post('api/hsnc/created', $scope.Hesonc, function (result) {
                $scope.cancel();
                ListHeSoNhanCongKnc();
                notification.success('Thêm nhóm hệ số thành công');
            }, function (error) {
                notification.error(error.data);
            });
        }

        function showFrmEditHSNC(ev, id) {

            var config = {
                params: {
                    id: id
                }
            }
            service.get('api/hsnc/getbyid', config, function (result) {
                $scope.HesoncUpdate = result.data;
            }, function (error) {

            });

            $mdDialog.show({
                locals: {
                    HesoncUpdate: $scope.HesoncUpdate,
                },
                controller: 'HeSoController',
                templateUrl: '/app/components/heso/suaHsnc.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: $scope.customFullscreen,// Only for -xs, -sm breakpoints.
                scope: $scope,
                preserveScope: true
            })
            .then(function (answer) {
                $scope.status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
        }

        function capNhatHnc() {
            service.put('api/hsnc/updated', $scope.HesoncUpdate, function (result) {
                $scope.cancel();
                ListHeSoNhanCongKnc();
                notification.success('cập nhật hệ số thành công');
            }, function (error) {
                notification.error('lỗi ~');
            });
        }

        function deleteHsnc(id) {
            $ngBootbox.confirm('bạn có chắc chắn muôn xóa Hệ số này không ? ').then(function (result) {
                var config = {
                    params: {
                        id: id
                    }
                }
                service.del('api/hsnc/delete', config, function (result) {
                    ListHeSoNhanCongKnc();
                    notification.success("Xóa hệ số thành công ~");
                }, function (error) {
                    notification.error("lỗi ~");
                });
            });
        }


        function cancel() {
            $mdDialog.cancel();
        }


        function ListNhomCongViec() {
            service.get('api/nhomcv/getall', null, function(result) {
                $scope.ListNhomCongViec = result.data;
            },function (errer) {});
        }

        function ListHeSoNhanCongKnc() {
            service.get('api/hsnc/getall', null, function (result) {
                $scope.ListHeSoNhanCongKnc = result.data;
            }, function (errer) { });
        }

        function ListHeSoThoiGian() {
            service.get('api/hstg/getall', null, function (result) {
                $scope.ListHeSoThoiGian = result.data;
            }, function (errer) { });
        }

        function ListHeSoLap() {
            service.get('api/hsl/getall', null, function (result) {
                $scope.ListHeSoLap = result.data;
            }, function (errer) { });
        }

        ListNhomCongViec();
        ListHeSoThoiGian();
        ListHeSoLap();
        ListHeSoNhanCongKnc();
    }]);
})(angular.module("componentmodule"))