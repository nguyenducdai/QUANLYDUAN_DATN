(function (app) {
    app.controller('suanhomnguoidungController', ['$scope', 'service', 'notification', '$stateParams', '$state', function ($scope, service, notification, $stateParams, $state) {
        $scope.NhomNguoiDung = {}

        function GetDetail() {
            var config = {
                params: {
                    id: $stateParams.id
                }
            }
            service.get('api/applicationgroup/detail', config, function (result) {
                $scope.NhomNguoiDung = result.data;
            }, function (error) {

            });
        }

        function LoadRoles() {
            service.get('api/applicationrole/getall', null, function (result) {
                $scope.Roles = result.data;
            }, function (error) {
            });
        }


        $scope.suaNhomNguoiDung = function () {
            service.put('api/applicationgroup/updated', $scope.NhomNguoiDung, function (result) {
                $state.go('nhomnguoidung');
                notification.success('Cập nhật nhóm người dùng thành công');
            }, function (error) {
                notification.success('Oop ?Lỗi');
            });
        }
        GetDetail();
        LoadRoles();
    }]);
})(angular.module('componentmodule'));