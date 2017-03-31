(function (app) {
    app.controller('themnhomnguoidungController', ['$scope', 'service', 'notification', '$state', function ($scope, service, notification, $state) {
        $scope.NhomNguoiDung = {
            Roles:[]
        }

        $scope.Roles = {}

        $scope.themNhomNguoiDung = function () {
            service.post('api/applicationgroup/created', $scope.NhomNguoiDung, function (result) {
                notification.success('Thêm nhóm người dùng thành công !');
                $state.go('nhomnguoidung');
            }, function (error) {
            });
        }

        function LoadRole() {
            service.get('api/applicationrole/getall', null, function (result) {
                $scope.Roles = result.data;
            }, function (error) {
            });
        }
        LoadRole();
    }]);
})(angular.module('componentmodule'));