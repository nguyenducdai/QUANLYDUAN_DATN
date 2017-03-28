(function (app) {
    app.controller('thanhvienController', thanhvienController);

    thanhvienController.$inject = ['$scope', 'service', '$state', 'notification', '$mdDialog', '$rootScope']
    function thanhvienController($scope, service, $state, notification, $mdDialog, $rootScope) {

        $scope.ThanhVien = {
            Created_at: new Date,
            Updatted_at:new Date,
            Groups: []
        }
        $scope.themThanhVien = themThanhVien;
        $scope.groups = {}
        $scope.ListNhanVien = {}
        $scope.Detail = {}

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
                var html = '';
                html +=' <div class="box box-primary"><div class="box-body box-profile"><ul class="list-group list-group-unbordered">';
                html += ' <li class="list-group-item"><i class="fa fa-user-o" aria-hidden="true"></i> Họ và Tên <b>' + $scope.Detail.FullName + '</b></li>';
                html += ' <li class="list-group-item"><i class="fa fa-map-marker margin-r-5"></i> Địa chỉ ' + $scope.Detail.Adress + ' Giới tính <span class="label label-info">' + $scope.Detail.Sex + '</span></li>';
                html += ' <li class="list-group-item"><i class="fa fa-birthday-cake" aria-hidden="true"></i> Ngày Sinh : <a> ' + $scope.Detail.Birthday + '</a></li>';
                html += ' <li class="list-group-item"><i class="fa fa-mobile" aria-hidden="true"></i> Số điện thoại : <a> ' + $scope.Detail.PhoneNumber + '</a></li>';
                html += ' <li class="list-group-item"><i class="fa fa-envelope-o" aria-hidden="true"></i> Địa chỉ Mail <a> ' + $scope.Detail.Email + '</a></li>';
                html += ' <li class="list-group-item"><i class="fa fa-key" aria-hidden="true"></i> Chức Vụ <a> ' + $scope.Detail.Function + '</a></li>';
                html += ' <li class="list-group-item"><i class="fa fa-calendar" aria-hidden="true"></i> Thời gian bắt đầu làm việc <a>' + $scope.Detail.Startdate + '</a></li>';
                html += ' <li class="list-group-item">giới thiệu bản thân <br><a>' + $scope.Detail.Bio + '</a></li>';
                html +='  </ul></div></div>';
               $('#main-body-user').html(html);
            }, function (error) {
                notification.error(error);
            })
        }
        loadApplicationUser();
        loadApplicationGroup();
      
    }

})(angular.module('componentmodule'));